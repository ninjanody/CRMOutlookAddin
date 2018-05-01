namespace CrmOutlookAddin.Wrappers
{
    using System;
    using Outlook = Microsoft.Office.Interop.Outlook;

    public class EmailItem : AbstractItem
    {
        private readonly Outlook.MailItem item;

        public EmailItem(Outlook.MailItem item)
        {
            this.item = item;
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
