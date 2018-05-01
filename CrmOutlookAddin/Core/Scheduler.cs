namespace CrmOutlookAddin.Core
{
    using Logging;
    using System;

    /// <summary>
    /// The single scheduler.
    /// </summary>
    public class Scheduler
    {
        /// <summary>
        /// My underlying instance.
        /// </summary>
        private static readonly Lazy<Scheduler> lazy =
            new Lazy<Scheduler>(() => new Scheduler());

        /// <summary>
        /// A lock on creating new items.
        /// </summary>
        private object creationLock = new object();

        /// <summary>
        /// A log, to log stuff to.
        /// </summary>
        private Log log = Log.Instance;

        /// <summary>
        /// A public accessor for my instance.
        /// </summary>
        public static Scheduler Instance { get { return lazy.Value; } }
    }
}
