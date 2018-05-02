namespace CrmOutlookAddin.Transducers.OutlookToCRM
{
    using CrmOutlookAddin.Wrappers;
    using System;
    using System.Collections.Generic;

    public class OutboundContactTransducer : AbstractOutboundTransducer<ContactItem>
    {
        public override ICollection<string> ItemToJson(ContactItem item)
        {
            throw new NotImplementedException();
        }
    }
}
