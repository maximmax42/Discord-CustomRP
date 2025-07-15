﻿using CommonMark;
using DiscordRPC;
using DiscordRPC.Helper;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Octokit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;
using Application = System.Windows.Forms.Application;
using Button = System.Windows.Forms.Button;
using DButton = DiscordRPC.Button;
using Timer = System.Timers.Timer;

namespace CustomRPC
{
    /*
     * TODO: profiles not as external files
     */

    /// <summary>
    /// A struct for handling preset importing/exporting.
    /// </summary>
    [Serializable]
    public struct Preset
    {
        public string ID;
        public int Type;
        public string Details;
        public string State;
        public int PartySize;
        public int PartyMax;
        public int Timestamps;
        public DateTime CustomTimestamp;
        public string LargeKey;
        public string LargeText;
        public string SmallKey;
        public string SmallText;
        public string Button1Text;
        public string Button1URL;
        public string Button2Text;
        public string Button2URL;
    }

    /// <summary>
    /// A struct for getting available image assets for current application.
    /// </summary>
    public struct ImageAssets
    {
        public string ID;
        public string Type;
        public string Name;
    }

    /// <summary>
    /// A struct describing activity types to use in a type selection combobox.
    /// </summary>
    public struct PresenceType
    {
        public string Name { get; private set; }
        public ActivityType Type { get; private set; }

        public PresenceType(string name, ActivityType type)
        {
            Name = name;
            Type = type;
        }
    }

    /// <summary>
    /// An enum for the timestamp setting.
    /// </summary>
    public enum TimestampType
    {
        None = 0,
        SinceStartup = 1,
        LocalTime = 2,
        Custom = 3, // These are hardcoded for backwards compatibility
        SincePresenceUpdate
    }

    public partial class MainForm : Form
    {
        /// <summary>
        /// Discord RPC Client.
        /// </summary>
        DiscordRpcClient client;

        /// <summary>
        /// List of presence buttons.
        /// </summary>
        List<DButton> buttonsList = new List<DButton>();

        /// <summary>
        /// Prevents some event handlers from executing while the app is loading.
        /// </summary>
        bool loading = true;
        /// <summary>
        /// Avoids recursion in <see cref="OnlyNumbers"/>.
        /// </summary>
        /// <remarks>
        /// ...This is stupid.
        /// </remarks>
        bool toAvoidRecursion = false;

        /// <summary>
        /// This bool makes sure the "The app will run in the tray" tooltip shows only once per session
        /// </summary>
        bool wasTooltipShown = false;

        /// <summary>
        /// A timer for automatic restart on connection error. Currently set to 10 seconds.
        /// </summary>
        Timer restartTimer = new Timer(10 * 1000);
        /// <summary>
        /// Limit for the amount of restart tries.
        /// </summary>
        int restartAttempts = 30;
        /// <summary>
        /// Counter for restart attempts left.
        /// </summary>
        int restartAttemptsLeft = -1;

        /// <summary>
        /// A timer that updates presence at midnight.
        /// </summary>
        Timer localTimeTimer = new Timer();

        /// <summary>
        /// Settings of the application. Self-explanatory.
        /// </summary>
        Properties.Settings settings = Properties.Settings.Default;

        /// <summary>
        /// GitHub client used for fetching all the releases of the app.
        /// </summary>
        GitHubClient githubClient = new GitHubClient(new ProductHeaderValue("CustomRP"));
        /// <summary>
        /// Latest release of the app available for downloading.
        /// </summary>
        Release latestRelease;

        /// <summary>
        /// A DateTime object showing since what moment you can make an API request in <see cref="FetchAssets(object, EventArgs)"/>.
        /// </summary>
        DateTime nextAssetCheck = DateTime.Now;
        /// <summary>
        /// A string showing what application ID was checked last in <see cref="FetchAssets(object, EventArgs)"/>.
        /// </summary>
        string lastIDChecked = "";

        /// <summary>
        /// A part of the URL path for docs.customrp.xyz links used in the app.
        /// Has the form of empty string if the app is in English or the docs aren't translated to the current UI language, "v/[locale]/" or "v/[locale]-[country]/" otherwise.
        /// </summary>
        string localeUrl = "";

        /// <summary>
        /// Timestamp of when the app started.
        /// </summary>
        readonly DateTime timestampStarted = DateTime.UtcNow;

        /// <summary>
        /// Path to the autorun link file.
        /// </summary>
        readonly string linkPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\CustomRP" + (Program.IsSecondInstance ? " 2" : "") + ".lnk";

