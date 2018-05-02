using CrmOutlookAddin.Wrappers;
namespace CrmOutlookAddin.Transducers.OutlookToCRM
{
    using System;
    using System.Collections.Generic;

    public class OutboundCallTransducer : AbstractOutboundTransducer<CallItem>
    {
        public override ICollection<string> ItemToJson(CallItem item)
        {
            throw new NotImplementedException();
        }
    }
}
