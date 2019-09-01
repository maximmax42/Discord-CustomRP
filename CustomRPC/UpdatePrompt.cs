using System;
using System.Windows.Forms;

namespace CustomRPC
{
    public partial class UpdatePrompt : Form
    {
        public UpdatePrompt(Version current, Version latest, string changelog)
        {
            InitializeComponent();

            string currentStr = $"{current.Major}.{current.Minor}.{current.Build}";
            string latestStr = $"{latest.Major}.{latest.Minor}.{(latest.Build == -1 ? 0 : latest.Build)}";

            labelVersions.Text = currentStr + "\r\n" + latestStr;

            htmlPanelChangelog.Text = changelog;

            System.Media.SystemSounds.Beep.Play();
            Activate();
        }

        // Called when changing the state of the changebox
        private void ToggleUpdates(object sender, EventArgs e)
        {
            var settings = Properties.Settings.Default;

            settings.checkUpdates = !((CheckBox)sender).Checked;
            settings.Save();
        }
    }
}
