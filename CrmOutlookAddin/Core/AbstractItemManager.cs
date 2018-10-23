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
    using Exceptions;
    using Logging;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using Utils;
    using Wrappers;
    using Outlook = Microsoft.Office.Interop.Outlook;

    /// <summary>
    /// The entire working guts of the ItemManager, to allow testable instances to be created.
    /// </summary>
    /// <remarks>
    /// Obviously, in production, this class should ONLY be subclassed by ItemManager. All other
    /// subclasses should be for test purposes only, and should NOT be available in production.
    /// </remarks>
    public abstract class AbstractItemManager : IItemManager
    {
        /// <summary>
        /// A dictionary of sync states indexed by crm id, where known.
        /// </summary>
        protected ConcurrentDictionary<string, AbstractItem> ByCrmId = new ConcurrentDictionary<string, AbstractItem>();

        /// <summary>
        /// A dictionary of sync states indexed by the values of distinct fields.
        /// </summary>
        protected ConcurrentDictionary<string, AbstractItem> ByDistinctFields = new ConcurrentDictionary<string, AbstractItem>();

        /// <summary>
        /// A dictionary of all known sync states indexed by outlook id.
        /// </summary>
        protected ConcurrentDictionary<string, AbstractItem> ByOutlookId = new ConcurrentDictionary<string, AbstractItem>();

        /// <summary>
        /// A lock on creating new items.
        /// </summary>
        private object creationLock = new object();

        /// <summary>
        /// A log, to log stuff to.
        /// </summary>
        private Log log = Log.Instance;

        public ICollection<AbstractItem> AllItems => this.ByOutlookId.Values;
 
        public AbstractItem GetByCrmId(string crmId, ItemType type)
        {
            AbstractItem result;

            try
            {
                result = this.ByCrmId[crmId];
            }
            catch (KeyNotFoundException)
            {
                result = this.CreateItem(type, null, crmId);
            }

            return result;
        }

        public AbstractItem GetByDistinctFields(Dictionary<string, object> fields, ItemType type)
        {
            return this.GetByDistinctFields(StringUtils.CanonicaliseFields(fields), type);
        }

        public AbstractItem GetByDistinctFields(string canonicalFields, ItemType type)
        {
            AbstractItem result;

            try
            {
                result = this.ByDistinctFields[canonicalFields];
            }
            catch (KeyNotFoundException)
            {
                result = null;
            }

            return result;
        }

        public AbstractItem GetByOutlookId(string outlookId, ItemType type)
        {
            AbstractItem result;

            try
            {
                result = this.ByOutlookId[outlookId];
            }
            catch (KeyNotFoundException)
            {
                result = null;
            }

            return result;
        }

        public void RemoveWrapper(AbstractItem abstractWrapper)
        {
            this.ByOutlookId[abstractWrapper.OutlookId] = null;

            if (!string.IsNullOrEmpty(abstractWrapper.CrmEntryId))
            {
                this.ByCrmId[abstractWrapper.CrmEntryId] = null;
            }
            if (!string.IsNullOrEmpty(abstractWrapper.DistinctFields))
            {
                this.ByDistinctFields[abstractWrapper.DistinctFields] = null;
            }
        }

        /// <summary>
        /// Create a new item of the specified type, wrapping the Outlook item with this outlook id,
        /// if specified, or a newly created Oulook item otherwise, and having this crm id.
        /// </summary>
        /// <remarks>
        /// `virtual` in order that it may be overridden in test subclasses.
        /// </remarks>
        /// <param name="type">The type of item to create.</param>
        /// <param name="outlookId">The outlook id of the item, if known, else null.</param>
        /// <param name="crmId">The CRM id of the item, if known, else null.</param>
        /// <returns>The item created.</returns>
        protected virtual AbstractItem CreateItem(ItemType type, string outlookId, string crmId)
        {
            AbstractItem result;

            switch (type)
            {
                case ItemType.Call:
                    result = this.CreateCall(outlookId, crmId);
                    break;

                case ItemType.Contact:
                    result = this.CreateContact(outlookId, crmId);
                    break;

                case ItemType.Meeting:
                    result = this.CreateMeeting(outlookId, crmId);
                    break;

                case ItemType.Task:
                    result = this.CreateTask(outlookId, crmId);
                    break;

                default:
                    throw new ShouldNotHappenException($"Unknown item type '{type}'");
            }

            if (!string.IsNullOrEmpty(crmId))
            {
                this.ByCrmId[crmId] = result;
            }

            return result;
        }

        private AbstractItem CreateAppointment(string outlookId, string crmId, Outlook.OlMeetingStatus status)
        {
            AbstractItem result;

            Outlook.NameSpace session = Globals.ThisAddIn.GetOutlookSession();

            if (session != null)
            {
                Outlook.AppointmentItem legacy = null;
                Outlook.MAPIFolder folder = session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderCalendar);

                if (!string.IsNullOrEmpty(outlookId))
                {
                    legacy = folder.Items.Add(Outlook.OlItemType.olAppointmentItem);
                    legacy.MeetingStatus = status;
                    result = new CallItem(legacy);
                    result.CrmEntryId = crmId;

                    this.ByCrmId[crmId] = result;
                    this.ByOutlookId[legacy.EntryID] = result;
                }
                else
                {
                    result = FindExistingAppointmentItem(outlookId, crmId, folder);
                }
            }
            else
            {
                throw new ShouldNotHappenException("No Outlook session!");
            }

            return result;
        }

        private AbstractItem CreateCall(string outlookId, string crmId)
        {
            return this.CreateAppointment(outlookId, crmId, Outlook.OlMeetingStatus.olNonMeeting);
        }

        private AbstractItem CreateContact(string outlookId, string crmId)
        {
            throw new NotImplementedException();
        }

        private AbstractItem CreateMeeting(string outlookId, string crmId)
        {
            return this.CreateAppointment(outlookId, crmId, Outlook.OlMeetingStatus.olMeeting);
        }

        private AbstractItem CreateTask(string outlookId, string crmId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Find an existing appointment item in the connected Outlook instance which matches these
        /// parameters.
        /// </summary>
        /// <remarks>You can't type parameterise this because Outlook types are not classes.</remarks>
        /// <param name="outlookId">The Outlook id sought, if known, else null.</param>
        /// <param name="crmId">The CRM id sought, if known, else null.</param>
        /// <param name="folder">The folder to search.</param>
        /// <returns>An abstract item wrapping the Outlook item sought.</returns>
        private AbstractItem FindExistingAppointmentItem(string outlookId, string crmId, Outlook.MAPIFolder folder)
        {
            AbstractItem result;
            Outlook.AppointmentItem legacy = null;

            foreach (object obj in folder.Items)
            {
                Outlook.AppointmentItem olItem = obj as Outlook.AppointmentItem;
                if (olItem != null && olItem.EntryID == outlookId)
                {
                    legacy = olItem;
                    break;
                }
                // TODO: CRM id.
            }
            if (legacy != null)
            {
                if (legacy.MeetingStatus == Outlook.OlMeetingStatus.olNonMeeting)
                {
                    result = new CallItem(legacy);
                }
                else
                {
                    result = new Wrappers.MeetingItem(legacy);
                }

                this.ByOutlookId[legacy.EntryID] = result;
                if (!string.IsNullOrEmpty(crmId)) this.ByCrmId[crmId] = result;
            }
            else
            {
                throw new ItemNotFoundException();
            }

            return result;
        }
    }
}
