namespace CrmOutlookAddin.Transducers.OutlookToCRM
{
    using CrmOutlookAddin.Wrappers;
    using System;
    using System.Collections.Generic;

    public class OutboundTaskTransducer : AbstractOutboundTransducer<TaskItem>
    {
        public override ICollection<string> ItemToJson(TaskItem item)
        {
            throw new NotImplementedException();
        }
    }
}
