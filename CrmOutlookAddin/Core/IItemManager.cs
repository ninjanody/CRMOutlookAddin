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
        /// <param name="outlookId">An outlook id.</param>
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
