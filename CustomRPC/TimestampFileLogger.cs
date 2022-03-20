using DiscordRPC.Logging;
using System;
using System.IO;

namespace CustomRPC
{
    /// <summary>
    /// Logs the outputs to a file with a timestamp
    /// </summary>
    public class TimestampFileLogger : ILogger
    {
        /// <summary>
        /// The level of logging to apply to this logger.
        /// </summary>
        public LogLevel Level { get; set; }

        /// <summary>
        /// File to write the logs to.
        /// </summary>
        public string LogFile { get; set; }

        private object filelock;

        /// <summary>
        /// Creates a new instance of the file logger
        /// </summary>
        /// <param name="path">The path of the log file.</param>
        public TimestampFileLogger(string path)
            : this(path, LogLevel.Trace) { }

        /// <summary>
        /// Creates a new instance of the file logger
        /// </summary>
        /// <param name="path">The path of the log file.</param>
        /// <param name="level">The level to assign to the logger.</param>
        public TimestampFileLogger(string path, LogLevel level)
        {
            Level = level;
            LogFile = path;
            filelock = new object();

            var fileInfo = new FileInfo(LogFile);

            if (fileInfo.Exists && fileInfo.Length >= 5 * 1024 * 1024)
            {
                File.Move(LogFile, LogFile + ".0");

                int fileCount = 0;

                while (File.Exists(LogFile + "." + (fileCount + 1)))
                    fileCount++;

                for (int i = fileCount; i >= 0; i--)
                {
                    File.Move(LogFile + "." + i, LogFile + "." + (i + 1));
                }
            }
        }

        /// <summary>
        /// Detailed log messages
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Trace(string message, params object[] args)
        {
            if (Level > LogLevel.Trace) return;
            lock (filelock)
                File.AppendAllText(LogFile, "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] TRCE: " + (args.Length > 0 ? string.Format(message, args) : message) + "\r\n");
        }

        /// <summary>
        /// Informative log messages
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Info(string message, params object[] args)
        {
            if (Level > LogLevel.Info) return;
            lock (filelock)
                File.AppendAllText(LogFile, "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] INFO: " + (args.Length > 0 ? string.Format(message, args) : message) + "\r\n");
        }

        /// <summary>
        /// Warning log messages
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Warning(string message, params object[] args)
        {
            if (Level > LogLevel.Warning) return;
            lock (filelock)
                File.AppendAllText(LogFile, "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] WARN: " + (args.Length > 0 ? string.Format(message, args) : message) + "\r\n");
        }

        /// <summary>
        /// Error log messsages
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Error(string message, params object[] args)
        {
            if (Level > LogLevel.Error) return;
            lock (filelock)
                File.AppendAllText(LogFile, "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] ERR : " + (args.Length > 0 ? string.Format(message, args) : message) + "\r\n");
        }
    }
}