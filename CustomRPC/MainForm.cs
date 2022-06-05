using CommonMark;
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
        /// Timestamp of when the app started.
        /// </summary>
        readonly DateTime timestampStarted = DateTime.UtcNow;

        /// <summary>
        /// Path to the autorun link file.
        /// </summary>
        readonly string linkPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\CustomRP.lnk";

        /// <summary>
        /// Resource manager. Yes I know, very descriptive.
        /// </summary>
        System.ComponentModel.ComponentResourceManager res = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));

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
            StartupSetup();

            // Setting up a restart timer
            restartTimer.AutoReset = false;
            restartTimer.Elapsed += RestartTimer_Elapsed;

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

            // Checks the needed timestamp radiobuttons because settings binding can't do that
            switch (settings.timestamps)
            {
                case 0: radioButtonNone.Checked = true; break;
                case 1: radioButtonStartTime.Checked = true; break;
                case 2: radioButtonLocalTime.Checked = true; break;
                case 3: radioButtonCustom.Checked = true; break;
            }

            // Enable or disable the date and time picker depending on whether a custom timestamp setting is chosen
            dateTimePickerTimestamp.Enabled = radioButtonCustom.Checked;

            // Change the date and time picker's format according to system's culture
            dateTimePickerTimestamp.CustomFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " + CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;

            // If the app was launched for the first time (including since update), set the default time to current one
            if (settings.customTimestamp.CompareTo(new DateTime(1969, 1, 1, 0, 0, 0)) == 0)
                settings.customTimestamp = DateTime.Now;

            // Change the earliest date user can choose according to user's timezone
            dateTimePickerTimestamp.MinDate = new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime();

            // Localize the header of the tooltip because Visual Studio can't do that for some reason
            toolTipInfo.ToolTipTitle = Strings.information;

            // Localize the Disconnect button in the tray menu
            trayMenuDisconnect.Text = buttonDisconnect.Text;

            // Localize the statusbar text in case the autoconnect is disabled
            toolStripStatusLabelStatus.Text = Strings.statusDisconnected;

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
                    Process.Start("https://github.com/maximmax42/Discord-CustomRP/wiki/Setting-Up");

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
                LoadPreset(Path.GetTempPath() + "preset.crp");
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
                case ConnectionType.Disconnected:
                    textBoxID.BackColor = CurrentColors.BgTextFields;
                    break;
                case ConnectionType.Connected:
                    textBoxID.BackColor = CurrentColors.BgTextFieldsSuccess;
                    break;
                case ConnectionType.Error:
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
            }
            catch
            {
                // If there's no internet or Github is down, do nothing, unless it's a user requested update check
                if (manual)
                    MessageBox.Show(this, Strings.errorNoInternet, Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            latestRelease = releases[0];

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
                    if (File.Exists(exec))
                        File.Delete(exec);

                    var result = MessageBox.Show(this, Strings.errorUpdateFailed, Strings.error, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    if (result == DialogResult.Yes)
                        continue;
                    else if (result == DialogResult.No)
                        Process.Start(latestRelease.Assets[fileType].BrowserDownloadUrl);

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
        /// <returns><see langword="False"/> if ID isn't set, otherwise <see langword="true"/>.</returns>
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

            client.Logger = new TimestampFileLogger(Application.StartupPath + "\\rpc.log");

            client.Initialize();

            SetPresence();

            return true;
        }

        /// <summary>
        /// Will be called if successfully connected and sent the presence payload.
        /// </summary>
        private void ClientOnPresenceUpdate(object sender, DiscordRPC.Message.PresenceMessage args)
        {
            var presence = client.CurrentPresence;

            ConnectionManager.State = ConnectionType.Connected;

            Invoke(new MethodInvoker(() =>
            {
                textBoxID.BackColor = CurrentColors.BgTextFieldsSuccess;
                toolStripStatusLabelStatus.Text = Strings.statusConnected;
            }));

            // This only tracks whether or not the presence has those parameters set, not their content
            Analytics.TrackEvent("Updated presence", new Dictionary<string, string> {
                { "Party", presence.HasParty().ToString() },
                { "Timestamp", settings.timestamps.ToString() },
                { "Big image", presence.Assets.LargeImageID.HasValue.ToString() },
                { "Small image", presence.Assets.SmallImageID.HasValue.ToString() },
                { "Buttons", buttonsList.Count.ToString() }
            });

            restartTimer.Stop();
        }

        /// <summary>
        /// Will be called if failed connecting (due to bad app id or anything else).
        /// </summary>
        private void ClientOnError(object sender, DiscordRPC.Message.ErrorMessage args)
        {
            ConnectionManager.State = ConnectionType.Error;

            Invoke(new MethodInvoker(() =>
            {
                if (buttonConnect.Enabled) // Ignore if the user disconnected before connection was established
                    return;

                textBoxID.BackColor = CurrentColors.BgTextFieldsError;
                toolStripStatusLabelStatus.Text = Strings.statusError;
            }));

            if (ConnectionManager.HasChanged()) // Ignore repeated calls caused by auto reconnect
                Analytics.TrackEvent("Connection error");

            restartTimer.Start();
        }

        /// <summary>
        /// Will be called if failed connecting (mostly due to Discord being closed).
        /// </summary>
        private void ClientOnConnFailed(object sender, DiscordRPC.Message.ConnectionFailedMessage args)
        {
            ConnectionManager.State = ConnectionType.Error;

            Invoke(new MethodInvoker(() =>
            {
                if (buttonConnect.Enabled) // Ignore if the user disconnected before connection was established
                    return;

                textBoxID.BackColor = CurrentColors.BgTextFieldsError;
                toolStripStatusLabelStatus.Text = Strings.statusConnectionFailed;
            }));

            if (ConnectionManager.HasChanged()) // Ignore repeated calls caused by auto reconnect
                Analytics.TrackEvent("Connection failed");

            restartTimer.Start();
        }

        /// <summary>
        /// Sets up new presence from the settings.
        /// </summary>
        private void SetPresence()
        {
            if (client == null || client.IsDisposed)
                return;

            if (settings.partySize > settings.partyMax)
                numericUpDownPartyMax.Value = settings.partySize;

            var rp = new RichPresence()
            {
                Details = settings.details,
                State = settings.state,
                Assets = new Assets()
                {
                    SmallImageKey = settings.smallKey,
                    SmallImageText = settings.smallText,
                    LargeImageKey = settings.largeKey,
                    LargeImageText = settings.largeText,
                },
                Party = new Party()
                {
                    ID = (settings.partySize > 0 && settings.partyMax > 0) ? "CustomRP" : "",
                    Size = (int)settings.partySize,
                    Max = (int)settings.partyMax
                },
            };

            // Is there a way to not write this code twice? I don't think so since strings are immutable.

            if (rp.Assets.LargeImageKey != null
                && !Regex.IsMatch(rp.Assets.LargeImageKey, "//(www\\.)?customrp\\.xyz")
                && Regex.IsMatch(rp.Assets.LargeImageKey, "//((cdn)|(media))\\.discordapp\\.((com)|(net))/"))
                rp.Assets.LargeImageKey = "https://customrp.xyz/p/?u=" + rp.Assets.LargeImageKey;

            if (rp.Assets.SmallImageKey != null
                && !Regex.IsMatch(rp.Assets.SmallImageKey, "//(www\\.)?customrp\\.xyz")
                && Regex.IsMatch(rp.Assets.SmallImageKey, "//((cdn)|(media))\\.discordapp\\.((com)|(net))/"))
                rp.Assets.SmallImageKey = "https://customrp.xyz/p/?u=" + rp.Assets.SmallImageKey;

            buttonsList.Clear();

            try
            {
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
                return;
            }

            rp.Buttons = buttonsList.ToArray();

            switch (settings.timestamps)
            {
                case 0: break;
                case 1: rp.Timestamps = new Timestamps(timestampStarted); break;
                case 2: rp.Timestamps = new Timestamps(DateTime.UtcNow.Subtract(new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second))); break;
                case 3:
                    DateTime customTimestamp = dateTimePickerTimestamp.Value.ToUniversalTime();
                    rp.Timestamps = customTimestamp.CompareTo(DateTime.UtcNow) < 0 ? new Timestamps(customTimestamp) : new Timestamps(DateTime.UtcNow, customTimestamp);
                    break;
            }

            client.SetPresence(rp);
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
                MessageBox.Show(e.Message, Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                if (!(settings.startMinimized || settings.wasTooltipShown))
                {
                    // Show a tooltip if it wasn't shown already and if the app doesn't start minimized 
                    trayIcon.ShowBalloonTip(500);
                    settings.wasTooltipShown = true;
                }
            }
        }

        /// <summary>
        /// Called when you double click the tray icon.
        /// </summary>
        private void MaximizeFromTray(object sender, EventArgs e) => MaximizeFromTray();

        /// <summary>
        /// Maximizes the window.
        /// </summary>
        private void MaximizeFromTray()
        {
            switch (ConnectionManager.State) // Because invoking doesn't work while the form is hidden
            {
                case ConnectionType.Disconnected:
                    textBoxID.BackColor = CurrentColors.BgTextFields;
                    toolStripStatusLabelStatus.Text = Strings.statusDisconnected;
                    break;
                case ConnectionType.Connected:
                    textBoxID.BackColor = CurrentColors.BgTextFieldsSuccess;
                    toolStripStatusLabelStatus.Text = Strings.statusConnected;
                    break;
                case ConnectionType.Error:
                    textBoxID.BackColor = CurrentColors.BgTextFieldsError;
                    toolStripStatusLabelStatus.Text = Strings.statusError;
                    break;
            }

            Show();
            Activate();
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

                if (wasConnected)
                    Disconnect();

                settings.id = preset.ID;
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

                switch (settings.timestamps)
                {
                    case 0: radioButtonNone.Checked = true; break;
                    case 1: radioButtonStartTime.Checked = true; break;
                    case 2: radioButtonLocalTime.Checked = true; break;
                    case 3: radioButtonCustom.Checked = true; break;
                }

                if (wasConnected)
                    Connect();

                Analytics.TrackEvent("Loaded a preset");
            }
            catch
            {
                MessageBox.Show(Strings.errorInvalidPresetFile, Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            file.Close();
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

            if (presetFile.ShowDialog() != DialogResult.OK)
                return;

            using (var file = presetFile.OpenFile())
            {
                xs.Serialize(file, new Preset()
                {
                    ID = settings.id,
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
        }

        /// <summary>
        /// Called when you press Upload Assets button.
        /// </summary>
        private void OpenDiscordSite(object sender, EventArgs e)
        {
            if (settings.id == "")
                return;

            Process.Start("https://discord.com/developers/applications/" + settings.id + "/rich-presence/assets");
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

            switch (setting.Name)
            {
                case "runOnStartupToolStripMenuItem":
                    // Apparently property binding doesn't work either for checkboxes or for bool variables
                    settings.runOnStartup = setting.Checked;
                    StartupSetup();
                    break;
                case "startMinimizedToolStripMenuItem":
                    settings.startMinimized = setting.Checked;
                    break;
                case "autoconnectToolStripMenuItem":
                    settings.autoconnect = setting.Checked;
                    break;
                case "checkUpdatesToolStripMenuItem":
                    settings.checkUpdates = setting.Checked;
                    if (setting.Checked)
                        CheckForUpdates();
                    break;
                case "allowAnalyticsToolStripMenuItem":
                    settings.analytics = setting.Checked;
                    Analytics.SetEnabledAsync(setting.Checked);
                    break;
                case "darkModeToolStripMenuItem":
                    settings.darkMode = setting.Checked;
                    ThemeSetup();
                    break;
                default:
                    throw new NotImplementedException(setting.Name);
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
            Process.Start((string)item.Tag);
        }

        /// <summary>
        /// Called when you press on a translator's or supporter's nickname.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenPersonsPage(object sender, EventArgs e)
        {
            var personItem = (ToolStripMenuItem)sender;
            var personTag = (ValueTuple<string, string>)personItem.Tag; // Item1 is type, Item2 is URL

            if (string.IsNullOrWhiteSpace(personTag.Item2))
                return;

            string personName = personItem.Text;

            if (personTag.Item1 == "supporter")
                personName = personName.Replace(" - ", "|").Split('|')[0]; // Doing this replacement thing just in case someone has "-" in their nickname

            Analytics.TrackEvent("Clicked on a " + personTag.Item1, new Dictionary<string, string> {
                { "Name", personName },
                { "URL", personTag.Item2 }
            });

            Process.Start(personTag.Item2); // Tags contain URLs
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
                if (ConnectionManager.State == ConnectionType.Disconnected)
                    Analytics.TrackEvent("Connected"); // Only send analytics if connect function was called from disconnected state

                buttonConnect.Enabled = false; // ...disable Connect button...
                buttonDisconnect.Enabled = true; // ...enable Disconnect button...
                trayMenuDisconnect.Enabled = true; // ...enable Disconnect button in tray menu...
                textBoxID.ReadOnly = true; // ...make the ID field read only...
                buttonUpdatePresence.Enabled = true; // ...enable Update Presence button...
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
            toolStripStatusLabelStatus.Text = Strings.statusDisconnected;

            textBoxID.BackColor = CurrentColors.BgTextFields;
            ConnectionManager.State = ConnectionType.Disconnected;

            restartTimer.Stop();

            client.Dispose();

            Analytics.TrackEvent("Disconnected");
        }

        /// <summary>
        /// Called on Validating event for all text fields except ID.
        /// </summary>
        private void LengthValidationFocus(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dynamic box = sender;

            if (box.Text.Length == 1 || !StringTools.WithinLength(box.Text, box.MaxLength))
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

            box.BackColor = (box.Text.Length != 1 && StringTools.WithinLength(box.Text, box.MaxLength)) ? CurrentColors.BgTextFields : CurrentColors.BgTextFieldsError;
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

            settings.timestamps = btn.TabIndex; // I mean... it's a great container for int values
            Utils.SaveSettings();

            dateTimePickerTimestamp.Enabled = settings.timestamps == 3;
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
                    var res = client.GetAsync($"https://discordapp.com/api/oauth2/applications/{settings.id}/assets").Result;

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
            btn.Text = string.Empty;
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(e.Graphics, res.GetString($"{btn.Name}.Text"), btn.Font, e.ClipRectangle, btn.Enabled ? btn.ForeColor : CurrentColors.TextInactive, flags);
        }

        /// <summary>
        /// Called when you press the Update Presence button.
        /// </summary>
        private void Update(object sender, EventArgs e)
        {
            Utils.SaveSettings();
            SetPresence();
        }
    }
}