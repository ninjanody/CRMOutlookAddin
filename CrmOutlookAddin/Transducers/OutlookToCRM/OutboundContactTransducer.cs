using CrmOutlookAddin.Wrappers;
using System;

namespace CrmOutlookAddin.Transducers.OutlookToCRM
{
    public class OutboundContactTransducer : AbstractOutboundTransducer<ContactItem>
    {
        public override string ItemToJson(ContactItem item)
        {
            throw new NotImplementedException();
        }
    }
}
