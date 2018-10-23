using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Outlook;
using Moq;

namespace CrmOutlookAddinTests.Mocks
{
    /// <summary>
    /// Is this really all I need to do to mock a mail item?
    /// </summary>
    internal class EmailItem : Mock<MailItem>
    {
    }
}
