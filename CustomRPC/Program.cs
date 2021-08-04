using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace CustomRPC
{
    // https://www.codeproject.com/Articles/32908/C-Single-Instance-App-With-the-Ability-To-Restore
    static public class WinApi
    {
        [DllImport("user32")]
        public static extern int RegisterWindowMessage(string message);

        [DllImport("user32")]
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
    }

    static class Program
    {
        public static Mutex AppMutex;

        public static int WM_SHOWFIRSTINSTANCE;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if DEBUG
            string mutexName = "CustomRP dev";
#else
            string mutexName = "CustomRP";
#endif
            WM_SHOWFIRSTINSTANCE = WinApi.RegisterWindowMessage("WM_SHOWFIRSTINSTANCE|" + mutexName);
            AppMutex = new Mutex(true, mutexName, out bool createdNew);

            if (!createdNew)
            {
                WinApi.PostMessage(new IntPtr(0xffff), WM_SHOWFIRSTINSTANCE, IntPtr.Zero, IntPtr.Zero);
                return;
            }

            string culture = Properties.Settings.Default.language;
            if (culture != "auto") CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo(culture);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new MainForm();
            Application.Run();

            GC.KeepAlive(AppMutex);
        }
    }
}