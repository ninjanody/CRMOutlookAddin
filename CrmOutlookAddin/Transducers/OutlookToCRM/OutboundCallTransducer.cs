using CrmOutlookAddin.Wrappers;
using System;

namespace CrmOutlookAddin.Transducers.OutlookToCRM
{
    public class OutboundCallTransducer : AbstractOutboundTransducer<CallItem>
    {
        public override string ItemToJson(CallItem item)
        {
            throw new NotImplementedException();
        }
    }
}
