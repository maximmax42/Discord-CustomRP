using System.Windows.Forms;

namespace CustomRPC
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();

            WinApi.UseImmersiveDarkMode(Handle);

            BackColor = CurrentColors.BgColor;
            ForeColor = CurrentColors.TextColor;

            buttonClose.FlatStyle = Properties.Settings.Default.darkMode ? FlatStyle.Flat : FlatStyle.Standard;

            labelVersion.Text = VersionHelper.GetVersionString(Application.ProductVersion);
#if DEBUG
            labelVersion.Text += " (DEV)";
#endif
        }
    }
}
