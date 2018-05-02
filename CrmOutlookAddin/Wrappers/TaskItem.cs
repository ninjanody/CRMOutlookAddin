namespace CrmOutlookAddin.Wrappers
{
    using System;
    using Outlook = Microsoft.Office.Interop.Outlook;

    /// <summary>
    /// A wrapper for an Outlook TaskItem.
    /// </summary>
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

        /// <summary>
        /// Gets or sets the CRM entry Id.
        /// </summary>
        /// <remarks>
        /// Because Outlook items are not real objects and do not inherit from a common superclass, this
        /// identical code needs to be in each of AbstractAppointmentItem, ContactItem and TaskItem. If
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
