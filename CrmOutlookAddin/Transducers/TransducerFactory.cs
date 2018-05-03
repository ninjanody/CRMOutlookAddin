namespace CrmOutlookAddin.Transducers
{
    using Core;
    using CRMToOutlook;
    using Exceptions;
    using OutlookToCRM;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Wrappers;

    public class TransducerFactory
    {
        /// <summary>
        /// Return an appropriate inbound transducer for this type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static AbstractInboundTransducer GetInbound(ItemType type)
        {
            AbstractInboundTransducer result;

            switch (type)
            {
                case ItemType.Call:
                    result = new InboundCallTransducer();
                    break;
                case ItemType.Contact:
                    result = new InboundContactTransducer();
                    break;
                case ItemType.Meeting:
                    result = new InboundMeetingTransducer();
                    break;
                case ItemType.Task:
                    result = new InboundTaskTransducer();
                    break;
                default:
                    throw new ShouldNotHappenException("Unknown item type");
            }

            return result;
        }

        /// <summary>
        /// Return an appropriate outbound transducer for this item.
        /// </summary>
        /// <remarks>
        /// This feels extremely clumsy. I feel there must be a better way, but I don't see it
        /// just now.
        /// </remarks>
        /// <param name="item">The item.</param>
        /// <returns>An appropriate transducer.</returns>
        public static AbstractOutboundTransducer<ItemClass> GetOutbound<ItemClass>(ItemClass item)
            where ItemClass: AbstractItem
        {
            if (item is CallItem)
            {
                return new OutboundCallTransducer() as AbstractOutboundTransducer<ItemClass>;
            } 
            else if (item is ContactItem)
            {
                return new OutboundContactTransducer() as AbstractOutboundTransducer<ItemClass>;
            }
            else if (item is EmailItem)
            {
                return new OutboundEmailTransducer() as AbstractOutboundTransducer<ItemClass>;
            }
            else if (item is MeetingItem)
            {
                return new OutboundMeetingTransducer() as AbstractOutboundTransducer<ItemClass>;
            }
            else if (item is TaskItem)
            {
                return new OutboundTaskTransducer() as AbstractOutboundTransducer<ItemClass>;
            }
            {
                throw new ShouldNotHappenException($"Unknown item type '{item.GetType().FullName}'");
            }
        }

        ///// <summary>
        ///// Return an appropriate outbound transducer for this item.
        ///// </summary>
        ///// <param name="item">The item.</param>
        ///// <returns>An appropriate transducer.</returns>
        //public static AbstractOutboundTransducer GetOutbound(EmailItem item)
        //{
        //    return new OutboundEmailTransducer();
        //}

        ///// <summary>
        ///// Return an appropriate outbound transducer for this item.
        ///// </summary>
        ///// <param name="item">The item.</param>
        ///// <returns>An appropriate transducer.</returns>
        //public static AbstractOutboundTransducer GetOutbound(ContactItem item)
        //{
        //    return new OutboundContactTransducer();
        //}

        ///// <summary>
        ///// Return an appropriate outbound transducer for this item.
        ///// </summary>
        ///// <param name="item">The item.</param>
        ///// <returns>An appropriate transducer.</returns>
        //public static AbstractOutboundTransducer GetOutbound(MeetingItem item)
        //{
        //    return new OutboundMeetingTransducer();
        //}

        ///// <summary>
        ///// Return an appropriate outbound transducer for this item.
        ///// </summary>
        ///// <param name="item">The item.</param>
        ///// <returns>An appropriate transducer.</returns>
        //public static AbstractOutboundTransducer GetOutbound(TaskItem item)
        //{
        //    return new OutboundTaskTransducer();
        //}
    }
}
