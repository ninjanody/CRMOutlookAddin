using CrmOutlookAddin.Wrappers;
using System;
using System.Collections.Generic;

namespace CrmOutlookAddin.Transducers.CRMToOutlook
{
    internal class InboundContactTransducer : AbstractInboundTransducer<ContactItem>
    {
        public override ContactItem JsonToItem(string json)
        {
            throw new NotImplementedException();
        }

        public override IList<ContactItem> JsonToItems(string json)
        {
            throw new NotImplementedException();
        }
    }
}
