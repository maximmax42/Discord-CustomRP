using System;
using System.Windows.Forms;

namespace CustomRPC
{
    public partial class UpdatePrompt : Form
    {
        public UpdatePrompt(Version current, Version latest, string changelog)
        {
            InitializeComponent();

            WinApi.UseImmersiveDarkMode(Handle);

            BackColor = htmlPanelChangelog.BackColor = CurrentColors.BgColor;
            ForeColor = CurrentColors.TextColor;

            if (ForeColor == System.Drawing.Color.White)
                htmlPanelChangelog.BaseStylesheet += "* {color: white;}";

            buttonSkipUpdate.FlatStyle = buttonNotNow.FlatStyle = buttonUpdate.FlatStyle = Properties.Settings.Default.darkMode ? FlatStyle.Flat : FlatStyle.Standard;

            string currentStr = VersionHelper.GetVersionString(current);
            string latestStr = VersionHelper.GetVersionString(latest);

            labelVersions.Text = currentStr + "\r\n" + latestStr;

            htmlPanelChangelog.Text = changelog;

            System.Media.SystemSounds.Beep.Play();
            Activate();
        }

        /// <summary>
        /// Called when changing the state of <see cref="checkBoxNeverNotify"/>.
        /// </summary>
        private void ToggleUpdates(object sender, EventArgs e)
        {
            var settings = Properties.Settings.Default;

            settings.checkUpdates = !((CheckBox)sender).Checked;
            settings.Save();
        }
    }
}
