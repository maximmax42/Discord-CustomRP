using CommonMark;
using DiscordRPC;
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

        DateTime started = DateTime.UtcNow; // Timestamp of when the app started.

        bool wasTooltipShown = false; // When you minimize, tooltip shows once (that is if Start Minimized is disabled)
        bool loading = true; // To prevent some event handlers from executing while app is loading
        bool toAvoidRecursion = false; // ...This is stupid

        short connectionState = 0; // 0 - not connected/connecting, 1 - connected, 2 - error

        Timer restartTimer = new Timer(5000); // A timer for automatic restart on connection error

        Properties.Settings settings = Properties.Settings.Default; // Settings

        string linkPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\CustomRP.lnk"; // Autorun file link

        GitHubClient githubClient = new GitHubClient(new ProductHeaderValue("CustomRP")); // For getting updates
        Release latestRelease;

        System.Drawing.Color defaultColor = System.Drawing.Color.FromName("Window");
        System.Drawing.Color successColor = System.Drawing.Color.FromArgb(192, 255, 192);
        System.Drawing.Color errorColor = System.Drawing.Color.FromArgb(255, 192, 192);

        // Constructor of the form
        public MainForm()
        {
            InitializeComponent();

            // Setting up startup link for current user (enabled by default)
            StartupSetup();

            // Setting up a restart timer
            restartTimer.AutoReset = true;
            restartTimer.Elapsed += RestartTimer_Elapsed;

            // Setting up checkboxes because apparently property binding doesn't work
            runOnStartupToolStripMenuItem.Checked = settings.runOnStartup;
            startMinimizedToolStripMenuItem.Checked = settings.startMinimized;
            checkUpdatesToolStripMenuItem.Checked = settings.checkUpdates;

            // Checks the chosen language setting
            foreach (var toolStripItemObj in languageToolStripMenuItem.DropDownItems)
            {
                if (toolStripItemObj is ToolStripSeparator) continue;

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
            dateTimePickerTimestamp.Enabled = settings.timestamps == 3;

            // Change the date and time picker's format according to system's culture
            dateTimePickerTimestamp.CustomFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " + CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;

            // Change the earliest date user can choose according to user's timezone
            dateTimePickerTimestamp.MinDate = new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime();

            // If the app was launched for the first time (including since update), set the default time to current one
            if (settings.customTimestamp.CompareTo(new DateTime(1969, 1, 1, 0, 0, 0)) == 0)
                settings.customTimestamp = DateTime.Now;

            // Localize the header of the tooltip because Visual Studio can't do that for some reason
            toolTipInfo.ToolTipTitle = Strings.information;

            loading = false;

            if (settings.changedLanguage || !settings.startMinimized) Show(); // Starts minimized to tray by default, unless you just changed language

            settings.changedLanguage = false;

            if (settings.id != "" && settings.firstStart) // That means user has upgraded from older version without that flag
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
            else if (settings.id != "")
            {
                Connect();
            }

            if (settings.checkUpdates) CheckForUpdates();
        }

        // If user launches second instance, this activates already running one
        protected override void WndProc(ref Message message)
        {
            if (message.Msg == Program.WM_SHOWFIRSTINSTANCE)
            {
                MaximizeFromTray();
            }
            base.WndProc(ref message);
        }

        // Will be called every 5 seconds to try and reconnect
        private void RestartTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Connect();
        }

        // Checking updates
        private async void CheckForUpdates(bool verbose = false)
        {
            // Fetching latest release and getting its version
            try
            {
                latestRelease = await githubClient.Repository.Release.GetLatest("maximmax42", "Discord-CustomRP");
            }
            catch
            {
                // If there's no internet or Github is down, do nothing, unless it's a user requested update check
                if (verbose) MessageBox.Show(this, Strings.errorNoInternet, Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }

            string latestVersion = latestRelease.TagName;

            if (latestVersion == settings.ignoreVersion && !verbose)
            {
                return; // The user ignored this version; this gets ignored if the user requested the update check manually, maybe they changed their mind?
            }

            Version current = new Version(Application.ProductVersion.Substring(0, Application.ProductVersion.Length - 2)); // To not deal with revision number
            Version latest = new Version(latestVersion);

            if (current.CompareTo(latest) < 0) // If update is available...
            {
                var changelogBuilder = new System.Text.StringBuilder(); // ...build the changelog...

                var releases = await githubClient.Repository.Release.GetAll("maximmax42", "Discord-CustomRP"); // Get all Releases of the app
                foreach (var release in releases)
                {
                    Version releaseVer = new Version(release.TagName);
                    if (releaseVer.Build == -1) releaseVer = new Version(releaseVer.Major, releaseVer.Minor, 0); // Because 1.3 != 1.3.0

                    if (releaseVer.Equals(current)) break;

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
                    DownloadAndInstallUpdate();
                else if (messageBox == DialogResult.Ignore)
                    settings.ignoreVersion = latestVersion;

                checkUpdatesToolStripMenuItem.Checked = settings.checkUpdates;

                if (!settings.checkUpdates || messageBox == DialogResult.Ignore)
                    downloadUpdateToolStripMenuItem.Visible = false; // If user doesn't want update notifications, let's not bother them
            }
            else if (verbose) // If there's no update available and it was a user initiated update check, notify them about it
            {
                MessageBox.Show(this, Strings.noUpdatesFound, Strings.information, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        // Downloading and installing latest update from GitHub
        public async void DownloadAndInstallUpdate()
        {
            if (latestRelease == null) return; // Probably shouldn't happen, but just in case

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
                    Process.Start(exec);

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

                    break;
                }
            }
        }

        // Connecting to the Discord API
        private bool Init()
        {
            if (settings.id == "")
            {
                MessageBox.Show(Strings.errorNoID, Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }

            if (client != null && !client.IsDisposed)
            {
                // This stuff needs proper disposal
                client.Dispose();
            }

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
            connectionState = 1;
            Invoke(new MethodInvoker(() =>
            {
                textBoxID.BackColor = successColor;
                toolStripStatusLabelStatus.Text = Strings.statusConnected;
            }));

            restartTimer.Stop();
        }

        // Will be called if failed connecting (due to bad app id or anything else)
        private void ClientOnError(object sender, DiscordRPC.Message.ErrorMessage args)
        {
            connectionState = 2;
            Invoke(new MethodInvoker(() =>
            {
                textBoxID.BackColor = errorColor;
                toolStripStatusLabelStatus.Text = Strings.statusError;
            }));

            restartTimer.Start();
        }

        // Will be called if failed connecting (mostly due to Discord being closed)
        private void ClientOnConnFailed(object sender, DiscordRPC.Message.ConnectionFailedMessage args)
        {
            connectionState = 2;
            Invoke(new MethodInvoker(() =>
            {
                textBoxID.BackColor = errorColor;
                toolStripStatusLabelStatus.Text = Strings.statusConnectionFailed;
            }));

            restartTimer.Start();
        }

        // Sets up new presence from the settings
        private void SetPresence()
        {
            if (client == null) return;

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
                case 1: rp.Timestamps = new Timestamps(started); break;
                case 2: rp.Timestamps = new Timestamps(DateTime.UtcNow.Subtract(new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second))); break;
                case 3:
                    DateTime customTimestamp = dateTimePickerTimestamp.Value.ToUniversalTime();
                    rp.Timestamps = customTimestamp.CompareTo(DateTime.UtcNow) < 0 ? new Timestamps(customTimestamp) : new Timestamps(DateTime.UtcNow, customTimestamp);
                    break;
            }

            client.SetPresence(rp);
        }

        // Sets up the startup link for the app.
        private void StartupSetup()
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
            {
                File.Delete(linkPath);
            }
        }

        // Called when you close the main window with the X button
        private void MinimizeToTray(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing) // Checks if it was closed by user and not by system in a shutdown, for example
            {
                // Prevent closing and hide the window to tray instead
                e.Cancel = true;
                Hide();

                if (!settings.startMinimized && !wasTooltipShown)
                {
                    // Show a tooltip if it wasn't shown already and if the app doesn't start minimized 
                    trayIcon.ShowBalloonTip(500);
                    wasTooltipShown = true;
                }
            }
        }

        // Called when you double click the tray icon
        private void MaximizeFromTray(object sender, EventArgs e)
        {
            switch (connectionState) // Because invoking doesn't work while the form is hidden
            {
                case 0:
                    textBoxID.BackColor = defaultColor;
                    toolStripStatusLabelStatus.Text = Strings.statusDisconnected;
                    break;
                case 1:
                    textBoxID.BackColor = successColor;
                    toolStripStatusLabelStatus.Text = Strings.statusConnected;
                    break;
                case 2:
                    textBoxID.BackColor = errorColor;
                    toolStripStatusLabelStatus.Text = Strings.statusError;
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

        // Called when you press Load Preset button
        private void LoadPreset(object sender, EventArgs e)
        {
            var xs = new XmlSerializer(typeof(Preset)); // Using XML here because... why not? Settings are already saved in XML
            var presetFile = new OpenFileDialog()
            {
                Filter = "CustomRP Preset|*.crp"
            };

            if (presetFile.ShowDialog() != DialogResult.OK) return;

            var file = presetFile.OpenFile();

            var preset = (Preset)xs.Deserialize(file);

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

            file.Close();
        }

        // Called when you press Save Preset button
        private void SavePreset(object sender, EventArgs e)
        {
            var xs = new XmlSerializer(typeof(Preset));
            var presetFile = new SaveFileDialog()
            {
                Filter = "CustomRP Preset|*.crp"
            };

            if (presetFile.ShowDialog() != DialogResult.OK) return;

            var file = presetFile.OpenFile();

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

            file.Close();
        }

        // Called when you press Upload Assets button
        private void OpenDiscordSite(object sender, EventArgs e)
        {
            if (settings.id == "") return;

            Process.Start("https://discord.com/developers/applications/" + settings.id + "/rich-presence/assets");
        }

        // Called when you click File -> Quit or right-click on the tray icon and choose Quit
        private void Quit(object sender, EventArgs e)
        {
            if (client != null) client.Dispose();

            settings.Save();
            Application.Exit();
        }

        // Called when you press anything in Settings submenu of menu strip
        private void SaveSettings(object sender, EventArgs e)
        {
            if (loading) return;

            // Apparently property binding doesn't work either for checkboxes or for bool variables
            settings.runOnStartup = runOnStartupToolStripMenuItem.Checked;
            settings.startMinimized = startMinimizedToolStripMenuItem.Checked;
            settings.checkUpdates = checkUpdatesToolStripMenuItem.Checked;

            settings.Save();

            StartupSetup();
            if (settings.checkUpdates) CheckForUpdates();
        }

        // Called when you change the language
        private void ChangeLanguage(object sender, EventArgs e)
        {
            var lang = (ToolStripMenuItem)sender;

            settings.language = (string)lang.Tag;
            settings.changedLanguage = true;
            settings.Save();
            Program.AppMutex.Close();
            Application.Restart();
        }

        // Called when you press Open the Manual button
        private void OpenManual(object sender, EventArgs e)
        {
            Process.Start("https://github.com/maximmax42/Discord-CustomRP/wiki/Setting-Up");
        }

        // Called when you press GitHub Page button
        private void OpenGitHub(object sender, EventArgs e)
        {
            Process.Start("https://github.com/maximmax42/Discord-CustomRP");
        }

        // Called when you press Check for updates... button
        private void CheckForUpdates(object sender, EventArgs e)
        {
            CheckForUpdates(true);
        }

        // Called when you press About... button
        private void ShowAbout(object sender, EventArgs e)
        {
            new About().ShowDialog(this);
        }

        // Called when you press on a translator's nickname
        private void OpenTranslatorPage(object sender, EventArgs e)
        {
            var translator = (ToolStripMenuItem)sender;

            if (String.IsNullOrWhiteSpace((string)translator.Tag)) return;

            Process.Start((string)translator.Tag); // Tags contain URLs
        }

        // Called when you press Download Update button
        private void DownloadUpdate(object sender, EventArgs e)
        {
            DownloadAndInstallUpdate();
        }

        // Called when you input into the ID textbox
        // This is overcomplicated isn't it, but hey, at least it works with pasting as well!
        private void OnlyNumbers(object sender, EventArgs e)
        {
            if (toAvoidRecursion || textBoxID.Text == "") return;

            textBoxID.ReadOnly = true; // Not sure if I need it?
            toAvoidRecursion = true; // Just so this handler doesn't get called again on...

            int sel = textBoxID.SelectionStart; // Current cursor position
            int changed = 0;

            string newline = "";

            foreach (var symbol in textBoxID.Text)
            {
                if (char.IsDigit(symbol)) newline += symbol;
                else changed++;
            }

            textBoxID.Text = newline; // ...this line
            textBoxID.SelectionStart = sel - changed;
            textBoxID.SelectionLength = 0;

            textBoxID.ReadOnly = false;
            toAvoidRecursion = false;
        }

        // Called on Validating event for all text fields except ID
        private void LengthValidationFocus(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var box = (TextBox)sender;
            var length = System.Text.Encoding.UTF8.GetByteCount(box.Text);

            if (length > box.MaxLength)
            {
                e.Cancel = true;
                System.Media.SystemSounds.Beep.Play();
            }
        }

        // Called on TextChanged event for all text fields except ID
        private void LengthValidation(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            var length = System.Text.Encoding.UTF8.GetByteCount(box.Text);

            if (length > box.MaxLength)
            {
                box.BackColor = errorColor;
            }
            else
            {
                box.BackColor = defaultColor;
            }
        }

        // Called when a timestamp radiobutton changed
        private void TimestampsChanged(object sender, EventArgs e)
        {
            if (loading) return;

            RadioButton btn = (RadioButton)sender;

            if (!btn.Checked) return;

            settings.timestamps = btn.TabIndex; // I mean... it's a great container for int values
            settings.Save();

            dateTimePickerTimestamp.Enabled = settings.timestamps == 3;
        }

        // Called when you press the Connect button or right-click on the tray icon and choose Reconnect
        private void Connect(object sender, EventArgs e)
        {
            settings.Save();

            if (ModifierKeys == Keys.Control)
            {
                new PipeSelector().ShowDialog(this);
                return;
            }

            if (Init()) // If successfully connected...
            {
                buttonConnect.Enabled = false; // ...disable Connect button...
                buttonDisconnect.Enabled = true; // ...enable Disconnect button...
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
            buttonUpdatePresence.Enabled = false;
            textBoxID.ReadOnly = false;
            toolStripStatusLabelStatus.Text = Strings.statusDisconnected;

            textBoxID.BackColor = defaultColor;
            connectionState = 0;

            restartTimer.Stop();

            client.Dispose();
        }

        // Same but as a tidy function for using in code
        private void Disconnect()
        {
            Disconnect(null, null);
        }

        // Called when you press the Update Presence button
        private void Update(object sender, EventArgs e)
        {
            settings.Save();
            SetPresence();
        }
    }
}