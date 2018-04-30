namespace CrmOutlookAddin.Utils
{
    using System;

    /// <summary>
    /// An object whichbarks if it isn't patted regularly enough.
    /// </summary>
    public abstract class Watchdog : RepeatingProcess
    {
        /// <summary>
        /// The interval after the last pat at which I bark.
        /// </summary>
        private readonly TimeSpan timeout;

        /// <summary>
        /// When I was last patted
        /// </summary>
        private DateTime lastPat = DateTime.Now;

        /// <summary>
        /// Create a new instance of watchdog with this name and timeout.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="timeout">The timeout.</param>
        public Watchdog(string name, long timeout) : this(name, new TimeSpan(timeout)) { }

        public Watchdog(string name, TimeSpan timeout) : base(name)
        {
            this.Interval = new TimeSpan(timeout.Ticks / 10);
            this.timeout = timeout;
        }

        /// <summary>
        /// Do whatever I need to do when my timeout has elapsed.
        /// </summary>
        public abstract void Bark();

        /// <summary>
        /// Reset my countdown start time.
        /// </summary>
        public void Pat()
        {
            this.lastPat = DateTime.Now;
        }

        /// <summary>
        /// If my timeout has elapsed, bark and stop.
        /// </summary>
        internal override void PerformIteration()
        {
            if (lastPat + timeout > DateTime.Now)
            {
                this.Bark();
                this.Stop();
            }
        }
    }
}
