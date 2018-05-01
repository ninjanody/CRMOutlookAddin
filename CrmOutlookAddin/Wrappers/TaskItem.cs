namespace CrmOutlookAddin.Wrappers
{
    using System;
    using Outlook = Microsoft.Office.Interop.Outlook;

    public class TaskItem : AbstractItem
    {
        /// <summary>
        /// The actual COM object which I wrap.
        /// </summary>
        private readonly Outlook.TaskItem item;

        public TaskItem(Outlook.TaskItem item)
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
