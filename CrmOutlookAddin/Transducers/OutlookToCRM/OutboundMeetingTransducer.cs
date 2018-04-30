using CrmOutlookAddin.Wrappers;
using System;

namespace CrmOutlookAddin.Transducers.OutlookToCRM
{
    public class OutboundMeetingTransducer : AbstractOutboundTransducer<MeetingItem>
    {
        public override string ItemToJson(MeetingItem item)
        {
            throw new NotImplementedException();
        }
    }
}
