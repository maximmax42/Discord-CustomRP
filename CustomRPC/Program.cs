using System;
using System.Threading;
using System.Windows.Forms;

namespace CustomRPC
{
    static class Program
    {
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

            var mutex = new Mutex(true, mutexName, out bool createdNew);

            if (!createdNew)
            {
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new MainForm();
            Application.Run();

            GC.KeepAlive(mutex);
        }
    }
}
