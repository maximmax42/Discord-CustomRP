using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CustomRPC
{
    static class Program
    {
        /// <summary>
        /// A mutex for the app. Prevents users from opening more than one instance of the app.
        /// </summary>
        public static Mutex AppMutex { get; private set; }
        /// <summary>
        /// Path to IPC data file.
        /// </summary>
        public static string IPCPath { get; private set; }
        /// <summary>
        /// <see langword="True"/> if the user started the app with the --second-instance command line argument.
        /// </summary>
        public static bool IsSecondInstance { get; private set; }
        /// <summary>
        /// Window message responsible for opening the already working instance in case user opens another one.
        /// </summary>
        public static int WM_SHOWFIRSTINSTANCE { get; private set; }
        /// <summary>
        /// Window message responsible for opening the already working instance and pushing a preset to import in it.
        /// </summary>
        public static int WM_IMPORTPRESET { get; private set; }

        /// <summary>
        /// Main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string presetFile = null;
#if DEBUG
            string mutexName = "CustomRP dev";
#else
            string mutexName = "CustomRP";
#endif
            bool isSilent = false;

            if (args.Length > 0 && (args[0] == "--help" || args[0] == "-?"))
            {
                var helpText = new StringBuilder();
                helpText.AppendLine("Usage: CustomRP.exe [options] [preset file path]");
                helpText.AppendLine();
                helpText.AppendLine("List of options:");
                helpText.AppendLine("-2, --second-instance: open as second instance");
                helpText.AppendLine("-s, --silent-import: silent preset import");
                helpText.AppendLine("-?, --help: shows this help text");
                helpText.AppendLine();
                helpText.AppendLine("Option(s) and file path(s) can be included in any order. Including more than one file path will result in the last valid one being used.");
                MessageBox.Show(helpText.ToString(), Application.ProductName);
                return;
            }

            if (args.Length > 0 && args[0] == "uninstall")
            {
                if (MessageBox.Show(Strings.deleteSettings, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    string settingsPath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
                    settingsPath = Path.GetFullPath(Path.Combine(settingsPath, @"..\.."));
                    Directory.Delete(settingsPath, true);
                }
                
                return;
            }

            foreach (string arg in args.Distinct())
            {
                if (arg == "--second-instance" || arg == "-2")
                {
                    if (IsSecondInstance)
                        continue;

                    IsSecondInstance = true;
                    mutexName += " 2";
                }
                else if (arg == "--silent-import" || arg == "-s")
                    isSilent = true;
                else if (File.Exists(arg))
                    presetFile = arg;
            }

            WM_SHOWFIRSTINSTANCE = WinApi.RegisterWindowMessage("WM_SHOWFIRSTINSTANCE|" + mutexName);
            WM_IMPORTPRESET = WinApi.RegisterWindowMessage("WM_IMPORTPRESET|" + mutexName);

            AppMutex = new Mutex(true, mutexName, out bool createdNew);
            IPCPath = Path.GetTempPath() + mutexName + ".pipe";

            if (!createdNew)
            {
                if (!isSilent)
                    WinApi.PostMessage(new IntPtr(0xffff), WM_SHOWFIRSTINSTANCE, IntPtr.Zero, IntPtr.Zero);

                if (presetFile != null)
                {
                    try
                    {
                        File.WriteAllText(IPCPath, presetFile);
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
                    Utils.SaveSettings();
                }

                // Analytics and crashes
                Crashes.ShouldProcessErrorReport = (ErrorReport report) =>
                {
                    if (report.StackTrace.StartsWith("Microsoft.AppCenter.Crashes.TestCrashException") ||
                    report.StackTrace.StartsWith("System.OutOfMemoryException"))
                        return false;

                    return true;
                };

                Crashes.GetErrorAttachments = (ErrorReport report) =>
                {
                    StringBuilder settingsTxt = new StringBuilder();

                    foreach (System.Configuration.SettingsPropertyValue setting in settings.PropertyValues)
                    {
                        if (setting.Name == "id")
                            continue;

                        settingsTxt.AppendLine(setting.Name + " = " + setting.SerializedValue);
                    }

                    StringBuilder rpcLog = new StringBuilder();
                    string[] temp = File.ReadAllLines(Application.StartupPath + "\\rpc.log");

                    for (int i = temp.Length - 1; i >= 0 && i >= temp.Length - 200; i--)
                    {
                        if (temp[i].Contains("applicationID"))
                            continue;

                        rpcLog.Insert(0, temp[i] + "\r\n");
                    }

                    return new ErrorAttachmentLog[]
                    {
                        ErrorAttachmentLog.AttachmentWithText(settingsTxt.ToString(), "settings.txt"),
                        ErrorAttachmentLog.AttachmentWithText(rpcLog.ToString(), "rpc.log")
                    };
                };

                AppCenter.SetCountryCode(RegionInfo.CurrentRegion.TwoLetterISORegionName);
                if (AppCenterSecret.Value != "{app secret}")
                {
                    AppCenter.Start(AppCenterSecret.Value, typeof(Analytics), typeof(Crashes));
                }
                // If you want to enable AppCenter, create a .appSecret file in the CustomRPC\CustomRPC folder

                IntPtr _ = new MainForm(presetFile).Handle; // Terrible, yet allows to fully initialize the form without showing it first
                Application.Run();
            }
            catch (Exception ex)
            {
                var errMsg = new StringBuilder();
                errMsg.AppendLine(DateTime.Now.ToLocalTime().ToString());
                errMsg.AppendLine(ex.ToString());
                errMsg.AppendLine();

                // This loop ensures writing to the file
                bool error = true;
                while (error)
                {
                    try
                    {
                        File.AppendAllText(Application.StartupPath + "\\crash.log", errMsg.ToString());
                        error = false;
                    }
                    catch
                    {
                        System.Threading.Tasks.Task.Delay(100);
                        error = true;
                    }
                }

                Exception ex_inner = ex;

                while (ex_inner.InnerException != null)
                    ex_inner = ex_inner.InnerException;

                if (ex_inner is FileNotFoundException && ex_inner.Message.Contains("Version=") || ex_inner is FileLoadException || ex_inner is BadImageFormatException)
                {
                    var result = MessageBox.Show($"{ex_inner.Message}\r\n\r\n{string.Format(Strings.errorLoadingAssembly, Application.StartupPath)}", Strings.error, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                    if (result == DialogResult.Retry)
                    {
                        AppMutex.Close();
                        Application.Restart();
                    }
                }
                else
                {
                    MessageBox.Show(ex.ToString(), "CustomRP - " + Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
            finally
            {
                Utils.SaveSettings();
            }

            GC.KeepAlive(AppMutex);
        }
    }
}