using CrmOutlookAddin.Wrappers;
using System;
using System.Collections.Generic;

namespace CrmOutlookAddin.Transducers.CRMToOutlook
{
    internal class InboundMeetingTransducer : AbstractInboundTransducer<MeetingItem>
    {
        public override MeetingItem JsonToItem(string json)
        {
            throw new NotImplementedException();
        }

        public override IList<MeetingItem> JsonToItems(string json)
        {
            throw new NotImplementedException();
        }
    }
}
