using DiscordRPC;
using System;
using System.IO;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace CustomRPC
{
    public partial class MainForm : Form
    {
        DiscordRpcClient client; // RPC Client
        Timer timer; // Timer for invoking, required

        DateTime started = DateTime.UtcNow; // Timestamp of when the app started.

        bool wasTooltipShown = false; // When you minimize, tooltip shows once (that is if Start Minimized is disabled)
        bool loading = true; // To prevent some event handlers from executing while app is loading
        bool toAvoidRecursion = false; // ...This is stupid

        Properties.Settings settings = Properties.Settings.Default; // Settings

        string linkPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\CustomRP.lnk"; // Autorun file link

        string latestVersion; // For checking updates
        string currentVersion;

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

            loading = false;

            if (!settings.startMinimized) Show(); // Starts minimized to tray by default

            if (settings.id == "")
            {
                // Opens the setup manual
                System.Diagnostics.Process.Start("https://github.com/maximmax42/Discord-CustomRP/wiki/Setting-Up");
            }
            else
            {
                if (Init()) // If successfully connected...
                {
                    buttonConnect.Enabled = false; // ...disable Connect button...
                    buttonDisconnect.Enabled = true; // ...enable Disconnect button...
                    textBoxID.ReadOnly = true; // ...and make the ID field read only
                }
            }

            currentVersion = Application.ProductVersion.Substring(0, Application.ProductVersion.Length - 4);
            latestVersion = new System.Net.WebClient().DownloadString("https://raw.githubusercontent.com/maximmax42/Discord-CustomRP/master/version").Trim();

            if (currentVersion != latestVersion) // If update is available...
            {
                updateAvailableToolStripMenuItem.Visible = true; // ...activate the "Download update" button...
                Show(); // ...make sure the app window is shown if it was minimized...
                Activate(); // ...make it the active window...
                MessageBox.Show(Strings.updateAvailableText, Strings.updateAvailable, MessageBoxButtons.OK, MessageBoxIcon.Information); // ...and show a message box telling there's an update
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

            if (client != null && !client.Disposed)
            {
                // This stuff needs proper disposal
                timer.Dispose();
                client.Dispose();
            }

            client = new DiscordRpcClient(settings.id); // Assigning the ID

            // Not sure why it's needed, but it is
            timer = new Timer(150);
            timer.Elapsed += (sender, args) => { client.Invoke(); };
            timer.Start();

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

        // Called when you click File -> Quit or right-click on the tray icon and choose Quit
        private void Quit(object sender, EventArgs e)
        {
            if (timer != null) timer.Dispose();
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
            settings.Save();
            StartupSetup();
        }

        // Called when you press About... in menu strip
        private void ShowAbout(object sender, EventArgs e)
        {
            new About(aboutToolStripMenuItem.Text).ShowDialog(this);
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
                textBoxID.ReadOnly = true;
            }
        }

        // Called when you press the Disconnect button
        private void Disconnect(object sender, EventArgs e)
        {
            buttonConnect.Enabled = true;
            buttonDisconnect.Enabled = false;
            textBoxID.ReadOnly = false;

            timer.Dispose();
            client.Dispose();
        }

        // Called when you press the Update Presence button
        private void Update(object sender, EventArgs e)
        {
            ChangePresence();
            SetPresence();
        }

        // Called when you press Upload Assets button
        private void OpenDiscordSite(object sender, EventArgs e)
        {
            if (settings.id == "") return;

            System.Diagnostics.Process.Start("https://discordapp.com/developers/applications/" + settings.id + "/rich-presence/assets");
        }

        // Called when you press Update available! button
        private void DownloadUpdate(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/maximmax42/Discord-CustomRP/releases/tag/" + latestVersion);
        }
    }
}