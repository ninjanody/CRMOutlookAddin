namespace CrmOutlookAddin.Wrappers
{
    using System;
    using Outlook = Microsoft.Office.Interop.Outlook;

    /// <summary>
    /// A wrapper for an Outlook ContactItem.
    /// </summary>
    public class ContactItem : AbstractItem
    {
        private readonly Outlook.ContactItem item;

        public ContactItem(Outlook.ContactItem item)
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
