namespace CrmOutlookAddin.Utils
{
    /// <summary>
    /// A state which a process (such as a synchronisation process) may be in.
    /// </summary>
    public enum RunState
    {
        /// <summary>
        /// Actually doing something now.
        /// </summary>
        Running,

        /// <summary>
        /// Waiting for my sync period to come around.
        /// </summary>
        Waiting,

        /// <summary>
        /// Signalled to stop but not yet stopped.
        /// </summary>
        Stopping,

        /// <summary>
        /// Stopped.
        /// </summary>
        Stopped
    };
}
