namespace CrmOutlookAddinTests
{
    using CrmOutlookAddin.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CrmOutlookAddin.Wrappers;
    using CrmOutlookAddin.Exceptions;
    using Outlook = Microsoft.Office.Interop.Outlook;

    public class TestableItemManager : AbstractItemManager
    {
        protected override AbstractItem CreateItem(ItemType type, string outlookId, string crmId)
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
                this.byCrmId[crmId] = result;
            }
            return result;
        }

        private AbstractItem CreateTask(string outlookId, string crmId)
        {
            throw new NotImplementedException();
        }

        private AbstractItem CreateMeeting(string outlookId, string crmId)
        {
            throw new NotImplementedException();
        }

        private AbstractItem CreateContact(string outlookId, string crmId)
        {
            throw new NotImplementedException();
        }

        private AbstractItem CreateCall(string outlookId, string crmId)
        {
            var result = new TestableCallItem();

            this.byOutlookId[result.OutlookId] = result;
            if (crmId != null)
            {
                result.CrmEntryId = crmId;
                this.byCrmId[crmId] = result;
            }

            return result;
        }
    }

    /// <summary>
    /// An object which looks sufficiently like a CallItem for test purposes, but which does not
    /// actually wrap an Outlook item.
    /// </summary>
    internal class TestableCallItem : CallItem
    {
        private string body;
        private int duration;
        private DateTime end;
        private string id;
        private DateTime modified;
        private DateTime start;
        private string subject;
        private string crmEntryId;

        internal TestableCallItem() : base((Outlook.AppointmentItem)null)
        {
            this.id = Guid.NewGuid().ToString("D");
            this.modified = DateTime.Now;
        }

        public override string CrmEntryId
        {
            get
            {
                return this.crmEntryId;
            }

            set
            {
                this.crmEntryId = value;
            }
        }

        public override string Body
        {
            get
            {
                return this.body;
            }

            set
            {
                this.body = value;
                this.modified = DateTime.Now;
            }
        }

        public override int Duration
        {
            get
            {
                return this.duration;
            }

            set
            {
                this.duration = value;
                this.modified = DateTime.Now;
            }
        }

        public override DateTime End
        {
            get
            {
                return this.end;
            }

            set
            {
                this.end = value;
                this.modified = DateTime.Now;
            }
        }

        public override DateTime LastModificationTime
        {
            get
            {
                return this.modified;
            }
        }

        public override string OutlookId
        {
            get
            {
                return this.id;
            }
        }

        public override DateTime Start
        {
            get
            {
                return this.start;
            }

            set
            {
                this.start = value;
                this.modified = DateTime.Now;
            }
        }

        public override DateTime StartUTC
        {
            get
            {
                return this.start;
            }

            set
            {
                this.start = value;
            }
        }

        public override string Subject
        {
            get
            {
                return this.subject;
            }

            set
            {
                this.subject = value;
                this.modified = DateTime.Now;
            }
        }
    }
}
