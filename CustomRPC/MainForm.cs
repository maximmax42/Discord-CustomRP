using DiscordRPC;
using Octokit;
using System;
using System.IO;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;

namespace CustomRPC
{
    public partial class MainForm : Form
    {
        DiscordRpcClient client; // RPC Client

        DateTime started = DateTime.UtcNow; // Timestamp of when the app started.

        bool wasTooltipShown = false; // When you minimize, tooltip shows once (that is if Start Minimized is disabled)
        bool loading = true; // To prevent some event handlers from executing while app is loading
        bool toAvoidRecursion = false; // ...This is stupid

        Properties.Settings settings = Properties.Settings.Default; // Settings

        string linkPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\CustomRP.lnk"; // Autorun file link

        GitHubClient githubClient = new GitHubClient(new ProductHeaderValue("CustomRP")); // For getting updates

        // Constructor of the form
        public MainForm()
        {
            InitializeComponent();

            // Setting up startup link for current user (enabled by default)
            StartupSetup();

            #region I don't wanna see this
            // There was a better way, and I only find that out 4-5 days in.
            // Eff it, not gonna remake it out of principle.

            // Displays currently saved settings in the menu strip
            runOnStartupToolStripMenuItem.Checked = settings.runOnStartup;
            startMinimizedToolStripMenuItem.Checked = settings.startMinimized;
            checkUpdatesToolStripMenuItem.Checked = settings.checkUpdates;

            // Fills out fields
            textBoxID.Text = settings.id;
            textBoxDetails.Text = settings.details;
            textBoxState.Text = settings.state;
            textBoxSmallKey.Text = settings.smallKey;
            textBoxSmallText.Text = settings.smallText;
            textBoxLargeKey.Text = settings.largeKey;
            textBoxLargeText.Text = settings.largeText;

            #endregion

            // Checks the needed timestamp radiobuttons
            switch (settings.timestamps)
            {
                case 0: radioButtonNone.Checked = true; break;
                case 1: radioButtonStartTime.Checked = true; break;
                case 2: radioButtonLocalTime.Checked = true; break;
            }

            // Localize the header of the tooltip because Visual Studio can't do that for some reason
            toolTipInfo.ToolTipTitle = Strings.information;

            loading = false;

            if (!settings.startMinimized) Show(); // Starts minimized to tray by default

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
                    System.Diagnostics.Process.Start("https://github.com/maximmax42/Discord-CustomRP/wiki/Setting-Up");

                settings.firstStart = false;
                settings.Save();
            }
            else if (settings.id != "")
            {
                if (Init()) // If successfully connected...
                {
                    buttonConnect.Enabled = false; // ...disable Connect button...
                    buttonDisconnect.Enabled = true; // ...enable Disconnect button...
                    textBoxID.ReadOnly = true; // ...make the ID field read only...
                    buttonUpdatePresence.Enabled = true; // ...and enable Update Presence button
                }
            }

            if (settings.checkUpdates) CheckForUpdates();
        }

        // Checking updates
        private async void CheckForUpdates()
        {
            // Fetching latest release and getting its version
            var latestRel = await githubClient.Repository.Release.GetLatest("maximmax42", "Discord-CustomRP");
            string latestVersion = latestRel.TagName;

            if (latestVersion == settings.ignoreVersion) return; // The user ignored this version

            Version current = new Version(Application.ProductVersion.Substring(0, Application.ProductVersion.Length - 2)); // To not deal with revision number
            Version latest = new Version(latestVersion);

            if (current.CompareTo(latest) < 0) // If update is available...
            {
                var changelogBuilder = new System.Text.StringBuilder();

                var releases = await githubClient.Repository.Release.GetAll("maximmax42", "Discord-CustomRP"); // ...get all Releases of the app
                foreach (var release in releases)
                {
                    Version releaseVer = new Version(release.TagName);
                    if (releaseVer.Build == -1) releaseVer = new Version(releaseVer.Major, releaseVer.Minor, 0); // Because 1.3 != 1.3.0

                    if (releaseVer.Equals(current)) break;

                    var releaseBody = release.Body.Trim().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                    changelogBuilder.Append("<h3>" + release.Name + "</h3><ul>");

                    for (int i = 1; i < releaseBody.Length - 2; i++)
                    {
                        changelogBuilder.Append("<li>" + releaseBody[i].Substring(2) + "</li>");
                    }

                    changelogBuilder.Append("</ul>");
                }

                string changelog = changelogBuilder.ToString();

                updateAvailableToolStripMenuItem.Visible = true; // ...activate the "Download update" button...
                Show(); // ...make sure the app window is shown if it was minimized...

                var messageBox = new UpdatePrompt(current, latest, changelog).ShowDialog(); // ...and show a dialog box telling there's an update

                if (messageBox == DialogResult.Yes)
                    System.Diagnostics.Process.Start("https://github.com/maximmax42/Discord-CustomRP/releases/tag/" + latestVersion);
                else if (messageBox == DialogResult.Ignore)
                    settings.ignoreVersion = latestVersion;

                checkUpdatesToolStripMenuItem.Checked = settings.checkUpdates;

                if (!settings.checkUpdates || messageBox == DialogResult.Ignore)
                    updateAvailableToolStripMenuItem.Visible = false; // If user doesn't want update notifications, let's not bother them
            }
        }

