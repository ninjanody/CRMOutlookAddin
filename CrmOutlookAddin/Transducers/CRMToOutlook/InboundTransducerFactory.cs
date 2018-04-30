using System;

namespace CrmOutlookAddin.Transducers.CRMToOutlook
{
    internal static class InboundTransducerFactory
    {
        internal static AbstractInboundTransducer GetTransducer(string moduleName)
        {
            AbstractInboundTransducer result;
            switch (moduleName)
            {
                case "Calls":
                    result = new InboundCallTransducer();
                    break;

                case "Contacts":
                    result = new InboundContactTransducer();
                    break;

                case "Meetings":
                    result = new InboundMeetingTransducer();
                    break;

                case "Tasks":
                    result = new InboundTaskTransducer();
                    break;

                default:
                    throw new Exception($"No inbound transducer class available for '{moduleName}'");
            }

            return result;
        }
    }
}
