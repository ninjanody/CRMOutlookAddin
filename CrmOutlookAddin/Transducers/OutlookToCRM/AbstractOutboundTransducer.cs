namespace CrmOutlookAddin.Transducers.OutlookToCRM
{
    using System.Collections.Generic;
    using Wrappers;

    public class AbstractOutboundTransducer
    {
    }


    public abstract class AbstractOutboundTransducer<Item> : AbstractOutboundTransducer
        where Item : AbstractItem
    {
        /// <summary>
        /// The marker which may be included in strings to indicate the position of a CRM id value which
        /// is not available at the time of composition.
        /// </summary>
        public const string missingIdMarker = "::Paste-ID-here::";

        /// <summary>
        /// Return a sequence of strings which may be send as POST requests to the CRM server to reproduce in it the item passed.
        /// </summary>
        /// <remarks>
        /// <para>
        /// For each of
        /// </para>
        /// <list type="ordered">
        /// <item>Contact</item>
        /// <item>Task</item>
        /// </list>
        /// <para>Only one `set_entry` string need be passed from Outlook to CRM; however, for each of</para>
        /// <list type="ordered">
        /// <item>Call</item>
        /// <item>Email</item>
        /// <item>Meeting</item>
        /// </list>
        /// <para>the initial `set_entry` call must be followed by one or more `set_relationship` calls. 
        /// Thus this method must be able to return a variable number of strings.</para>
        /// <para>Further, when an object is being sent to CRM the first time, its CRM id will not be known; 
        /// consequently we must have a special marker in the second and subsequent strings, which will be 
        /// substituted for by the value of the id returned by the initial `set_entry` call.</para>
        /// </remarks>
        /// <param name="item">The item to be copied to CRM.</param>
        /// <returns>A sequence of strings as discussed above.</returns>
        public abstract ICollection<string> ItemToJson(Item item);
    }
}
