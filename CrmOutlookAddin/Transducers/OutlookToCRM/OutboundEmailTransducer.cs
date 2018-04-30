using CrmOutlookAddin.Wrappers;
using System;

namespace CrmOutlookAddin.Transducers.OutlookToCRM
{
    public class OutboundEmailTransducer : AbstractOutboundTransducer<EmailItem>
    {
        public override string ItemToJson(EmailItem item)
        {
            throw new NotImplementedException();
        }
    }
}
