using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiscordRPC;
using Timer = System.Timers.Timer;

namespace CustomRPC
{
    public partial class MainForm : Form
    {
        DiscordRpcClient client;
        Timer timer;
		
        bool wasTooltipShown = false;
		bool loading = true;
        Properties.Settings settings = Properties.Settings.Default;
        
        public MainForm()
        {
            InitializeComponent();

            runOnStartupToolStripMenuItem.Checked = settings.runOnStartup;
            startMinimizedToolStripMenuItem.Checked = settings.startMinimized;

            textBoxID.Text = settings.id;
            textBoxDetails.Text = settings.details;
            textBoxState.Text = settings.state;
            textBoxSmallKey.Text = settings.smallKey;
            textBoxSmallText.Text = settings.smallText;
            textBoxLargeKey.Text = settings.largeKey;
            textBoxLargeText.Text = settings.largeText;

            loading = false;

            if (!settings.startMinimized) Show();
            if (settings.id == "")
            {
                
                MessageBox.Show(this, @"To set this up:
1) Go to https://discordapp.com/developers/applications/
2) Create a new app
3) Add some more instructions
4) Profit!", "Welcome!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start("https://discordapp.com/developers/applications/");
                return;
            }
            else
            {
                if (Init())
                {
                    buttonConnect.Enabled = false;
                    buttonDisconnect.Enabled = true;
                    textBoxID.ReadOnly = true;
                }
            }
        }

        private bool Init()
        {
            if (settings.id == "")
            {
                MessageBox.Show(this, "No ID specified!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (client != null && !client.Disposed)
            {
                timer.Dispose();
                client.Dispose();
            }
            
            client = new DiscordRpcClient(settings.id);

            timer = new Timer(150);
            timer.Elapsed += (sender, args) => { client.Invoke(); };
            timer.Start();

            client.Initialize();

            SetPresence();
            return true;
        }

        void SetPresence()
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
        }

        private void MinimizeToTray(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
                
                if (!settings.startMinimized && !wasTooltipShown)
                {
                    trayIcon.ShowBalloonTip(500);
                    wasTooltipShown = true;
                }
            }
        }

        private void MaximizeFromTray(object sender, EventArgs e)
        {
            Show();
        }

        private void Reconnect(object sender, EventArgs e)
        {
            ChangePresence();
            if (Init())
            {
                buttonConnect.Enabled = false;
                buttonDisconnect.Enabled = true;
                textBoxID.ReadOnly = true;
            }
        }

        private void Quit(object sender, EventArgs e)
        {
            if (timer != null) timer.Dispose();
            if (client != null) client.Dispose();
            ChangePresence();
            Application.Exit();
        }

        private void SaveSettings(object sender, EventArgs e)
        {
            if (loading) return;

            settings.runOnStartup = runOnStartupToolStripMenuItem.Checked;
            settings.startMinimized = startMinimizedToolStripMenuItem.Checked;
            settings.Save();
        }

        private void ShowAbout(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Made by maximmax42\n© 2018", "About...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

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

        private void Update(object sender, EventArgs e)
        {
            ChangePresence();
            SetPresence();
        }

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

        private void Disconnect(object sender, EventArgs e)
        {
            buttonConnect.Enabled = true;
            buttonDisconnect.Enabled = false;
            textBoxID.ReadOnly = false;
            timer.Dispose();
            client.Dispose();
        }

        private void OpenDiscordSite(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (settings.id == "") return;

            System.Diagnostics.Process.Start("https://discordapp.com/developers/applications/" + settings.id + "/rich-presence/assets");
        }

        private void OnlyNumbers(object sender, EventArgs e)
        {
            textBoxID.ReadOnly = true;

            string newline = "";
            foreach (var symbol in textBoxID.Text)
            {
                if (char.IsDigit(symbol)) newline += symbol;
            }

            textBoxID.Text = newline;
            textBoxID.SelectionStart = textBoxID.Text.Length;
            textBoxID.SelectionLength = 0;
            textBoxID.ReadOnly = false;
        }
    }
}
