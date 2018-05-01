namespace CrmOutlookAddin.Wrappers
{
    using System;
    using Outlook = Microsoft.Office.Interop.Outlook;

    /// <summary>
    /// A wrapper which wraps an appointment which will be considered by CRM to be a 'Call'.
    /// </summary>
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

        public override void CacheItem()
        {
            throw new NotImplementedException();
        }
    }
}
