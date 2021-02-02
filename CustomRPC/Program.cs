using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace CustomRPC
{
    static class Program
    {
        public static Mutex AppMutex;

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

            AppMutex = new Mutex(true, mutexName, out bool createdNew);

            if (!createdNew)
            {
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
