namespace CrmOutlookAddin.Core
{
    using CrmOutlookAddin.Logging;
    using CrmOutlookAddin.Wrappers;
    using Exceptions;
    using Microsoft.Office.Interop.Outlook;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Utils;
    using Outlook = Microsoft.Office.Interop.Outlook;

    /// <summary>
    /// The singleton Item Manager, which acts as a factory and broker for wrappers around Outlook items.
    /// </summary>
    public class ItemManager : AbstractItemManager
    {
        /// <summary>
        /// My underlying instance.
        /// </summary>
        private static readonly Lazy<ItemManager> lazy =
            new Lazy<ItemManager>(() => new ItemManager());

        /// <summary>
        /// A public accessor for my instance.
        /// </summary>
        public static ItemManager Instance { get { return lazy.Value; } }

        /// <summary>
        /// You cannot subclass the ItemManager.
        /// </summary>
        private ItemManager() : base() { }
    }
}
