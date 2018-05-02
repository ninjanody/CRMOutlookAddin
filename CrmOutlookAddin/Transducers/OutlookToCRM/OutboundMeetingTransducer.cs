namespace CrmOutlookAddin.Transducers.OutlookToCRM
{
    using CrmOutlookAddin.Wrappers;
    using System;
    using System.Collections.Generic;

    public class OutboundMeetingTransducer : AbstractOutboundTransducer<MeetingItem>
    {
        public override ICollection<string> ItemToJson(MeetingItem item)
        {
            throw new NotImplementedException();
        }
    }
}
