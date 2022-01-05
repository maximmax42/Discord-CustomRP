using System.Drawing;
using System.Windows.Forms;

namespace CustomRPC
{
    public partial class ErrorReportViewer : Form
    {
        public ErrorReportViewer(string stackTrace)
        {
            InitializeComponent();

            WinApi.UseImmersiveDarkMode(Handle);

            BackColor = richTextBoxReport.BackColor = CurrentColors.BgColor;
            ForeColor = richTextBoxReport.ForeColor = CurrentColors.TextColor;

            buttonOK.FlatStyle = Properties.Settings.Default.darkMode ? FlatStyle.Flat : FlatStyle.Standard;

            richTextBoxReport.Text = stackTrace;

            richTextBoxReport.Select(0, stackTrace.Split('\n')[0].Length);
            richTextBoxReport.SelectionFont = new Font(richTextBoxReport.Font.FontFamily, richTextBoxReport.Font.Size + 2, FontStyle.Bold);
            richTextBoxReport.DeselectAll();
        }
    }
}
