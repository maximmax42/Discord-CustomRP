using System.Windows.Forms;

namespace CustomRPC
{
    public partial class About : Form
    {
        public About(string aboutLocalized)
        {
            InitializeComponent();

            Text = aboutLocalized;
        }
    }
}
