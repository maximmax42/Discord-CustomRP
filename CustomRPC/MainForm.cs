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

    // A struct for handling preset importing/exporting
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

    public partial class MainForm : Form
    {
        DiscordRpcClient client; // RPC Client

        List<DButton> buttonsList = new List<DButton>(); // List of custom buttons

        bool loading = true; // To prevent some event handlers from executing while app is loading
        bool toAvoidRecursion = false; // ...This is stupid

        ConnectionState connectionState = new ConnectionState();

        Timer restartTimer = new Timer(10 * 1000); // A timer for automatic restart on connection error
        int restartAttempts = 30; // Limit the amount of restart tries
        int restartAttemptsLeft = -1;

        Properties.Settings settings = Properties.Settings.Default; // Settings

        GitHubClient githubClient = new GitHubClient(new ProductHeaderValue("CustomRP")); // For getting updates
        Release latestRelease;

        readonly DateTime timestampStarted = DateTime.UtcNow; // Timestamp of when the app started

        readonly string linkPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\CustomRP.lnk"; // Autorun file link

        readonly System.Drawing.Color defaultColor = System.Drawing.Color.FromName("Window");
        readonly System.Drawing.Color successColor = System.Drawing.Color.FromArgb(192, 255, 192);
        readonly System.Drawing.Color errorColor = System.Drawing.Color.FromArgb(255, 192, 192);

        // Constructor of the form
        public MainForm(string preset)
        {
            InitializeComponent();

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
                settings.Save();
            }

            if (settings.firstStart)
            {
                // Asking if the user wants the manual
                var messageBox = MessageBox.Show(this, Strings.firstTimeRunText, Strings.firstTimeRun, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                if (messageBox == DialogResult.Yes)
                    // Opens the setup manual
                    Process.Start("https://github.com/maximmax42/Discord-CustomRP/wiki/Setting-Up");

                settings.firstStart = false;
                settings.Save();
            }
            else if (settings.id != "" && ((settings.changedLanguage && settings.wasConnected) || (settings.autoconnect && !settings.changedLanguage)))
                Connect();

            CheckIfCrashed();

            if (settings.checkUpdates)
                CheckForUpdates();

            settings.changedLanguage = false;
        }

        // Handles communication between instances
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

        // Will be called 10 seconds after a failed connection to try and reconnect
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

        // Checks if the app has crashed in the previous session
        private async void CheckIfCrashed()
        {
            if (!await Crashes.HasCrashedInLastSessionAsync())
                return;

            var report = await Crashes.GetLastSessionCrashReportAsync();

            new ErrorReportViewer(report.StackTrace).ShowDialog();
        }

        // Helper class to get a proper version object
        private Version GetVersion(string version)
        {
            if (string.IsNullOrEmpty(version))
                throw new ArgumentNullException("version");

            var array = version.Split('.');

            if (array.Length < 2 || array.Length > 4)
                throw new ArgumentException($"Version has {array.Length} part(s)!");

            switch (array.Length)
            {
                case 2:
                    return new Version(version + ".0.0");
                case 3:
                    return new Version(version + ".0");
            }

            return new Version(version);
        }

        // Checking updates
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

            Version current = GetVersion(Application.ProductVersion);
            Version latest = GetVersion(latestStr);

            if (current.CompareTo(latest) < 0) // If update is available...
            {
                var changelogBuilder = new System.Text.StringBuilder(); // ...build the changelog...

                foreach (var release in releases)
                {
                    Version releaseVer = GetVersion(release.TagName);

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

        // Downloading and installing latest update from GitHub
        public async void DownloadAndInstallUpdate()
        {
            if (latestRelease == null)
                return; // Probably shouldn't happen, but just in case

            // Check whether the application is installed or used as a portable app
            int fileType = Application.StartupPath.EndsWith("Roaming\\CustomRP") ? 0 : 1; // 0 is exe, 1 is zip

            var wc = new WebClient();
            var exec = Path.GetTempPath() + latestRelease.Assets[fileType].Name;

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
                    break;
                }
            }
        }

        // Initializing connection to the Discord API
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

            client.Initialize();

            SetPresence();

            return true;
        }

        // Will be called if successfully connected and sent the presence payload
        private void ClientOnPresenceUpdate(object sender, DiscordRPC.Message.PresenceMessage args)
        {
            var presence = client.CurrentPresence;

            connectionState.State = ConnectionType.Connected;

            Invoke(new MethodInvoker(() =>
            {
                textBoxID.BackColor = successColor;
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

        // Will be called if failed connecting (due to bad app id or anything else)
        private void ClientOnError(object sender, DiscordRPC.Message.ErrorMessage args)
        {
            connectionState.State = ConnectionType.Error;

            Invoke(new MethodInvoker(() =>
            {
                if (buttonConnect.Enabled) // Ignore if the user disconnected before connection was established
                    return;

                textBoxID.BackColor = errorColor;
                toolStripStatusLabelStatus.Text = Strings.statusError;
            }));

            if (connectionState.HasChanged()) // Ignore repeated calls caused by auto reconnect
                Analytics.TrackEvent("Connection error");

            restartTimer.Start();
        }

        // Will be called if failed connecting (mostly due to Discord being closed)
        private void ClientOnConnFailed(object sender, DiscordRPC.Message.ConnectionFailedMessage args)
        {
            connectionState.State = ConnectionType.Error;

            Invoke(new MethodInvoker(() =>
            {
                if (buttonConnect.Enabled) // Ignore if the user disconnected before connection was established
                    return;

                textBoxID.BackColor = errorColor;
                toolStripStatusLabelStatus.Text = Strings.statusConnectionFailed;
            }));

            if (connectionState.HasChanged()) // Ignore repeated calls caused by auto reconnect
                Analytics.TrackEvent("Connection failed");

            restartTimer.Start();
        }

        // Sets up new presence from the settings
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
                    ID = (settings.partySize != 0) ? "CustomRP" : "",
                    Size = (int)settings.partySize,
                    Max = (int)settings.partyMax
                },
            };

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

        // Sets up the startup link for the app
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

        // Called when you drag a file into app's window
        private void DragDropEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        // Called upon dropping a file
        private void DragDropHandler(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is string[] files && files.Length > 0)
                LoadPreset(files[0]); // If multiple files are passed, only the first one gets imported
        }

        // Called when you close the main window with the X button
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

        // Called when you double click the tray icon
        private void MaximizeFromTray(object sender, EventArgs e)
        {
            switch (connectionState.State) // Because invoking doesn't work while the form is hidden
            {
                case ConnectionType.Disconnected:
                    textBoxID.BackColor = defaultColor;
                    toolStripStatusLabelStatus.Text = Strings.statusDisconnected;
                    break;
                case ConnectionType.Connected:
                    textBoxID.BackColor = successColor;
                    toolStripStatusLabelStatus.Text = Strings.statusConnected;
                    break;
                case ConnectionType.Error:
                    textBoxID.BackColor = errorColor;
                    toolStripStatusLabelStatus.Text = Strings.statusError;
                    break;
                case ConnectionType.Unknown: // This should never happen, but just in case
                    textBoxID.BackColor = System.Drawing.Color.FromArgb(69420);
                    toolStripStatusLabelStatus.Text = "pipis";
                    Analytics.TrackEvent("You've set ConnectionState.State to ConnectionType.Unknown");
                    break;
            }

            Show();
            Activate();
        }

        // Same but as a function to use in code
        private void MaximizeFromTray()
        {
            MaximizeFromTray(null, null);
        }

        // Base function for loading a preset
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
                settings.Save();

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

        // Called when you press Load Preset button
        private void LoadPreset(object sender, EventArgs e)
        {
            var presetFile = new OpenFileDialog()
            {
                Filter = "CustomRP Preset|*.crp"
            };

            if (presetFile.ShowDialog() != DialogResult.OK)
                return;

            LoadPreset(presetFile.OpenFile());
        }

        // Same function but for use in code
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

        // Called when you press Save Preset button
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

        // Called when you press Upload Assets button
        private void OpenDiscordSite(object sender, EventArgs e)
        {
            if (settings.id == "")
                return;

            Process.Start("https://discord.com/developers/applications/" + settings.id + "/rich-presence/assets");
        }

        // Called when you click File -> Quit or right-click on the tray icon and choose Quit
        private void Quit(object sender, EventArgs e)
        {
            if (client != null)
                client.Dispose();

            settings.Save();
            Application.Exit();
        }

        // Called when you press anything in Settings submenu of menu strip
        private void SaveSettings(object sender, EventArgs e)
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
                default:
                    throw new NotImplementedException(setting.Name);
            }

            settings.Save();
        }

        // Called when you change the language
        private void ChangeLanguage(object sender, EventArgs e)
        {
            var lang = (ToolStripMenuItem)sender;

            settings.language = (string)lang.Tag;
            settings.changedLanguage = true;
            settings.wasConnected = buttonDisconnect.Enabled;
            settings.Save();
            Program.AppMutex.Close();
            Application.Restart();
        }

        // Called when you press on menu items that open websites
        private void OpenSite(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            Process.Start((string)item.Tag);
        }

        // Called when you press on a translator's nickname
        private void OpenTranslatorPage(object sender, EventArgs e)
        {
            var translator = (ToolStripMenuItem)sender;

            if (string.IsNullOrWhiteSpace((string)translator.Tag))
                return;

            Analytics.TrackEvent("Clicked on a translator", new Dictionary<string, string> {
                { "Name", translator.Text },
                { "URL", (string)translator.Tag }
            });

            Process.Start((string)translator.Tag); // Tags contain URLs
        }

        // Called when you press Check for updates... button
        private void CheckForUpdates(object sender, EventArgs e)
        {
            CheckForUpdates(true);
        }

        // Called when you press About... button
        private void ShowAbout(object sender, EventArgs e)
        {
            Analytics.TrackEvent("Opened about window");
            new About().ShowDialog(this);
        }

        // Called when you press Download Update button
        private void DownloadUpdate(object sender, EventArgs e)
        {
            downloadUpdateToolStripMenuItem.Enabled = false;
            DownloadAndInstallUpdate();
        }

        // Called when you input into the ID textbox
        // This is overcomplicated isn't it, but hey, at least it works with pasting as well!
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

            textBoxID.Text = newline; // ...this line
            textBoxID.SelectionStart = sel - changed;
            textBoxID.SelectionLength = 0;

            textBoxID.ReadOnly = false;
            toAvoidRecursion = false;
        }

        // Called when you press the Connect button or right-click on the tray icon and choose Reconnect
        private void Connect(object sender, EventArgs e)
        {
            settings.Save();
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

            if (Init()) // If successfully initialized...
            {
                if (connectionState.State == ConnectionType.Disconnected)
                    Analytics.TrackEvent("Connected"); // Only send analytics if connect function was called from disconnected state

                buttonConnect.Enabled = false; // ...disable Connect button...
                buttonDisconnect.Enabled = true; // ...enable Disconnect button...
                trayMenuDisconnect.Enabled = true; // ...enable Disconnect button in tray menu...
                textBoxID.ReadOnly = true; // ...make the ID field read only...
                buttonUpdatePresence.Enabled = true; // ...enable Update Presence button...
                toolStripStatusLabelStatus.Text = Strings.statusConnecting; // and update the connection status label
            }
        }

        // Same but as a tidy function for using in code
        private void Connect()
        {
            Connect(null, null);
        }

        // Called when you press the Disconnect button
        private void Disconnect(object sender, EventArgs e)
        {
            buttonConnect.Enabled = true;
            buttonDisconnect.Enabled = false;
            trayMenuDisconnect.Enabled = false;
            buttonUpdatePresence.Enabled = false;
            textBoxID.ReadOnly = false;
            toolStripStatusLabelStatus.Text = Strings.statusDisconnected;

            textBoxID.BackColor = defaultColor;
            connectionState.State = ConnectionType.Disconnected;

            restartTimer.Stop();

            client.Dispose();

            Analytics.TrackEvent("Disconnected");
        }

        // Same but as a tidy function for using in code
        private void Disconnect()
        {
            Disconnect(null, null);
        }

        // Called on Validating event for all text fields except ID
        private void LengthValidationFocus(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var box = (TextBox)sender;

            if (!StringTools.WithinLength(box.Text, box.MaxLength))
            {
                e.Cancel = true;
                System.Media.SystemSounds.Beep.Play();
            }
        }

        // Called on TextChanged event for all text fields except ID
        private void LengthValidation(object sender, EventArgs e)
        {
            var box = (TextBox)sender;

            box.BackColor = StringTools.WithinLength(box.Text, box.MaxLength) ? defaultColor : errorColor;
        }

        // Called on Validating event to validate party size values
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

        // Called when a timestamp radiobutton changed
        private void TimestampsChanged(object sender, EventArgs e)
        {
            if (loading)
                return;

            RadioButton btn = (RadioButton)sender;

            if (!btn.Checked)
                return;

            settings.timestamps = btn.TabIndex; // I mean... it's a great container for int values
            settings.Save();

            dateTimePickerTimestamp.Enabled = settings.timestamps == 3;
        }

        // Called when you press the Update Presence button
        private void Update(object sender, EventArgs e)
        {
            settings.Save();
            SetPresence();
        }
    }
}