/**
 * Outlook integration for SuiteCRM.
 * @package Outlook integration for SuiteCRM
 * @copyright Simon Brooke simon@journeyman.cc
 * @author Simon Brooke simon@journeyman.cc
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU LESSER GENERAL PUBLIC LICENCE as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU LESSER GENERAL PUBLIC LICENCE
 * along with this program; if not, see http://www.gnu.org/licenses
 * or write to the Free Software Foundation,Inc., 51 Franklin Street,
 * Fifth Floor, Boston, MA 02110-1301  USA
 */
namespace CrmOutlookAddin.Core
{
    using System;
    using System.Linq;
    using CrmOutlookAddin.Logging;
    using CrmOutlookAddin.Transducers;
    using CrmOutlookAddin.Transducers.OutlookToCRM;

    public class Synchroniser
    {
        /// <summary>
        ///     The item manager from which I shall request items.
        /// </summary>
        private readonly IItemManager manager;

        /// <summary>
        ///     The session through which I shall transmit data to CRM.
        /// </summary>
        private readonly ISession transmitter;

        /// <summary>
        ///     Create a new instance of a Synchroniser, using the specified item manager and session.
        /// </summary>
        /// <remarks>
        ///     Exposed for testing only; it is not intended that this override should be used outside
        ///     this class in production.
        /// </remarks>
        /// <param name="manager">An item manager.</param>
        /// <param name="transmitter">A session.</param>
        public Synchroniser(IItemManager manager, ISession transmitter)
        {
            this.manager = manager;
            this.transmitter = transmitter;
        }

        /// <summary>
        ///     Create a new instance of a Synchroniser, using the standard item manager and session.
        /// </summary>
        public Synchroniser() : this(ItemManager.Instance, Globals.ThisAddIn.GetCRMSession())
        {
        }

        /// <summary>
        ///     Better to move Synchronise out into a lightweight Synchroniser object in order that
        ///     a custom ItemManager can be injected for testing.
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
                    lock (queued.TransmissionLock)
                    {
                        try
                        {
                            queued.SetTransmitted();
                            var commands = TransducerFactory.GetOutbound(queued).ItemToJson(queued).ToArray();

                            var id = transmitter.Transmit(commands[0]);

                            for (var index = 1; index < commands.Count(); index++)
                            {
                                var command = commands[index].Replace(AbstractOutboundTransducer.MissingIdMarker, id);

                                transmitter.Transmit(command);
                            }

                            queued.SetSynced();
                        }
                        catch (Exception any)
                        {
                            Log.Instance.Error($"Failure white transmitting '{queued.Description}'", any);
                            queued.SetPending(true);
                        }
                    }
                }
            }
        }
    }
}