using CrmOutlookAddin.Wrappers;

namespace CrmOutlookAddin.Transducers.OutlookToCRM
{
    public abstract class AbstractOutboundTransducer<Wrapper> : AbstractOutboundTransducer
        where Wrapper : AbstractItem
    {
        public abstract string ItemToJson(Wrapper item);
    }
}
