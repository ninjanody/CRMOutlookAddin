namespace CrmOutlookAddin.Wrappers
{
    using System;
    using Outlook = Microsoft.Office.Interop.Outlook;

    public class CallItem : AbstractAppointmentItem
    {
        public CallItem(Outlook.AppointmentItem item) : base(item)
        {
        }

        public override string DistinctFields
        {
            get
            {
                return $"subject: '{this.Subject}'; start: '{this.Start}'";
            }
        }

        public override string OutlookId
        {
            get
            {
                return this.item.EntryID;
            }
        }

        public override void CacheItem()
        {
            throw new NotImplementedException();
        }
    }
}
