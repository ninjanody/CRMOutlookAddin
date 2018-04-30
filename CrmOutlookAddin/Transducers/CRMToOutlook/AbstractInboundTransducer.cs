using CrmOutlookAddin.Core;

namespace CrmOutlookAddin.Transducers.CRMToOutlook
{
    public class AbstractInboundTransducer
    {
        private readonly IItemManager manager;

        public AbstractInboundTransducer()
        {
            manager = ItemManager.Instance;
        }

        public AbstractInboundTransducer(IItemManager manager)
        {
            this.manager = manager;
        }
    }
}
