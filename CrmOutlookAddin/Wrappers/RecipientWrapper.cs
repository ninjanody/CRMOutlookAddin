using CrmOutlookAddin.Logging;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;

namespace CrmOutlookAddin.Wrappers
{
    public class RecipientWrapper : AbstractItem
    {
        private static Dictionary<Recipient, string> smtpAddressCache = new Dictionary<Recipient, string>();
        private Recipient recipient;

        public RecipientWrapper(Recipient recipient)
        {
            this.recipient = recipient;
        }

        public override string Description
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
                        smtpAddressCache[recipient] = result;
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
