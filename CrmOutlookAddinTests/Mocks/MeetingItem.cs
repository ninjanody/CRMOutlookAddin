using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Outlook;
using Moq;

namespace CrmOutlookAddinTests.Mocks
{
    internal class MeetingItem : Mock<AppointmentItem>
    {
    }
}
