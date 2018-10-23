/**
 * Outlook integration for SuiteCRM.
 * @package Outlook integration for SuiteCRM
 * @copyright Simon Brooke simon@journeyman.cc
 * @author Simon Brooke simon@journeyman.cc
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU LESSER GENERAL PUBLIC LICENCE as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU LESSER GENERAL PUBLIC LICENCE
 * along with this program; if not, see http://www.gnu.org/licenses
 * or write to the Free Software Foundation,Inc., 51 Franklin Street,
 * Fifth Floor, Boston, MA 02110-1301  USA
 */
namespace CrmOutlookAddin.Transducers.OutlookToCRM
{
    using System.Collections.Generic;
    using Wrappers;

    public class AbstractOutboundTransducer
    {
        /// <summary>
        /// The marker which may be included in strings to indicate the position of a CRM id value which
        /// is not available at the time of composition.
        /// </summary>
        public const string MissingIdMarker = "::Paste-ID-here::";
    }


    public abstract class AbstractOutboundTransducer<Item> : AbstractOutboundTransducer
        where Item : AbstractItem
    {
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
