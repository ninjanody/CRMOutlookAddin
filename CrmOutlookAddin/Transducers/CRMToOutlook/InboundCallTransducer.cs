namespace CrmOutlookAddin.Transducers.CRMToOutlook
{
    using Wrappers;
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Core;
    using Logging;

    public class InboundCallTransducer : AbstractInboundTransducer<CallItem>
    {
        public InboundCallTransducer()
        {
        }

        public InboundCallTransducer(IItemManager manager) : base(manager) { }

        public override CallItem JsonToItem(string json)
        {
            return ProcessOne(JObject.Parse(json));
        }

        public override IList<CallItem> JsonToItems(string json)
        {
            List<CallItem> result = new List<CallItem>();

            dynamic packet = JObject.Parse(json);

            foreach (var elt in packet.entry_list)
            {
                result.Add(this.ProcessOne(elt));
            }

            return result;
        }

        /// <summary>
        /// Process a single dynamic object parsed from JSON, and presumed to represent a CallItem.
        /// </summary>
        /// <param name="obj">The dynamic object, presumed to represent a CallItem.</param>
        /// <returns>A call item representing the dynamic object</returns>
        private CallItem ProcessOne(dynamic obj)
        {
            DateTime start = DateTime.Now;
            CallItem result = this.manager.GetByCrmId(obj.id.ToString(), ItemType.Call) as CallItem;
            DateTime modified = DateTime.ParseExact(obj.name_value_list.date_modified.value.ToString(), "yyyy-MM-dd HH:mm:ss", null);

            /* if the call item is new, its LastModified will be later than start, in which case it 
             * needs to be populated. Otherwise, if the CRM item has changed more recently than the
             * Outlook item, the latter needs to be updated. */
            if (start <= result.LastModified || modified > result.LastModified)
            {
                result.Subject = obj.name_value_list.name.value.ToString();
                result.Body = obj.name_value_list.description.value.ToString();
                result.StartUTC = DateTime.ParseExact(obj.name_value_list.date_start.value.ToString(), "yyyy-MM-dd HH:mm:ss", null);
                DateTime end = DateTime.ParseExact(obj.name_value_list.date_end.value.ToString(), "yyyy-MM-dd HH:mm:ss", null);
                
                result.Duration = end.Subtract(result.StartUTC).Minutes;
            }
            // TODO: else flag a possible conflict to the user.

            return result;
        }
    }
}
