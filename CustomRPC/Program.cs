using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CustomRPC
{
    /// <summary>
    /// Native Windows functions.
    /// </summary>
    /// <remarks>
    /// Something adapted from <see href="https://www.codeproject.com/Articles/32908/C-Single-Instance-App-With-the-Ability-To-Restore"/>, something from ShareX.
    /// </remarks>
    static public class WinApi
    {
        [DllImport("user32")]
        public static extern int RegisterWindowMessage(string message);

        [DllImport("user32")]
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        private const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        public static bool UseImmersiveDarkMode(IntPtr handle)
        {
            if (IsWindows10OrGreater(17763))
            {
                var attribute = DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
                if (IsWindows10OrGreater(18985))
                {
                    attribute = DWMWA_USE_IMMERSIVE_DARK_MODE;
                }

                int useImmersiveDarkMode = Properties.Settings.Default.darkMode ? 1 : 0;
                return DwmSetWindowAttribute(handle, (int)attribute, ref useImmersiveDarkMode, sizeof(int)) == 0;
            }

            return false;
        }

        private static bool IsWindows10OrGreater(int build = -1)
        {
            return Environment.OSVersion.Version.Major >= 10 && Environment.OSVersion.Version.Build >= build;
        }
    }

    static class Program
    {
        public static Mutex AppMutex;

        public static int WM_SHOWFIRSTINSTANCE;
        public static int WM_IMPORTPRESET;

        /// <summary>
        /// Main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
#if DEBUG
            string mutexName = "CustomRP dev";
#else
            string mutexName = "CustomRP";
#endif
            WM_SHOWFIRSTINSTANCE = WinApi.RegisterWindowMessage("WM_SHOWFIRSTINSTANCE|" + mutexName);
            WM_IMPORTPRESET = WinApi.RegisterWindowMessage("WM_IMPORTPRESET|" + mutexName);

            AppMutex = new Mutex(true, mutexName, out bool createdNew);

            if (!createdNew)
            {
                WinApi.PostMessage(new IntPtr(0xffff), WM_SHOWFIRSTINSTANCE, IntPtr.Zero, IntPtr.Zero);

                if (args.Length > 0)
                {
                    try
                    {
                        File.Copy(args[0], Path.GetTempPath() + "preset.crp", true);
                        WinApi.PostMessage(new IntPtr(0xffff), WM_IMPORTPRESET, IntPtr.Zero, IntPtr.Zero);
                    }
                    catch
                    {
                        MessageBox.Show(Strings.errorInvalidPresetFile, Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                return;
            }

            var settings = Properties.Settings.Default;

            string culture = "auto";

            try
            {
                // In case the settings file goes corrupt, trying to read any property will throw an exception.
                culture = settings.language;
            }
            catch (System.Configuration.ConfigurationErrorsException e)
            {
                var result = MessageBox.Show(Strings.errorCorruptSettings, Strings.error, MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (result == DialogResult.Yes)
                {
                    // I can't just do settings.Reset() since it will throw the same exception, so instead I have to delete the config file.
                    string filename = ((System.Configuration.ConfigurationErrorsException)e.InnerException).Filename;
                    File.Delete(filename);
                    AppMutex.Close();
                    Application.Restart();
                }

                return;
            }

            if (culture != "auto")
                CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo(culture);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);

            try
            {
                if (!settings.analyticsAskedConsent) // First time launching the app since the analytics integration
                {
                    var result = MessageBox.Show(Strings.askAnalyticsConsent, Strings.information, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    var allowAnalytics = result == DialogResult.Yes;

                    Analytics.SetEnabledAsync(allowAnalytics);
                    settings.analytics = allowAnalytics;
                    settings.analyticsAskedConsent = true;
                    settings.Save();
                }

                // Analytics and crashes
                Crashes.ShouldProcessErrorReport = (ErrorReport report) =>
                {
                    if (report.StackTrace.StartsWith("Microsoft.AppCenter.Crashes.TestCrashException"))
                        return false;

                    return true;
                };

                Crashes.GetErrorAttachments = (ErrorReport report) =>
                {
                    StringBuilder result = new StringBuilder();

                    foreach (System.Configuration.SettingsPropertyValue setting in settings.PropertyValues)
                    {
                        if (setting.Name == "id")
                            continue;

                        result.AppendLine(setting.Name + " = " + setting.SerializedValue);
                    }

                    return new ErrorAttachmentLog[]
                    {
                    ErrorAttachmentLog.AttachmentWithText(result.ToString(), "settings.txt")
                    };
                };

                AppCenter.SetCountryCode(RegionInfo.CurrentRegion.TwoLetterISORegionName);
                AppCenter.Start("141506f2-5a6b-46c5-a70e-693831ee131a", typeof(Analytics), typeof(Crashes));

                IntPtr _ = new MainForm(args.Length > 0 ? args[0] : null).Handle; // Terrible, yet allows to fully initialize the form without showing it first
                Application.Run();
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;

                if (ex is FileNotFoundException && ex.Message.Contains("Version=") || ex is FileLoadException)
                {
                    var result = MessageBox.Show($"{ex.Message}\r\n\r\n{string.Format(Strings.errorLoadingAssembly, Application.StartupPath)}", Strings.error, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                    if (result == DialogResult.Retry)
                    {
                        AppMutex.Close();
                        Application.Restart();
                    }
                }
                else
                    throw;
            }
            finally
            {
                settings.Save();
            }

            GC.KeepAlive(AppMutex);
        }
    }
}