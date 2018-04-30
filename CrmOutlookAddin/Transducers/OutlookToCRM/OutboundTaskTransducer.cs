using CrmOutlookAddin.Wrappers;
using System;

namespace CrmOutlookAddin.Transducers.OutlookToCRM
{
    public class OutboundTaskTransducer : AbstractOutboundTransducer<TaskItem>
    {
        public override string ItemToJson(TaskItem item)
        {
            throw new NotImplementedException();
        }
    }
}
