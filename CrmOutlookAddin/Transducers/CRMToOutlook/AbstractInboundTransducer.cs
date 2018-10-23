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
namespace CrmOutlookAddin.Transducers.CRMToOutlook
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
