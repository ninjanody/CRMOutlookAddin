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
namespace CrmOutlookAddin.Wrappers
{
    using System;
    using System.Collections.Generic;
    using Outlook = Microsoft.Office.Interop.Outlook;

    /// <summary>
    /// Abstract superclass for CallItem and MeetingItem, capturing the commonality of things which, underneath, actually wrap an Outlook.AppointmentItem.
    /// </summary>
    /// <see cref="CallItem"/>
    /// <see cref="MeetingItem"/> 
    public abstract class AbstractAppointmentItem : AbstractItem
    {
        protected Outlook.AppointmentItem item;

        public AbstractAppointmentItem(Outlook.AppointmentItem item)
        {
            this.item = item;
        }

        /// <summary>
        /// Gets or sets the CRM entry Id.
        /// </summary>
        /// <remarks>
        /// Because Outlook items are not real objects and do not inherit from a common superclass, this
        /// identical code needs to be in each of AbstractAppointmentItem, ContactItem, EmailItem and TaskItem. If
        /// edited in any of those places, please keep the other two in sync.
        /// </remarks>
        public override string CrmEntryId
        {
            get
            {
                string result = null;
                try
                {
                    var prop = item.UserProperties[AbstractItem.CrmIdPropertyName];

                    if (prop != null)
                    {
                        result = prop.Value;
                    }
                }
                catch (Exception) { }

                return result;
            }

            set
            {
                Outlook.UserProperty prop;

                try
                {
                    prop = item.UserProperties[AbstractItem.CrmIdPropertyName];

                    if (prop == null)
                    {
                        prop = item.UserProperties.Add(AbstractItem.CrmIdPropertyName, Outlook.OlUserPropertyType.olText);
                    }
                }
                catch (Exception)
                {
                    prop = item.UserProperties.Add(AbstractItem.CrmIdPropertyName, Outlook.OlUserPropertyType.olText);
                }

                /* don't set it unless the value is actually different. */
                if (prop.Value != value)
                {
                    prop.Value = value;
                }
            }
        }

        public override string OutlookId
        {
            get
            {
                return this.item.EntryID;
            }
        }

        /// <summary>
        /// Wrappers round item properties: AllDayEvent.
        /// </summary>
        public virtual bool AllDayEvent
        {
            get { return this.item.AllDayEvent; }
            set { this.item.AllDayEvent = value; }
        }

        /// <summary>
        /// Wrappers round item properties: Body.
        /// </summary>
        public virtual string Body
        {
            get { return this.item.Body; }
            set { this.item.Body = value; }
        }

        /// <summary>
        /// Wrappers round item properties: BusyStatus.
        /// </summary>
        public virtual Outlook.OlBusyStatus BusyStatus
        {
            get { return this.item.BusyStatus; }
            set { this.item.BusyStatus = value; }
        }

        /// <summary>
        /// Wrappers round item properties: Categories.
        /// </summary>
        public virtual string Categories
        {
            get { return this.item.Categories; }
            set { this.item.Categories = value; }
        }

        /// <summary>
        /// Wrappers round item properties: Companies.
        /// </summary>
        public virtual string Companies
        {
            get { return this.item.Companies; }
            set { this.item.Companies = value; }
        }

        public override string Description
        {
            get
            {
                return $"{this.Subject} at {this.Start}";
            }
        }

        /// <summary>
        /// Wrappers round item properties: Duration.
        /// </summary>
        public virtual int Duration
        {
            get { return this.item.Duration; }
            set { this.item.Duration = value; }
        }

        /// <summary>
        /// Wrappers round item properties: End.
        /// </summary>
        public virtual DateTime End
        {
            get { return this.item.End; }
            set { this.item.End = value; }
        }

        /// <summary>
        /// Wrappers round item properties: EndUTC.
        /// </summary>
        public virtual DateTime EndUTC
        {
            get { return this.item.EndUTC; }
            set { this.item.EndUTC = value; }
        }

        /// <summary>
        /// Wrappers round item properties: GlobalAppointmentID.
        /// </summary>
        public virtual string GlobalAppointmentID
        {
            get { return this.item.GlobalAppointmentID; }
        }

        /// <summary>
        /// Wrappers round item properties: LastModificationTime.
        /// </summary>
        public virtual DateTime LastModificationTime
        {
            get { return this.item.LastModificationTime; }
        }

        /// <summary>
        /// Wrappers round item properties: Location.
        /// </summary>
        public virtual string Location
        {
            get { return this.item.Location; }
            set { this.item.Location = value; }
        }

        /// <summary>
        /// Wrappers round item properties: MeetingStatus.
        /// </summary>
        public virtual Outlook.OlMeetingStatus MeetingStatus
        {
            get { return this.item.MeetingStatus; }
            set { this.item.MeetingStatus = value; }
        }

        /// <summary>
        /// Wrappers round item properties: Organizer.
        /// </summary>
        public virtual string Organizer
        {
            get { return this.item.Organizer; }
        }

        /// <summary>
        /// Wrappers round item properties: Recipients.
        /// </summary>
        public virtual IList<RecipientWrapper> Recipients
        {
            get
            {
                List<RecipientWrapper> result = new List<RecipientWrapper>();

                foreach (Outlook.Recipient recipient in item.Recipients)
                {
                    result.Add(new RecipientWrapper(recipient));
                }

                return result;
            }
            set
            {
                foreach (RecipientWrapper wrapper in value)
                {
                    // TODO: ish. Test this does not create duplicates.
                    item.Recipients.Add(wrapper.Description);
                }
            }
        }

        /// <summary>
        /// Wrappers round item properties: Start.
        /// </summary>
        public virtual DateTime Start
        {
            get { return this.item.Start; }
            set { this.item.Start = value; }
        }

        /// <summary>
        /// Wrappers round item properties: StartUTC.
        /// </summary>
        public virtual DateTime StartUTC
        {
            get { return this.item.StartUTC; }
            set { this.item.StartUTC = value; }
        }

        /// <summary>
        /// Wrappers round item properties: Subject.
        /// </summary>
        public virtual string Subject
        {
            get { return this.item.Subject; }
            set { this.item.Subject = value; }
        }

        /// <summary>
        /// True if this appointment is a 'Call', in CRM terminology; false if it's a 'Meeting'.
        /// </summary>
        /// <param name="item">The appointment in question.</param>
        /// <returns>
        /// true if the specified appointment is a call.
        /// </returns>
        public static bool IsCall(Outlook.AppointmentItem item)
        {
            return item.MeetingStatus == Outlook.OlMeetingStatus.olNonMeeting;
        }

        /* not all the properties of an AppointmentItem are represented here; essentially these
         *  are the ones we need */
    }
}
