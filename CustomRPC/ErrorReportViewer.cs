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
    public partial class ErrorReportViewer : Form
    {
        public ErrorReportViewer(string stackTrace)
        {
            InitializeComponent();

            richTextBoxReport.Text = stackTrace;

            richTextBoxReport.Select(0, stackTrace.Split('\n')[0].Length);
            richTextBoxReport.SelectionFont = new Font(richTextBoxReport.Font.FontFamily, richTextBoxReport.Font.Size + 2, FontStyle.Bold);
            richTextBoxReport.DeselectAll();
        }
    }
}
