﻿namespace CrmOutlookAddin.Transducers.CRMToOutlook
{
    using Core;
    using Wrappers;
    using System.Collections.Generic;

    public class AbstractInboundTransducer
    {
        protected readonly IItemManager manager;

        public AbstractInboundTransducer()
        {
            manager = ItemManager.Instance;
        }

        public AbstractInboundTransducer(IItemManager manager)
        {
            this.manager = manager;
        }
    }


    public abstract class AbstractInboundTransducer<Item> : AbstractInboundTransducer
        where Item : AbstractItem
    {
        public AbstractInboundTransducer() : base() { }

        public AbstractInboundTransducer(IItemManager manager) : base(manager) { }

        /// <summary>
        /// Attempt to parse a single item from a string assumed to be one returned from a `get_entry` call.
        /// </summary>
        /// <param name="json">The JSON string to parse.</param>
        /// <returns>The item parsed.</returns>
        public abstract Item JsonToItem(string json);

        /// <summary>
        /// Attempt to parse a list of items from a string assumed to be one returned from a `get_entry_list` call.
        /// </summary>
        /// <param name="json">The JSON string to parse.</param>
        /// <returns>The list of items parsed.</returns>
        public abstract IList<Item> JsonToItems(string json);
    }
}