        // Connecting to the Discord API
        private bool Init()
        {
            if (settings.id == "")
            {
                MessageBox.Show(Strings.errorNoID, Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (client != null && !client.IsDisposed)
            {
                // This stuff needs proper disposal
                client.Dispose();
            }

            client = new DiscordRpcClient(settings.id); // Assigning the ID

            client.Initialize();

            SetPresence();
            return true;
        }

        // Sets up new presence from the settings
        private void SetPresence()
        {
            if (client == null) return;

            client.SetPresence(new RichPresence()
            {
                Details = settings.details,
                State = settings.state,
                Assets = new Assets()
                {
                    SmallImageKey = settings.smallKey,
                    SmallImageText = settings.smallText,
                    LargeImageKey = settings.largeKey,
                    LargeImageText = settings.largeText,
                }
            });

            switch (settings.timestamps)
            {
                case 0: break;
                case 1: client.UpdateStartTime(started); break;
                case 2: client.UpdateStartTime(DateTime.UtcNow.Subtract(new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second))); break;
            }
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
            Show();
            Activate();
        }

        // Called when you press Upload Assets button
        private void OpenDiscordSite(object sender, EventArgs e)
        {
            if (settings.id == "") return;

            System.Diagnostics.Process.Start("https://discordapp.com/developers/applications/" + settings.id + "/rich-presence/assets");
        }

        // Called when you click File -> Quit or right-click on the tray icon and choose Quit
        private void Quit(object sender, EventArgs e)
        {
            if (client != null) client.Dispose();

            ChangePresence();
            Application.Exit();
        }

        // Called when you press anything in Settings submenu of menu strip
        private void SaveSettings(object sender, EventArgs e)
        {
            if (loading) return;

            settings.runOnStartup = runOnStartupToolStripMenuItem.Checked;
            settings.startMinimized = startMinimizedToolStripMenuItem.Checked;
            settings.checkUpdates = checkUpdatesToolStripMenuItem.Checked;

            settings.Save();

            StartupSetup();
            if (settings.checkUpdates) CheckForUpdates();
        }

        // Called when you press Open the Manual button
        private void OpenManual(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/maximmax42/Discord-CustomRP/wiki/Setting-Up");
        }

        // Called when you press GitHub Page button
        private void OpenGitHub(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/maximmax42/Discord-CustomRP");
        }

        // Called when you press About... button
        private void ShowAbout(object sender, EventArgs e)
        {
            new About(aboutToolStripMenuItem.Text).ShowDialog(this);
        }

        // Called when you press on a translator's nickname
        private void OpenTranslatorPage(object sender, EventArgs e)
        {
            var translator = (ToolStripMenuItem)sender;

            System.Diagnostics.Process.Start((string)translator.Tag);
        }

        // Called when you press Download Update button
        private void DownloadUpdate(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/maximmax42/Discord-CustomRP/releases/latest");
        }

        // Saves all the fields to settings
        private void ChangePresence()
        {
            settings.id = textBoxID.Text;
            settings.details = textBoxDetails.Text;
            settings.state = textBoxState.Text;
            settings.smallKey = textBoxSmallKey.Text;
            settings.smallText = textBoxSmallText.Text;
            settings.largeKey = textBoxLargeKey.Text;
            settings.largeText = textBoxLargeText.Text;
            settings.Save();
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

        // Called when a timestamp radiobutton changed
        private void TimestampsChanged(object sender, EventArgs e)
        {
            if (loading) return;

            RadioButton btn = (RadioButton)sender;

            if (!btn.Checked) return;

            settings.timestamps = btn.TabIndex; // I mean... it's a great container for int values
            settings.Save();
        }

        // Called when you press the Connect button or right-click on the tray icon and choose Reconnect
        private void Connect(object sender, EventArgs e)
        {
            ChangePresence();
            if (Init())
            {
                buttonConnect.Enabled = false;
                buttonDisconnect.Enabled = true;
                buttonUpdatePresence.Enabled = true;
                textBoxID.ReadOnly = true;
            }
        }

        // Called when you press the Disconnect button
        private void Disconnect(object sender, EventArgs e)
        {
            buttonConnect.Enabled = true;
            buttonDisconnect.Enabled = false;
            buttonUpdatePresence.Enabled = false;
            textBoxID.ReadOnly = false;

            client.Dispose();
        }

        // Called when you press the Update Presence button
        private void Update(object sender, EventArgs e)
        {
            ChangePresence();
            SetPresence();
        }
    }
}