namespace CrmOutlookAddin.Wrappers
{
    using System;
    using Outlook = Microsoft.Office.Interop.Outlook;

    /// <summary>
    /// A wrapper which wraps an appointment which will be considered by CRM to be a 'Meeting'.
    /// </summary>

    public class MeetingItem : AbstractAppointmentItem
    {
        public MeetingItem(Outlook.AppointmentItem item) : base(item)
        {
        }

        public override string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string DistinctFields
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void CacheItem()
        {
            throw new NotImplementedException();
        }
    }
}
