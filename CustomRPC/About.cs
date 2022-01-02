using System.Windows.Forms;

namespace CustomRPC
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();

            labelVersion.Text = VersionHelper.GetVersionString(Application.ProductVersion);
#if DEBUG
            labelVersion.Text += " (DEV)";
#endif
        }
    }
}
