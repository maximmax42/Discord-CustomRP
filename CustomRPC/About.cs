using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
