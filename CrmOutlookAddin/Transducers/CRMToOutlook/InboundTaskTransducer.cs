namespace CrmOutlookAddin.Transducers.CRMToOutlook
{
    using CrmOutlookAddin.Wrappers;
    using System;
    using System.Collections.Generic;

    internal class InboundTaskTransducer : AbstractInboundTransducer<TaskItem>
    {
        public override TaskItem JsonToItem(string json)
        {
            throw new NotImplementedException();
        }

        public override IList<TaskItem> JsonToItems(string json)
        {
            throw new NotImplementedException();
        }
    }
}