        /// <summary>
        /// Resource manager. Yes I know, very descriptive.
        /// </summary>
        readonly System.ComponentModel.ComponentResourceManager res = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));

        /// <summary>
        /// List of locales docs.customrp.xyz is translated to.
        /// </summary>
        readonly List<string> translatedWikiLocales = new List<string> { "de", "es", "fi", "fr", "hi", "ko", "nl", "pl", "ro", "ru", "th", "uk", "vi" };

        /// <summary>
        /// List of available activity types.
        /// </summary>
        readonly List<PresenceType> presenceTypes = new List<PresenceType>
            {
                new PresenceType(Strings.presenceTypePlaying, ActivityType.Playing),
                new PresenceType(Strings.presenceTypeListening, ActivityType.Listening),
                new PresenceType(Strings.presenceTypeWatching, ActivityType.Watching),
                new PresenceType(Strings.presenceTypeCompeting, ActivityType.Competing),
            };

        /// <summary>
        /// Unicode character "No-Break Space" (" ").
        /// </summary>
        readonly string U00A0 = "\u00A0";
        /// <summary>
        /// Unicode character "Zero-Width Space" (invisible).
        /// </summary>
        readonly string U200B = "\u200B";

        /// <summary>
        /// The constructor of the form.
        /// </summary>
        /// <param name="preset">File location of a preset to load on startup or <see langword="null"/>.</param>
        public MainForm(string preset)
        {
            InitializeComponent();

            // Populating language related menu items
            Utils.LanguagesSetup(translatorsToolStripMenuItem, OpenPersonsPage, languageToolStripMenuItem, ChangeLanguage);

            // Populating supporters menu items
            Utils.SupportersSetup(supportersToolStripMenuItem, OpenPersonsPage);

            // Setting up dark/light mode
            ThemeSetup();

            // Setting up startup link for current user (enabled by default)
#if !DEBUG
            StartupSetup();
#endif

            // Setting up a restart timer
            restartTimer.AutoReset = false;
            restartTimer.Elapsed += RestartTimer_Elapsed;

            // Setting up a midnight presence update timer
            localTimeTimer.AutoReset = false;
            localTimeTimer.Elapsed += LocalTimeTimer_Elapsed;

            // If we supply a preset file to import on load, load it right away
            if (preset is string)
                LoadPreset(preset);

            // Setting up checkboxes because apparently property binding doesn't work
            runOnStartupToolStripMenuItem.Checked = settings.runOnStartup;
            startMinimizedToolStripMenuItem.Checked = settings.startMinimized;
            autoconnectToolStripMenuItem.Checked = settings.autoconnect;
            checkUpdatesToolStripMenuItem.Checked = settings.checkUpdates;
            allowAnalyticsToolStripMenuItem.Checked = Analytics.IsEnabledAsync().Result;
            darkModeToolStripMenuItem.Checked = settings.darkMode;

            // Checks the chosen language setting
            foreach (var toolStripItemObj in languageToolStripMenuItem.DropDownItems)
            {
                if (toolStripItemObj is ToolStripSeparator)
                    continue;

                var langItem = (ToolStripMenuItem)toolStripItemObj;

                if ((string)langItem.Tag == settings.language)
                {
                    langItem.Checked = true;
                    break;
                }
            }

            // Setting up activity type combobox
            comboBoxType.DisplayMember = "Name";
            comboBoxType.ValueMember = "Type";
            comboBoxType.DataSource = presenceTypes;
            comboBoxType.SelectedValue = (ActivityType)settings.type;

            // Set up tags for the radio buttons
            radioButtonNone.Tag = TimestampType.None;
            radioButtonStartTime.Tag = TimestampType.SinceStartup;
            radioButtonPresence.Tag = TimestampType.SincePresenceUpdate;
            radioButtonLocalTime.Tag = TimestampType.LocalTime;
            radioButtonCustom.Tag = TimestampType.Custom;

            // Checks the needed timestamp radiobuttons because settings binding can't do that
            switch ((TimestampType)settings.timestamps)
            {
                case TimestampType.None: // radioButtonNone.Checked = true; break;
                case TimestampType.SincePresenceUpdate: radioButtonPresence.Checked = true; break;
                case TimestampType.SinceStartup: radioButtonStartTime.Checked = true; break;
                case TimestampType.LocalTime: radioButtonLocalTime.Checked = true; break;
                case TimestampType.Custom: radioButtonCustom.Checked = true; break;
            }

            // Enable or disable the date and time picker depending on whether a custom timestamp setting is chosen
            dateTimePickerTimestamp.Enabled = radioButtonCustom.Checked;

            // Change the date and time picker's format according to system's culture
            dateTimePickerTimestamp.CustomFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " + CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;

            // If the app was launched for the first time (including since update), set the default time to current one
            if (settings.customTimestamp.CompareTo(new DateTime(1969, 1, 1, 0, 0, 0)) == 0)
                settings.customTimestamp = DateTime.Now;

            // Change the earliest date user can choose according to user's timezone
            dateTimePickerTimestamp.MinDate = new DateTime(1970, 1, 1, 0, 0, 1, DateTimeKind.Utc).ToLocalTime();

            // Localize the header of the tooltip because Visual Studio can't do that for some reason
            toolTipInfo.ToolTipTitle = Strings.information;

            // Localize the Disconnect button in the tray menu, unless it is already localized
            if (trayMenuDisconnect.Text == res.GetString("trayMenuDisconnect.Text", CultureInfo.GetCultureInfo("en")))
                trayMenuDisconnect.Text = res.GetString("buttonDisconnect.Text");

            // Localize the statusbar text in case the autoconnect is disabled
            toolStripStatusLabelStatus.Text = Strings.statusDisconnected;

            // Slightly changing the name of the tray tooptip and main window title for a second instance of the app
            if (Program.IsSecondInstance)
            {
                trayIcon.Text += " 2";
                Text += " 2";
            }

            // Set up a localeUrl variable if docs are translated to the current UI language.
            if (translatedWikiLocales.FindLast(localePredicate) is string locale && locale != "")
                localeUrl = "v/" + locale + '/';

            bool localePredicate(string loc)
            {
                var currentLocale = CultureInfo.CurrentUICulture.Name;
                return loc == currentLocale || loc == currentLocale.Split('-')[0];
            }

            // Set up shortcuts for certain menu elements
            newPresetToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            loadPresetToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            savePresetToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            uploadAssetsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.U;
            openTheManualToolStripMenuItem.ShortcutKeys = Keys.F1;

            loading = false;

            // Starts minimized to tray by default, unless you just changed language
            if (settings.changedLanguage || !settings.startMinimized)
                Show();

            // That means user has upgraded from older version without that flag
            if (settings.id != "" && settings.firstStart)
            {
                settings.firstStart = false;
                Utils.SaveSettings();
            }

            if (settings.firstStart)
            {
                // Asking if the user wants the manual
                var messageBox = MessageBox.Show(this, Strings.firstTimeRunText, Strings.firstTimeRun, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                if (messageBox == DialogResult.Yes)
                    // Opens the setup manual
                    Utils.OpenInBrowser("https://docs.customrp.xyz/" + localeUrl + "setting-up");

                settings.firstStart = false;
                Utils.SaveSettings();
            }

            if (settings.id != "" && ((settings.changedLanguage && settings.wasConnected) || (settings.autoconnect && !settings.changedLanguage)))
                Connect();

            CheckIfCrashed();

            if (settings.checkUpdates)
                CheckForUpdates();

            settings.changedLanguage = false;
        }

        /// <summary>
        /// Handles communication between instances.
        /// </summary>
        protected override void WndProc(ref Message message)
        {
            if (message.Msg == Program.WM_SHOWFIRSTINSTANCE)
            {
                MaximizeFromTray();
            }
            else if (message.Msg == Program.WM_IMPORTPRESET)
            {
                LoadPreset(File.ReadAllText(Program.IPCPath));
            }
            else if (message.Msg == 0x0016) // WM_ENDSESSION
            {
                Utils.SaveSettings();
                Application.Exit();
            }

            base.WndProc(ref message);
        }

        /// <summary>
        /// Sets up color scheme of the application.
        /// </summary>
        private void ThemeSetup()
        {
            if (WinApi.UseImmersiveDarkMode(Handle))
            {
                // Hacky way to forcefully redraw a window's title bar
                Opacity = 0.99;
                Opacity = 1;
            }

            CurrentColors.Update();

            BackColor = CurrentColors.BgColor;
            ForeColor = CurrentColors.TextColor;

            foreach (dynamic ctrl in Controls)
            {
                if (ctrl is TextBox || ctrl is ComboBox || ctrl is NumericUpDown)
                {
                    ctrl.BackColor = CurrentColors.BgTextFields;
                    ctrl.ForeColor = CurrentColors.TextColor;
                }
            }

            switch (ConnectionManager.State)
            {
                case ConnectionState.Disconnected:
                case ConnectionState.Connecting:
                case ConnectionState.UpdatingPresence:
                    textBoxID.BackColor = CurrentColors.BgTextFields;
                    break;
                case ConnectionState.Connected:
                    textBoxID.BackColor = CurrentColors.BgTextFieldsSuccess;
                    break;
                case ConnectionState.Error:
                    textBoxID.BackColor = CurrentColors.BgTextFieldsError;
                    break;
            }

            if (settings.darkMode)
            {
                ToolStripManager.Renderer = new DarkModeRenderer();
                buttonConnect.FlatStyle = buttonDisconnect.FlatStyle = buttonUpdatePresence.FlatStyle = FlatStyle.Flat;
            }
            else
            {
                ToolStripManager.Renderer = new LightModeRenderer();
                buttonConnect.FlatStyle = buttonDisconnect.FlatStyle = buttonUpdatePresence.FlatStyle = FlatStyle.Standard;
            }
        }

        /// <summary>
        /// Will be called 10 seconds after a failed connection to try and reconnect.
        /// </summary>
        private void RestartTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (restartAttemptsLeft == 0)
            {
                restartAttemptsLeft = -1;
                Invoke(new MethodInvoker(() => Disconnect()));
                return;
            }

            if (restartAttemptsLeft == -1)
                restartAttemptsLeft = restartAttempts;

            restartAttemptsLeft--;

            Invoke(new MethodInvoker(() => Connect()));
        }
        /// <summary>
        /// Will be called at midnight to update presence.
        /// </summary>
        private void LocalTimeTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Invoke(new MethodInvoker(() => SetPresence()));
        }

        /// <summary>
        /// Checks if the app has crashed in the previous session.
        /// </summary>
        private async void CheckIfCrashed()
        {
            if (!await Crashes.HasCrashedInLastSessionAsync())
                return;

            var report = await Crashes.GetLastSessionCrashReportAsync();

            new ErrorReportViewer(report.StackTrace).ShowDialog();
        }

        /// <summary>
        /// Checks for updates.
        /// </summary>
        /// <param name="manual"><see langword="True"/> if the user requested the check, <see langword="false"/> otherwise.</param>
        private async void CheckForUpdates(bool manual = false)
        {
            IReadOnlyList<Release> releases;

            // Fetching all releases
            try
            {
                releases = await githubClient.Repository.Release.GetAll("maximmax42", "Discord-CustomRP");
                latestRelease = releases[0];
            }
            catch
            {
                // If there's no internet or Github is down, do nothing, unless it's a user requested update check
                if (manual)
                    MessageBox.Show(this, Strings.errorNoInternet, Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string latestStr = latestRelease.TagName;

            if (latestStr == settings.ignoreVersion && !manual)
                return; // The user ignored this version; this gets ignored if the user requested the update check manually, maybe they changed their mind?

            Version current = VersionHelper.GetVersion(Application.ProductVersion);
            Version latest = VersionHelper.GetVersion(latestStr);

            if (current.CompareTo(latest) < 0) // If update is available...
            {
                var changelogBuilder = new System.Text.StringBuilder(); // ...build the changelog...

                foreach (var release in releases)
                {
                    Version releaseVer = VersionHelper.GetVersion(release.TagName);

                    if (releaseVer.Equals(current))
                        break;

                    var releaseBodyArr = release.Body.Split("\r\n".ToCharArray());
                    var releaseBody = "";

                    // Removing 1st ("Changes:") and 2 last lines (description to exe and zip files) from the changelog
                    for (int i = 1; i < releaseBodyArr.Length - 3; i++)
                    {
                        releaseBody += releaseBodyArr[i] + "\r\n";
                    }

                    changelogBuilder
                        .Append("<h3>" + release.Name + "</h3>")
                        .Append(CommonMarkConverter.Convert(releaseBody.Trim()));
                }

                string changelog = changelogBuilder.ToString();

                downloadUpdateToolStripMenuItem.Visible = true; // ...activate the "Download update" button...
                MaximizeFromTray(); // ...make sure the app window is shown if it was minimized...

                var messageBox = new UpdatePrompt(current, latest, changelog).ShowDialog(); // ...and show a dialog box telling there's an update

                if (messageBox == DialogResult.Yes)
                {
                    DownloadAndInstallUpdate();
                    downloadUpdateToolStripMenuItem.Enabled = false;
                }
                else if (messageBox == DialogResult.Ignore)
                {
                    settings.ignoreVersion = latestStr;
                    Analytics.TrackEvent("Ignored an update", new Dictionary<string, string> {
                        { "Version", latestStr }
                    });
                }

                checkUpdatesToolStripMenuItem.Checked = settings.checkUpdates;

                if (!settings.checkUpdates || messageBox == DialogResult.Ignore)
                    downloadUpdateToolStripMenuItem.Visible = false; // If user doesn't want update notifications, let's not bother them
            }
            else if (manual) // If there's no update available and it was a user initiated update check, notify them about it
                MessageBox.Show(this, Strings.noUpdatesFound, Strings.information, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// Downloading and installing latest update from GitHub.
        /// </summary>
        public async void DownloadAndInstallUpdate()
        {
            if (latestRelease == null)
                return; // Probably shouldn't happen, but just in case

            // Check whether the application is installed or used as a portable app
            int fileType = Application.StartupPath.EndsWith("Roaming\\CustomRP") ? 0 : 1; // 0 is exe, 1 is zip

            var wc = new WebClient();
            var exec = Path.GetTempPath() + latestRelease.Assets[fileType].Name;

            wc.DownloadProgressChanged += DownloadProgress;

            while (true)
            {
                try
                {
                    if (!File.Exists(exec))
                        await wc.DownloadFileTaskAsync(latestRelease.Assets[fileType].BrowserDownloadUrl, exec);

                    if (fileType == 1) // Open up app's folder for ease of manual update
                        Process.Start(Application.StartupPath);

                    Process.Start(exec);

                    Application.Exit();
                    break;
                }
                catch
                {
                    try
                    {
                        if (File.Exists(exec))
                            File.Delete(exec);
                    }
                    catch
                    {
                    }

                    var result = MessageBox.Show(this, Strings.errorUpdateFailed, Strings.error, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    if (result == DialogResult.Yes)
                        continue;
                    else if (result == DialogResult.No)
                        Utils.OpenInBrowser(latestRelease.Assets[fileType].BrowserDownloadUrl);

                    downloadUpdateToolStripMenuItem.Enabled = true;
                    downloadUpdateToolStripMenuItem.Text = res.GetString("downloadUpdateToolStripMenuItem.Text");

                    break;
                }
            }
        }

        /// <summary>
        /// Provides visual feedback for downloading.
        /// </summary>
        private void DownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            downloadUpdateToolStripMenuItem.Text = e.ProgressPercentage.ToString() + "%";
        }

        /// <summary>
        /// Initializes connection to the Discord API.
        /// </summary>
        /// <returns><see langword="False"/> if ID isn't set or something happened when establishing the connection, otherwise <see langword="true"/>.</returns>
        private bool Init()
        {
            if (settings.id == "")
            {
                MessageBox.Show(Strings.errorNoID, Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }

            if (client != null && !client.IsDisposed)
                client.Dispose(); // This stuff needs proper disposal

            client = new DiscordRpcClient(settings.id, (int)settings.pipe); // Assigning the ID
            client.OnPresenceUpdate += ClientOnPresenceUpdate;
            client.OnError += ClientOnError;
            client.OnConnectionFailed += ClientOnConnFailed;
            client.OnReady += ClientOnReady;

            client.Logger = new TimestampFileLogger(Application.StartupPath + "\\logs");

            return client.Initialize();
        }

        /// <summary>
        /// Will be called if successfully connected and sent the presence payload.
        /// </summary>
        private void ClientOnPresenceUpdate(object sender, DiscordRPC.Message.PresenceMessage args)
        {
            var presence = client.CurrentPresence;

            ConnectionManager.State = ConnectionState.Connected;

            Invoke(new MethodInvoker(() =>
            {
                textBoxID.BackColor = CurrentColors.BgTextFieldsSuccess;
                toolStripStatusLabelStatus.Text = Strings.statusConnected;
            }));

            // This only tracks whether or not the presence has those parameters set, not their content
            Analytics.TrackEvent("Updated presence", new Dictionary<string, string> {
                { "Party", presence.HasParty().ToString() },
                { "Timestamp", ((TimestampType)settings.timestamps).ToString() },
                { "Big image", presence.Assets?.LargeImageID.HasValue.ToString() },
                { "Small image", presence.Assets?.SmallImageID.HasValue.ToString() },
                { "Buttons", buttonsList.Count.ToString() }
            });

            restartTimer.Stop();
        }

        /// <summary>
        /// Will be called if failed connecting (due to bad app id or anything else).
        /// </summary>
        private void ClientOnError(object sender, DiscordRPC.Message.ErrorMessage args)
        {
            ConnectionManager.State = ConnectionState.Error;

            Invoke(new MethodInvoker(() =>
            {
                if (buttonConnect.Enabled) // Ignore if the user disconnected before connection was established
                    return;

                textBoxID.BackColor = CurrentColors.BgTextFieldsError;
                toolStripStatusLabelStatus.Text = Strings.statusError;
                if (args.Code != DiscordRPC.Message.ErrorCode.UnkownError) // Yes, there is a typo in the DiscordRPC lib
                    MessageBox.Show(this, args.Message, Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }));

            if (ConnectionManager.HasChanged()) // Ignore repeated calls caused by auto reconnect
                Analytics.TrackEvent("Connection error");

            restartTimer.Start();
        }

        /// <summary>
        /// Will be called if failed connecting (usually due to Discord being closed).
        /// </summary>
        private void ClientOnConnFailed(object sender, DiscordRPC.Message.ConnectionFailedMessage args)
        {
            Invoke(new MethodInvoker(() =>
            {
                if (buttonConnect.Enabled) // Ignore if the user disconnected before connection was established
                    return;

                textBoxID.BackColor = CurrentColors.BgTextFieldsError;
                toolStripStatusLabelStatus.Text = Strings.statusConnectionFailed;
            }));

            ConnectionManager.State = ConnectionState.Error;

            if (ConnectionManager.HasChanged()) // Ignore repeated calls caused by auto reconnect
                Analytics.TrackEvent("Connection failed");

            restartTimer.Start();
        }

        /// <summary>
        /// Will be called as soon as CustomRP connects to Discord.
        /// </summary>
        private void ClientOnReady(object sender, DiscordRPC.Message.ReadyMessage args)
        {
            Invoke(new MethodInvoker(() =>
            {
                toolStripStatusLabelUsername.Text = client.CurrentUser.Username;
                trayIcon.Text = $"{res.GetString("trayIcon.Text")}{(Program.IsSecondInstance ? " 2" : "")}\n{client.CurrentUser.Username}";

                buttonUpdatePresence.Enabled = true;

                ConnectionManager.State = ConnectionState.UpdatingPresence;
                toolStripStatusLabelStatus.Text = Strings.statusUpdatingPresence;

                SetPresence();
            }));
        }

        /// <summary>
        /// Sets up new presence from the settings.
        /// </summary>
        /// <returns><see langword="True"/> if presence was successfully set, <see langword="false"/> otherwise.</returns>
        private bool SetPresence()
        {
            if (client == null || client.IsDisposed)
                return false;

            localTimeTimer.Stop();

            // Add ZWS character if details or state textboxes start a no-break space character
            foreach (var paramBox in new[] { textBoxDetails, textBoxState })
            {
                if (paramBox.Text.StartsWith(U00A0) && paramBox.Text.Length < paramBox.MaxLength)
                    paramBox.Text = paramBox.Text.Insert(0, U200B);
                // In case it doesn't fit but there's at least 2 space symbols, we can replace one of them with the zws
                else if (paramBox.Text.StartsWith(U00A0 + U00A0))
                    paramBox.Text = U200B + paramBox.Text.Substring(1);
                // In case it still doesn't fit, why would you even use spaces then, 128 characters is a long string
            }

            if (settings.partySize > settings.partyMax)
                settings.partyMax = settings.partySize;

            settings.smallKey = settings.smallKey.Trim();
            settings.largeKey = settings.largeKey.Trim();
            settings.button1URL = settings.button1URL.Trim();
            settings.button2URL = settings.button2URL.Trim();

            string settingsWithCountdown = Countdownify(settings.details);

            var rp = new RichPresence()
            {
                Type = (ActivityType)settings.type,
                Details = settingsWithCountdown,
                State = settings.state,
                Party = new Party()
                {
                    ID = (settings.partySize > 0 && settings.partyMax > 0) ? "CustomRP" : "",
                    Size = (int)settings.partySize,
                    Max = (int)settings.partyMax
                },
            };

            Uri tempUri;

            /* Unused
            string GetProcessedURL(Uri origUri)
            {
                if (!origUri.Host.Contains("discordapp"))
                    return origUri.AbsoluteUri;

                var newUri = new UriBuilder(origUri.AbsoluteUri);
                var newQuery = origUri.ParseQueryString();

                newQuery.Remove("ex");
                newQuery.Remove("is");
                newQuery.Remove("hm");
                newUri.Query = newQuery.ToString();

                return newUri.Uri.AbsoluteUri;
            }
            */

            string Proxify(string key)
            {
                if (key != null)
                    return Regex.Replace(key, @"//((cdn)|(media))\.discordapp\.((com)|(net))/", "//customrp.xyz/proxy/");

                return key;
            }
            ;

            try
            {
                // Thank you Discord, very cool
                bool IsMpExternalStringOverLimit(Uri uri)
                {
                    return $"mp:external/43 characters that probably represent an id/{Uri.EscapeDataString(tempUri.Query)}/{tempUri.Scheme}/{(tempUri.IdnHost == "media.discordapp.net" ? "cdn.discordapp.com" : tempUri.IdnHost)}{tempUri.AbsolutePath}".Length > 256;
                }

                if (Uri.TryCreate(settings.smallKey, UriKind.Absolute, out tempUri))
                {
                    if (IsMpExternalStringOverLimit(tempUri))
                        throw new ArgumentException("Small");

                    settings.smallKey = tempUri.AbsoluteUri.Replace(tempUri.Host, tempUri.IdnHost);
                }

                if (Uri.TryCreate(settings.largeKey, UriKind.Absolute, out tempUri))
                {
                    if (IsMpExternalStringOverLimit(tempUri))
                        throw new ArgumentException("Large");

                    settings.largeKey = tempUri.AbsoluteUri.Replace(tempUri.Host, tempUri.IdnHost);
                }

                rp.Assets = new Assets()
                {
                    SmallImageKey = settings.smallKey,
                    SmallImageText = settings.smallText,
                    LargeImageKey = settings.largeKey,
                    LargeImageText = settings.largeText,
                };

                rp.Assets.SmallImageKey = Proxify(rp.Assets.SmallImageKey);
                rp.Assets.LargeImageKey = Proxify(rp.Assets.LargeImageKey);
            }
            catch (Exception e)
            {
                if (e is ArgumentException)
                    MessageBox.Show(Strings.errorInvalidImageURL + " (" + res.GetString("label" + e.Message + ".Text") + ")", Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                else
                    MessageBox.Show(e.Message, Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                return false;
            }
            finally
            {
                Utils.SaveSettings();
            }

            buttonsList.Clear();

            string AddProtocol(string url)
            {
                if (string.IsNullOrEmpty(url))
                    return url;

                string protocol = "https://";

                if (!url.Contains("://"))
                    return (protocol + url).Substring(0, Math.Min(textBoxButton1URL.MaxLength, url.Length + protocol.Length));

                return url;
            }

            settings.button1URL = AddProtocol(settings.button1URL);
            settings.button2URL = AddProtocol(settings.button2URL);

            try
            {
                if (Uri.TryCreate(settings.button1URL, UriKind.Absolute, out tempUri))
                    settings.button1URL = tempUri.AbsoluteUri.Replace(tempUri.Host, tempUri.IdnHost);

                if (Uri.TryCreate(settings.button2URL, UriKind.Absolute, out tempUri))
                    settings.button2URL = tempUri.AbsoluteUri.Replace(tempUri.Host, tempUri.IdnHost);

                Utils.SaveSettings();

                if (settings.button1Text != "" && settings.button1URL != "")
                    buttonsList.Add(new DButton()
                    {
                        Label = settings.button1Text,
                        Url = settings.button1URL
                    });

                if (settings.button2Text != "" && settings.button2URL != "")
                    buttonsList.Add(new DButton()
                    {
                        Label = settings.button2Text,
                        Url = settings.button2URL
                    });
            }
            catch
            {
                MessageBox.Show(Strings.errorInvalidURL, Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
            finally
            {
                Utils.SaveSettings();
            }

            rp.Buttons = buttonsList.ToArray();

            switch ((TimestampType)settings.timestamps)
            {
                case TimestampType.None: break;
                case TimestampType.SinceStartup: rp.Timestamps = new Timestamps(timestampStarted); break;
                case TimestampType.SincePresenceUpdate: rp.Timestamps = Timestamps.Now; break;
                case TimestampType.LocalTime:
                    rp.Timestamps = new Timestamps(DateTime.UtcNow.Subtract(new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)));
                    localTimeTimer.Interval = DateTime.Today.AddDays(1).AddSeconds(5).Subtract(DateTime.Now).TotalMilliseconds;
                    localTimeTimer.Start();
                    break;
                case TimestampType.Custom:
                    DateTime customTimestamp = dateTimePickerTimestamp.Value.ToUniversalTime();
                    /// I must apologize preemptively for this monster of an if-statement
                    /// Timestamps before 2001-09-09 01:46:40 UTC only work if you have a "dumb" presence (the one that only has ID,
                    /// timestamp and small image fields set)
                    /// Technically, it doesn't even matter what date you put in the rich presence timestamp, since it only shows the hours
                    /// since/to the timestamp
                    if (customTimestamp.CompareTo(new DateTime(2001, 9, 9, 1, 46, 40, DateTimeKind.Utc)) < 0 &&
                        !(string.IsNullOrEmpty(settings.details) && string.IsNullOrEmpty(settings.state) && settings.partySize == 0 &&
                        settings.partyMax == 0 && string.IsNullOrEmpty(settings.largeKey) && string.IsNullOrEmpty(settings.largeText) &&
                        string.IsNullOrEmpty(settings.smallText) && string.IsNullOrEmpty(settings.button1Text) &&
                        string.IsNullOrEmpty(settings.button1URL) && string.IsNullOrEmpty(settings.button2Text) &&
                        string.IsNullOrEmpty(settings.button2URL)))
                    {
                        customTimestamp = new DateTime(2002, 1, 1, customTimestamp.Hour, customTimestamp.Minute, customTimestamp.Second, DateTimeKind.Utc);
                    }
                    rp.Timestamps = customTimestamp.CompareTo(DateTime.UtcNow) < 0 ? new Timestamps(customTimestamp) : new Timestamps(DateTime.UtcNow, customTimestamp);
                    break;
            }

            ConnectionManager.State = ConnectionState.UpdatingPresence;
            toolStripStatusLabelStatus.Text = Strings.statusUpdatingPresence;
            client.SetPresence(rp);

            return true;
        }

        /// <summary>
        /// Sets up the startup link for the app.
        /// </summary>
        private void StartupSetup()
        {
            try
            {
                if (settings.runOnStartup && !File.Exists(linkPath)) // If run on startup is enabled and the link isn't in the Startup folder
                {
                    IWshRuntimeLibrary.WshShell wsh = new IWshRuntimeLibrary.WshShell();
                    IWshRuntimeLibrary.IWshShortcut shortcut = wsh.CreateShortcut(linkPath) as IWshRuntimeLibrary.IWshShortcut;
                    shortcut.Description = "Discord Custom Rich Presence Manager";
                    shortcut.TargetPath = Environment.CurrentDirectory + @"\CustomRP.exe";
                    shortcut.WorkingDirectory = Environment.CurrentDirectory + @"\";
                    if (Program.IsSecondInstance)
                        shortcut.Arguments = "--second-instance";
                    shortcut.Save();
                }
                else if (!settings.runOnStartup && File.Exists(linkPath)) // If run on startup is disabled and the link is in the Startup folder
                    File.Delete(linkPath);
            }
            catch (Exception e)
            {
                // I *think* this would only happen if an antivirus would intervene saving/deleting a file in a user folder,
                // therefore I'm just allowing the user to quickly try changing the option again
                runOnStartupToolStripMenuItem.Checked = !settings.runOnStartup;
                MessageBox.Show($"{Strings.errorStartupShortcut} {e.Message}", Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Called when you drag a file into app's window.
        /// </summary>
        private void DragDropEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// Called upon dropping a file.
        /// </summary>
        private void DragDropHandler(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is string[] files && files.Length > 0)
                LoadPreset(files[0]); // If multiple files are passed, only the first one gets imported
        }

        /// <summary>
        /// Called when you close the main window with the X button.
        /// </summary>
        private void MinimizeToTray(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing) // Checks if it was closed by user and not by system in a shutdown, for example
            {
                // Prevent closing and hide the window to tray instead
                e.Cancel = true;
                Hide();

                if (!(settings.startMinimized || wasTooltipShown))
                {
                    // Show a tooltip if it wasn't shown already and if the app doesn't start minimized 
                    trayIcon.ShowBalloonTip(500);
                    wasTooltipShown = true;
                }
            }
        }

        /// <summary>
        /// Called when you click the tray icon, balloon tip, or the "Open" option in the tray menu.
        /// </summary>
        private void MaximizeFromTray(object sender, EventArgs e)
        {
            if (e is MouseEventArgs ev && ev.Button != MouseButtons.Left)
                return;

            MaximizeFromTray();
        }

        /// <summary>
        /// Maximizes the window.
        /// </summary>
        private void MaximizeFromTray()
        {
            switch (ConnectionManager.State) // Because invoking doesn't work while the form is hidden
            {
                case ConnectionState.Disconnected:
                    textBoxID.BackColor = CurrentColors.BgTextFields;
                    toolStripStatusLabelStatus.Text = Strings.statusDisconnected;
                    break;
                case ConnectionState.Connecting:
                    textBoxID.BackColor = CurrentColors.BgTextFields;
                    toolStripStatusLabelStatus.Text = Strings.statusConnecting;
                    break;
                case ConnectionState.UpdatingPresence:
                    textBoxID.BackColor = CurrentColors.BgTextFields;
                    toolStripStatusLabelStatus.Text = Strings.statusUpdatingPresence;
                    break;
                case ConnectionState.Connected:
                    textBoxID.BackColor = CurrentColors.BgTextFieldsSuccess;
                    toolStripStatusLabelStatus.Text = Strings.statusConnected;
                    break;
                case ConnectionState.Error:
                    textBoxID.BackColor = CurrentColors.BgTextFieldsError;
                    toolStripStatusLabelStatus.Text = Strings.statusError;
                    break;
            }

            Show();
            Activate();
        }

        /// <summary>
        /// Called when you press New Preset button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewPreset(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, Strings.newPresetConfirmation, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            if (ConnectionManager.State != ConnectionState.Disconnected)
                Disconnect();

            textBoxID.Text = textBoxDetails.Text = textBoxState.Text =
                comboBoxLargeKey.Text = textBoxLargeText.Text =
                comboBoxSmallKey.Text = textBoxSmallText.Text =
                textBoxButton1Text.Text = textBoxButton1URL.Text =
                textBoxButton2Text.Text = textBoxButton2URL.Text = "";
            comboBoxType.SelectedValue = ActivityType.Playing;
            numericUpDownPartySize.Value = numericUpDownPartyMax.Value = 0;
            radioButtonNone.Checked = true;
        }

        /// <summary>
        /// Base function for loading a preset.
        /// </summary>
        /// <param name="file">A file stream of the preset file.</param>
        private void LoadPreset(Stream file)
        {
            try
            {
                var xs = new XmlSerializer(typeof(Preset)); // Using XML here because... why not? Settings are already saved in XML
                var preset = (Preset)xs.Deserialize(file);

                bool wasConnected = buttonDisconnect.Enabled;
                bool isNewID = settings.id != preset.ID;

                settings.id = preset.ID;
                settings.type = preset.Type;
                settings.details = preset.Details;
                settings.state = preset.State;
                settings.partySize = preset.PartySize;
                settings.partyMax = preset.PartyMax;
                settings.timestamps = preset.Timestamps;
                settings.customTimestamp = preset.CustomTimestamp;
                settings.largeKey = preset.LargeKey;
                settings.largeText = preset.LargeText;
                settings.smallKey = preset.SmallKey;
                settings.smallText = preset.SmallText;
                settings.button1Text = preset.Button1Text;
                settings.button1URL = preset.Button1URL;
                settings.button2Text = preset.Button2Text;
                settings.button2URL = preset.Button2URL;
                Utils.SaveSettings();

                comboBoxType.SelectedValue = (ActivityType)settings.type;

                switch ((TimestampType)settings.timestamps)
                {
                    case TimestampType.None: // radioButtonNone.Checked = true; break;
                    case TimestampType.SincePresenceUpdate: radioButtonPresence.Checked = true; break;
                    case TimestampType.SinceStartup: radioButtonStartTime.Checked = true; break;
                    case TimestampType.LocalTime: radioButtonLocalTime.Checked = true; break;
                    case TimestampType.Custom: radioButtonCustom.Checked = true; break;
                }

                Analytics.TrackEvent("Loaded a preset");

                if (!wasConnected)
                    return;

                if (isNewID || ConnectionManager.State != ConnectionState.Connected)
                    Reconnect();
                else
                    SetPresence();
            }
            catch
            {
                MessageBox.Show(Strings.errorInvalidPresetFile, Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                file.Close();
            }
        }

        /// <summary>
        /// Called when you press Load Preset button.
        /// </summary>
        private void LoadPreset(object sender, EventArgs e)
        {
            var presetFile = new OpenFileDialog()
            {
                Filter = "CustomRP Preset|*.crp"
            };

            if (presetFile.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                LoadPreset(presetFile.OpenFile());
            }
            catch
            {
                MessageBox.Show(Strings.errorInvalidPresetFile, Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads preset from a file.
        /// </summary>
        /// <param name="filePath">The path to the preset file.</param>
        private void LoadPreset(string filePath)
        {
            try
            {
                LoadPreset(File.OpenRead(filePath));
            }
            catch
            {
                MessageBox.Show(Strings.errorInvalidPresetFile, Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Called when you press Save Preset button.
        /// </summary>
        private void SavePreset(object sender, EventArgs e)
        {
            var xs = new XmlSerializer(typeof(Preset));
            var presetFile = new SaveFileDialog()
            {
                Filter = "CustomRP Preset|*.crp"
            };

            if (presetFile.ShowDialog() != DialogResult.OK || presetFile.FileNames.Length == 0)
                return;

            while (true)
            {
                try
                {
                    using (var file = presetFile.OpenFile())
                    {
                        xs.Serialize(file, new Preset()
                        {
                            ID = settings.id,
                            Type = settings.type,
                            Details = settings.details,
                            State = settings.state,
                            PartySize = (int)settings.partySize,
                            PartyMax = (int)settings.partyMax,
                            Timestamps = settings.timestamps,
                            CustomTimestamp = settings.customTimestamp,
                            LargeKey = settings.largeKey,
                            LargeText = settings.largeText,
                            SmallKey = settings.smallKey,
                            SmallText = settings.smallText,
                            Button1Text = settings.button1Text,
                            Button1URL = settings.button1URL,
                            Button2Text = settings.button2Text,
                            Button2URL = settings.button2URL,
                        });
                    }

                    Analytics.TrackEvent("Saved a preset");

                    return;
                }
                catch (IOException ex)
                {
                    if (MessageBox.Show(ex.Message, Strings.error, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                        return;
                }
            }
        }

        /// <summary>
        /// Called when you press Upload Assets button.
        /// </summary>
        private void OpenDiscordSite(object sender, EventArgs e)
        {
            if (settings.id == "")
            {
                MessageBox.Show(Strings.errorNoID, Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }

            Utils.OpenInBrowser("https://discord.com/developers/applications/" + settings.id + "/rich-presence/assets");
        }

        /// <summary>
        /// Called when you click File -> Quit or right-click on the tray icon and choose Quit.
        /// </summary>
        private void Quit(object sender, EventArgs e)
        {
            if (client != null)
                client.Dispose();

            if (Utils.SaveSettings())
                Application.Exit();
        }

        /// <summary>
        /// Called when you press anything in Settings submenu of menu strip.
        /// </summary>
        /// <exception cref="NotImplementedException">In case I forgot something.</exception>
        private void SaveMenuSettings(object sender, EventArgs e)
        {
            if (loading)
                return;

            var setting = (ToolStripMenuItem)sender;
            var properName = setting.Name.Replace("ToolStripMenuItem", "");

            switch (properName)
            {
                case "runOnStartup":
                    // Apparently property binding doesn't work either for checkboxes or for bool variables
                    settings.runOnStartup = setting.Checked;
                    StartupSetup();
                    break;
                case "startMinimized":
                    settings.startMinimized = setting.Checked;
                    break;
                case "autoconnect":
                    settings.autoconnect = setting.Checked;
                    break;
                case "checkUpdates":
                    settings.checkUpdates = setting.Checked;
                    if (setting.Checked)
                        CheckForUpdates();
                    break;
                case "allowAnalytics":
                    settings.analytics = setting.Checked;
                    Analytics.SetEnabledAsync(setting.Checked);
                    break;
                case "darkMode":
                    settings.darkMode = setting.Checked;
                    ThemeSetup();
                    break;
                default:
                    throw new NotImplementedException(properName);
            }

            Utils.SaveSettings();
        }

        /// <summary>
        /// Called when you change the language.
        /// </summary>
        private void ChangeLanguage(object sender, EventArgs e)
        {
            var lang = (ToolStripMenuItem)sender;

            settings.language = (string)lang.Tag;
            settings.changedLanguage = true;
            settings.wasConnected = buttonDisconnect.Enabled;
            Utils.SaveSettings();
            Program.AppMutex.Close();
            Application.Restart();
        }

        /// <summary>
        /// Called when you press on menu items that open websites.
        /// </summary>
        private void OpenSite(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            var url = (string)item.Tag;

            Utils.OpenInBrowser(url.Replace("docs.customrp.xyz/", "docs.customrp.xyz/" + localeUrl));
        }

        /// <summary>
        /// Called when you press on a translator's or supporter's nickname.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenPersonsPage(object sender, EventArgs e)
        {
            var personItem = (ToolStripMenuItem)sender;

            if (personItem.Tag == null)
                return;

            var (personType, personUrl) = (ValueTuple<string, string>)personItem.Tag;

            if (string.IsNullOrWhiteSpace(personUrl))
                return;

            string personName = personItem.Text;

            if (personType == "supporter")
                personName = personName.Replace(" - ", "|").Split('|')[0]; // Doing this replacement thing just in case someone will have "-" in their nickname

            Analytics.TrackEvent("Clicked on a " + personType, new Dictionary<string, string> {
                { "Name", personName },
                { "URL", personUrl }
            });

            Utils.OpenInBrowser(personUrl);
        }

        /// <summary>
        /// Called when you press Check for updates... button.
        /// </summary>
        private void CheckForUpdates(object sender, EventArgs e) => CheckForUpdates(true);

        /// <summary>
        /// Called when you press About... button.
        /// </summary>
        private void ShowAbout(object sender, EventArgs e)
        {
            Analytics.TrackEvent("Opened about window");
            new About().ShowDialog(this);
        }

        /// <summary>
        /// Called when you press Download Update button.
        /// </summary>
        private void DownloadUpdate(object sender, EventArgs e)
        {
            downloadUpdateToolStripMenuItem.Enabled = false;
            DownloadAndInstallUpdate();
        }

        /// <summary>
        /// Called when you input into the ID textbox.
        /// </summary>
        /// <remarks>
        /// This is overcomplicated isn't it, but hey, at least it works with pasting as well!
        /// At least it used to. Huh.
        /// </remarks>
        private void OnlyNumbers(object sender, EventArgs e)
        {
            if (toAvoidRecursion || textBoxID.Text == "")
                return;

            textBoxID.ReadOnly = true; // Not sure if I need it?
            toAvoidRecursion = true; // Just so this handler doesn't get called again on...

            int sel = textBoxID.SelectionStart; // Current cursor position
            int changed = 0;

            string newline = "";

            foreach (var symbol in textBoxID.Text)
            {
                if (char.IsDigit(symbol))
                    newline += symbol;
                else
                    changed++;
            }

            if (changed > 0)
            {
                textBoxID.Text = newline; // ...this line
                textBoxID.SelectionStart = sel - changed;
                textBoxID.SelectionLength = 0;
            }

            textBoxID.ReadOnly = false;
            toAvoidRecursion = false;
        }

        /// <summary>
        /// Called when a presence type is changed in comboBoxType.
        /// </summary>
        private void PresenceTypeChanged(object sender, EventArgs e)
        {
            if (comboBoxType.Items.Count == 0 || comboBoxType.SelectedItem == null)
                return;

            ActivityType type = (ActivityType)comboBoxType.SelectedValue;

            if (!loading)
            {
                settings.type = (int)type;
                Utils.SaveSettings();
            }

            bool canHaveParty = true, canHaveTimestamps = true;

            if (type != ActivityType.Playing)
                canHaveParty = false;

            if (type == ActivityType.Competing)
                canHaveTimestamps = false;

            numericUpDownPartySize.Enabled = canHaveParty;
            numericUpDownPartyMax.Enabled = canHaveParty;
            panelTimestamps.Enabled = canHaveTimestamps;
        }

        /// <summary>
        /// Called on Leave event for all text fields except ID.
        /// </summary>
        private void TrimTextBoxes(object sender, EventArgs e)
        {
            Control box = (Control)sender;
            if (box.Text.StartsWith(U00A0))
                return;
            box.Text = box.Text.Trim();
        }

        /// <summary>
        /// Called on Validating event for all text fields except ID.
        /// </summary>
        private void LengthValidationFocus(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dynamic box = sender;

            bool useBytes = box.Name.EndsWith("Button1Text") || box.Name.EndsWith("Button2Text");

            if (box.Text.Length == 1 || useBytes && !StringTools.WithinLength(box.Text, box.MaxLength))
            {
                e.Cancel = true;
                System.Media.SystemSounds.Beep.Play();
            }
        }

        /// <summary>
        /// Called on TextChanged event for all text fields except ID.
        /// </summary>
        private void LengthValidation(object sender, EventArgs e)
        {
            dynamic box = sender;

            bool useBytes = box.Name.EndsWith("Button1Text") || box.Name.EndsWith("Button2Text");

            box.BackColor = (box.Text.Length == 1 || useBytes && !StringTools.WithinLength(box.Text, box.MaxLength)) ? CurrentColors.BgTextFieldsError : CurrentColors.BgTextFields;
        }

        /// <summary>
        /// Called on Validating event to validate party size values.
        /// </summary>
        private void PartySizeValidation(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (sender == numericUpDownPartyMax && numericUpDownPartyMax.Value == 0)
                numericUpDownPartySize.Value = 0;
            else if (numericUpDownPartySize.Value > numericUpDownPartyMax.Value)
            {
                numericUpDownPartyMax.Value = numericUpDownPartySize.Value;
                // If user sets max value less than current value, play error sound, but not if user sets current value more than max
                if (sender == numericUpDownPartyMax) System.Media.SystemSounds.Beep.Play();
            }
        }

        /// <summary>
        /// Called when a timestamp radiobutton changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimestampsChanged(object sender, EventArgs e)
        {
            if (loading)
                return;

            RadioButton btn = (RadioButton)sender;

            if (!btn.Checked)
                return;

            // settings.timestamps = btn.TabIndex; // I mean... it's a great container for int values
            // It was, until I needed to add a new type in the middle of the list
            settings.timestamps = (int)btn.Tag;
            Utils.SaveSettings();

            dateTimePickerTimestamp.Enabled = (TimestampType)settings.timestamps == TimestampType.Custom;
        }

        /// <summary>
        /// Called on DropDown event for <see cref="comboBoxLargeKey"/> and <see cref="comboBoxSmallKey"/>.
        /// </summary>
        private void FetchAssets(object sender, EventArgs e)
        {
            if (settings.id == "" || (lastIDChecked == settings.id && nextAssetCheck.CompareTo(DateTime.Now) > 0))
                return;

            lastIDChecked = settings.id;

            comboBoxLargeKey.Items.Clear();
            comboBoxSmallKey.Items.Clear();

            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 0, 15);

                try
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    var res = client.GetAsync($"https://discord.com/api/oauth2/applications/{settings.id}/assets").Result;

                    if (res.IsSuccessStatusCode)
                    {
                        var resList = res.Content.ReadAsAsync<List<ImageAssets>>().Result;

                        resList.ForEach(asset =>
                        {
                            comboBoxLargeKey.Items.Add(asset.Name);
                            comboBoxSmallKey.Items.Add(asset.Name);
                        });

                        nextAssetCheck = DateTime.Now.Add(new TimeSpan(0, 1, 0));
                    }
                }
                catch
                {
                    MessageBox.Show(Strings.errorNoInternet, Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Called on Paint event for all the buttons.
        /// </summary>
        private void ButtonPaint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Enabled)
                return;

            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, e.ClipRectangle, CurrentColors.TextInactive, flags);
        }

        /// <summary>
        /// Called when you press the Connect button or right-click on the tray icon and choose Reconnect.
        /// </summary>
        private void Connect(object sender, EventArgs e)
        {
#if DEBUG
            if (ModifierKeys == (Keys.Control | Keys.Alt) && sender is Button)
                Crashes.GenerateTestCrash();
#endif
            if (ModifierKeys == (Keys.Control | Keys.Shift) && sender is Button)
            {
                Analytics.TrackEvent("Opened pipe selector window");

                new PipeSelector().ShowDialog(this);
                return;
            }

            Connect();
        }

        /// <summary>
        /// Connects to Discord and makes UI changes.
        /// </summary>
        private void Connect()
        {
            Utils.SaveSettings();

            if (Init()) // If successfully initialized...
            {
                if (ConnectionManager.State == ConnectionState.Disconnected)
                    Analytics.TrackEvent("Connected"); // Only send analytics if connect function was called from disconnected state

                ConnectionManager.State = ConnectionState.Connecting;

                buttonConnect.Enabled = false; // ...disable Connect button...
                buttonDisconnect.Enabled = true; // ...enable Disconnect button...
                trayMenuDisconnect.Enabled = true; // ...enable Disconnect button in tray menu...
                textBoxID.ReadOnly = true; // ...make the ID field read only...
                toolStripStatusLabelStatus.Text = Strings.statusConnecting; // and update the connection status label
            }
        }

        /// <summary>
        /// Called when you press the Disconnect button.
        /// </summary>
        private void Disconnect(object sender, EventArgs e) => Disconnect();

        /// <summary>
        /// Disconnects from Discord and makes UI changes.
        /// </summary>
        private void Disconnect()
        {
            buttonConnect.Enabled = true;
            buttonDisconnect.Enabled = false;
            trayMenuDisconnect.Enabled = false;
            buttonUpdatePresence.Enabled = false;
            textBoxID.ReadOnly = false;
            toolStripStatusLabelUsername.Text = "";
            toolStripStatusLabelStatus.Text = Strings.statusDisconnected;
            trayIcon.Text = $"{res.GetString("trayIcon.Text")}{(Program.IsSecondInstance ? " 2" : "")}";

            textBoxID.BackColor = CurrentColors.BgTextFields;
            ConnectionManager.State = ConnectionState.Disconnected;

            restartTimer.Stop();
            localTimeTimer.Stop();

            client.Dispose();

            Analytics.TrackEvent("Disconnected");
        }

        /// <summary>
        /// Disconnects from Discord and instantly connects back.
        /// </summary>
        private void Reconnect()
        {
            // Quick disconnect
            restartTimer.Stop();
            localTimeTimer.Stop();
            client.Dispose();
            textBoxID.BackColor = CurrentColors.BgTextFields;

            // Quick connect
            Utils.SaveSettings();
            if (Init())
            {
                ConnectionManager.State = ConnectionState.Connecting;
                toolStripStatusLabelStatus.Text = Strings.statusConnecting;
            }
            else
                Disconnect(); // In case something goes wrong, disconnect fully.
        }

        /// <summary>
        /// Called when you press the Update Presence button.
        /// </summary>
        private void Update(object sender, EventArgs e)
        {
            Utils.SaveSettings();
            SetPresence();
        }

        /// <summary>
        /// Replace {countdown: dd/mm/yyyy} with number of days until that day
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string Countdownify(string input)
        {
            string[] split = input.Split('{', '}');
            string output = "";

            foreach (var s in split)
            {
                if (s.StartsWith("countdown: ") && s.Length == 21)
                {
                    string date = s.Substring(11, 10);

                    DateTime dateTime = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    int daysUntilDate = dateTime.Subtract(DateTime.Now).Days;
                    int daysLeft = Math.Max(daysUntilDate, 0); // If we pass the day we don't want to go negative

                    output += daysLeft;
                }
                else
                {
                    output += s;
                }
            }

            return output;
        }
    }
}