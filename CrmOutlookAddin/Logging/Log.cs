using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace CrmOutlookAddin.Logging
{
    /// <summary>
    /// The one and only log.
    /// </summary>
    /// <remarks>
    /// We cannot hang the log off Globals.ThisAddin, because that makes everything untestable.
    /// I dislike singletons, but the log is a natural singleton. So it shall be.
    /// </remarks>
    public class Log
    {
        /// <summary>
        /// My underlying instance.
        /// </summary>
        private static readonly Lazy<Log> lazy =
            new Lazy<Log>(() => new Log());

        /// <summary>
        /// A log, to log stuff to.
        /// </summary>
        private readonly ILog log;

        /// <summary>
        /// A lock on creating new items.
        /// </summary>
        private object creationLock = new object();

        private Log()
        {
            //Get the assembly information
            Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var hierarchy = (Hierarchy)LogManager.GetRepository();

            var patternLayout = new PatternLayoutWithHeader("%date | %-2thread | %-5level | %message%newline", assembly);
            patternLayout.ActivateOptions();

            Level level = FromLevel(Properties.Settings.Default.LogLevel);
            string name = assembly.GetName().Name;
            var appender = new RollingFileAppender
            {
                AppendToFile = true,
                File = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\{name}\\Logs\\{name}.log",
                Layout = patternLayout,
                RollingStyle = RollingFileAppender.RollingMode.Size,
                MaxFileSize = 1000000, // 1MB
                StaticLogFileName = true,
                MaxSizeRollBackups = 10,
                Threshold = level,
                Encoding = Encoding.UTF8,
            };
            appender.ActivateOptions();

            hierarchy.Root.AddAppender(appender);
            hierarchy.Root.Level = level;
            hierarchy.Configured = true;

            this.log = LogManager.GetLogger(assembly.FullName);

            //Location is where the assembly is run from
            string assemblyLocation = assembly.Location;
        }

        /// <summary>
        /// A public accessor for my instance.
        /// </summary>
        public static Log Instance { get { return lazy.Value; } }

        public void AddEntry(string message, LogEntryType type)
        {
            switch (type)
            {
                case LogEntryType.Debug:
                    this.Debug(message);
                    break;

                case LogEntryType.Information:
                    this.Info(message);
                    break;

                case LogEntryType.Warning:
                    this.Warn(message);
                    break;

                default:
                    this.Error($"({type}): {message}");
                    break;
            }
        }

        public void Debug(string message)
        {
            try
            {
                log.Debug(message);
            }
            catch (Exception) { }
        }

        public void Error(string message)
        {
            try
            {
                log.Error(message);
            }
            catch (Exception) { }
        }

        public void Error(string message, Exception error)
        {
            try
            {
                log.Error(message, error);
            }
            catch (Exception) { }
        }

        public void Info(string message)
        {
            try
            {
                log.Info(message);
            }
            catch (Exception) { }
        }

        public void ShowAndAddEntry(string message, LogEntryType type)
        {
            this.AddEntry(message, type);
            MessageBox.Show(message, type.ToString(), MessageBoxButtons.OK, IconForLogLevel(type));
        }

        public void Warn(string message)
        {
            try
            {
                log.Warn(message);
            }
            catch (Exception) { }
        }

        public void Warn(string message, Exception error)
        {
            try
            {
                log.Warn(message, error);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Return an appropriate icon for this log level.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <returns>An appropriate icon.</returns>
        private static MessageBoxIcon IconForLogLevel(LogEntryType level)
        {
            MessageBoxIcon icon;

            switch (level)
            {
                case LogEntryType.Debug:
                case LogEntryType.Information:
                    icon = MessageBoxIcon.Information;
                    break;

                case LogEntryType.Warning:
                    icon = MessageBoxIcon.Warning;
                    break;

                default:
                    icon = MessageBoxIcon.Error;
                    break;
            }

            return icon;
        }

        private Level FromLevel(LogEntryType entryType)
        {
            Level result;

            switch (entryType)
            {
                case LogEntryType.Debug:
                    result = Level.Debug;
                    break;

                case LogEntryType.Information:
                    result = Level.Info;
                    break;

                case LogEntryType.Warning:
                    result = Level.Warn;
                    break;

                default:
                    result = Level.Error;
                    break;
            }

            return result;
        }

        private IEnumerable<string> GetLogHeader()
        {
            List<string> result = new List<string>();
            System.Reflection.Assembly assemblyInfo = System.Reflection.Assembly.GetExecutingAssembly();

            try
            {
                result.Add($"{assemblyInfo.GetName().Name} v{assemblyInfo.GetName().Version}");
            }
            catch (Exception any)
            {
                result.Add($"Exception {any.GetType().Name} '{any.Message}' while printing log header");
            }

            return result;
        }

        /// <summary>
        /// Layout for log files.
        /// </summary>
        private class PatternLayoutWithHeader : PatternLayout
        {
            private readonly string[] pageHeader;

            public PatternLayoutWithHeader(string pattern, Assembly assembly)
                : base(pattern)
            {
                AssemblyName assemblyName = assembly.GetName();

                pageHeader = new string[]
                {
                    $"{assemblyName.Name} v{assemblyName.Version}",
                    $"Developed by {assembly.GetCustomAttribute<AssemblyCompanyAttribute>()}",
                    $"Copyright {assembly.GetCustomAttribute<AssemblyCopyrightAttribute>()}"
                };
            }

            public override string Header
            {
                get
                {
                    var newline = Environment.NewLine;
                    string separator = new String('-', pageHeader.Select(s => s.Length).Max());
                    return
                        separator + newline +
                        string.Join(newline, pageHeader) + newline +
                        separator + newline;
                }
            }
        }
    }
}
