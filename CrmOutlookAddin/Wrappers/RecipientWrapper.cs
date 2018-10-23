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

        /// <summary>
        /// We don't synchronise recipients as such.
        /// </summary>
        public override bool Synchronisable
        {
            get
            {
                return false;
            }
        }

        public override void CacheItem()
        {
            throw new NotImplementedException();
        }
    }
}
