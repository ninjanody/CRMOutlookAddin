namespace CrmOutlookAddin.Wrappers
{
    using Logging;
    using Microsoft.Office.Interop.Outlook;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A wrapper for an Outlook RecipientItem.
    /// </summary>
    public class RecipientWrapper : AbstractItem
    {
        /// <summary>
        /// A cache of all SMTP addresses we've already established.
        /// </summary>
        private static Dictionary<Recipient, string> smtpAddressCache = new Dictionary<Recipient, string>();

        /// <summary>
        /// The actual recipient COM object which I wrap.
        /// </summary>
        private Recipient recipient;

        public RecipientWrapper(Recipient recipient)
        {
            this.recipient = recipient;
        }

        /// <summary>
        /// A recipient is never stored in CRM as such, so this is probably not needed.
        /// </summary>
        /// <remarks>
        /// If it is needed, we also need the module it is stored in (recipients may be
        /// CRM `Users`, `Contacts` or `Leads`, and may possibly also be in custom
        /// modules).
        /// </remarks>
        public override string CrmEntryId
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string DistinctFields
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                return recipient.Name;
            }
        }

        public override string OutlookId
        {
            get
            {
                return recipient.EntryID;
            }
        }

        /// <summary>
        /// Return the SMTP address of this recipient, from the cache if possible.
        /// </summary>
        public string SMTPAddress
        {
            get
            {
                string result = string.Empty;

                try
                {
                    result = smtpAddressCache[recipient];
                }
                catch (KeyNotFoundException)
                {
                    switch (recipient.AddressEntry.Type)
                    {
                        case "SMTP":
                            result = recipient.Address;
                            break;

                        case "EX": /* an Exchange address */
                            var exchangeUser = recipient.AddressEntry.GetExchangeUser();
                            if (exchangeUser != null)
                            {
                                result = exchangeUser.PrimarySmtpAddress;
                            }
                            break;

                        default:
                            Log.Instance.Warn(
                                $"RecipientExtensions.GetSmtpAddres: unknown email type {recipient.AddressEntry.Type}");
                            break;
                    }

                    if (!string.IsNullOrEmpty(result))
                    {
                        RecipientWrapper.smtpAddressCache[recipient] = result;
                    }
                }

                return result;
            }
        }

        public override void CacheItem()
        {
            throw new NotImplementedException();
        }
    }
}
