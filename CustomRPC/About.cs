using System.Windows.Forms;

namespace CustomRPC
{
    public partial class About : Form
    {
        public About(string aboutLocalized)
        {
            InitializeComponent();

            Text = aboutLocalized;

            var versionSplit = Application.ProductVersion.Split('.');

            labelVersion.Text = "v" + versionSplit[0] + "." + versionSplit[1];
            if (versionSplit[2] != "0") labelVersion.Text += "." + versionSplit[2];
        }
    }
}
