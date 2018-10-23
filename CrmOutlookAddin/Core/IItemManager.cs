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
namespace CrmOutlookAddin.Core
{
    using System.Collections.Generic;
    using Wrappers;

    /// <summary>
    /// In production, only one singleton ItemManager instance shall be used; this interface
    /// is to allow test item managers to be injected in tests.
    /// </summary>
    public interface IItemManager
    {
        /// <summary>
        /// Get the item with this CRM id from the item manager.
        /// </summary>
        /// <param name="crmId">A CRM id.</param>
        /// <param name="type">The expected type of the item.</param>
        /// <returns>The wrapper which wraps the Outlook item with the id indicated</returns>
        AbstractItem GetByCrmId(string crmId, ItemType type);

        /// <summary>
        /// All the items I hold.
        /// </summary>
        /// <returns>All the items I hold.</returns>
        ICollection<AbstractItem> AllItems { get; }

        /// <summary>
        /// Get the object indicated by these distinct fields from the item manager
        /// </summary>
        /// <param name="fields">The fieldnames/values</param>
        /// <param name="type">The expected type of the item.</param>
        /// <returns>The wrapper which wraps the Outlook item with the distinct fields indicated</returns>
        AbstractItem GetByDistinctFields(Dictionary<string, object> fields, ItemType type);

        /// <summary>
        /// Get the object indicated by these distinct fields from the item manager.
        /// </summary>
        /// <param name="canonicalFields">A string representing the fieldnames/values in canonical order.</param>
        /// <param name="type">The expected type of the item.</param>
        /// <returns>The wrapper which wraps the Outlook item with the distinct fields indicated</returns>
        AbstractItem GetByDistinctFields(string canonicalFields, ItemType type);

        /// <summary>
        /// Get the item with this Outlook id from the item manager.
        /// </summary>
        /// <param name="outlookId">An Outlook id.</param>
        /// <param name="type">The expected type of the item.</param>
        /// <returns>The wrapper which wraps the Outlook item with the id indicated</returns>
        AbstractItem GetByOutlookId(string outlookId, ItemType type);

        /// <summary>
        /// Remove this wrapper object from the objects I manage.
        /// </summary>
        /// <param name="abstractWrapper">the object to remove.</param>
        void RemoveWrapper(AbstractItem abstractWrapper);
    }
}
