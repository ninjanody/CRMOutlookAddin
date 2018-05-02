namespace CrmOutlookAddin.Transducers.OutlookToCRM
{
    using CrmOutlookAddin.Wrappers;
    using System;
    using System.Collections.Generic;

    public class OutboundEmailTransducer : AbstractOutboundTransducer<EmailItem>
    {
        public override ICollection<string> ItemToJson(EmailItem item)
        {
            throw new NotImplementedException();
        }
    }
}
