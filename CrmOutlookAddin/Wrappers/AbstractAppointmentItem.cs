namespace CrmOutlookAddin.Wrappers
{
    using System;
    using System.Collections.Generic;
    using Outlook = Microsoft.Office.Interop.Outlook;

    public abstract class AbstractAppointmentItem : AbstractItem
    {
        protected Outlook.AppointmentItem item;

        public AbstractAppointmentItem(Outlook.AppointmentItem item)
        {
            this.item = item;
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
