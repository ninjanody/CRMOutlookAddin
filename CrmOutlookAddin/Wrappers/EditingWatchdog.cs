namespace CrmOutlookAddin.Wrappers
{
    using Exceptions;
    using System;
    using Utils;

    /// <summary>
    /// A watchdog which if not patted often enough sets the state of its wrapper to 'Pending'
    /// </summary>
    /// <remarks>
    /// The 'Editing' state is special in so much as we are not told when the user ceases editing.
    /// So we operate a timeout.
    /// </remarks>
    public class EditingWatchdog : Watchdog
    {
        /// <summary>
        /// The wrapper for which I am a watchdog
        /// </summary>
        private AbstractItem watched;

        public EditingWatchdog(AbstractItem wrapper) : base("EW", new TimeSpan(0, 2, 0))
        {
            this.watched = wrapper;
        }

        public override void Bark()
        {
            try
            {
                watched.SetPending();
            }
            catch (BadStateTransition bst)
            {
                Log.Warn(bst.Message);
            }
        }
    }
}
