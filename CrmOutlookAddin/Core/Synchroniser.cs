namespace CrmOutlookAddin.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Transducers;
    using Transducers.OutlookToCRM;
    using Wrappers;

    public class Synchroniser
    {
        /// <summary>
        /// The item manager from which I shall request items.
        /// </summary>
        private readonly IItemManager manager;

        /// <summary>
        /// The session through which I shall transmit data to CRM.
        /// </summary>
        private readonly ISession transmitter;

        /// <summary>
        /// Create a new instance of a Synchroniser, using the specified item manager and session.
        /// </summary>
        /// <remarks>
        /// Exposed for testing only; it is not intended that this override should be used outside
        /// this class in production.
        /// </remarks>
        /// <param name="manager">An item manager.</param>
        /// <param name="transmitter">A session.</param>
        public Synchroniser(IItemManager manager, ISession transmitter)
        {
            this.manager = manager;
            this.transmitter = transmitter;
        }

        /// <summary>
        /// Create a new instance of a Synchroniser, using the standard item manager and session.
        /// </summary>
        public Synchroniser() : this(ItemManager.Instance, Globals.ThisAddIn.GetCRMSession()) { }

        /// <summary>
        /// Better to move Synchronise out into a lightweight Synchroniser object in order that
        /// a custom ItemManager can be injected for testing.
        /// </summary>
        private void Synchronise()
        {
            foreach (var candidate in manager.AllItems.Where(x => x.State == States.Pending && x.Synchronisable))
            {
                candidate.SetQueued();
            }

            foreach (var queued in manager.AllItems.Where(x => x.State == States.Queued))
            {
                if (queued.State == States.Queued && queued.Synchronisable)
                {
                    /* yes, I know we just checked that, but it could have changed. Synchronisation 
                     * of a queue of items will take time. */
                    lock (queued.TransmissionLock)
                    {
                        try
                        {
                            queued.SetTransmitted();
                            string[] commands = TransducerFactory.GetOutbound(queued).ItemToJson(queued).ToArray();

                            string id = transmitter.Transmit(commands[0]);

                            for (int index = 1; index < commands.Count(); index++)
                            {
                                string command = commands[index].Replace(AbstractOutboundTransducer.missingIdMarker, id);

                                transmitter.Transmit(command);
                            }

                            queued.SetSynced();
                        }
                        catch (Exception any)
                        {
                            Logging.Log.Instance.Error($"Failure white transmitting '{queued.Description}'", any);
                            queued.SetPending(true);
                        }
                    }
                }
            }
        }
    }
}
