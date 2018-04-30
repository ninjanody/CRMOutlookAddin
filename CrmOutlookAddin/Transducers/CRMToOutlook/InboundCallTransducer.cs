using CrmOutlookAddin.Wrappers;
using System;
using System.Collections.Generic;

namespace CrmOutlookAddin.Transducers.CRMToOutlook
{
    internal class InboundCallTransducer : AbstractInboundTransducer<CallItem>
    {
        public override CallItem JsonToItem(string json)
        {
            throw new NotImplementedException();
        }

        public override IList<CallItem> JsonToItems(string json)
        {
            throw new NotImplementedException();
        }
    }
}
