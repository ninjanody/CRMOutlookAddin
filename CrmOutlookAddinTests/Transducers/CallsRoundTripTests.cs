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
namespace CrmOutlookAddinTests.Transducers
{
    using NUnit.Framework;
    using CrmOutlookAddin.Transducers.CRMToOutlook;

    /// <summary>
    /// Summary description for CallsRoundTripTests
    /// </summary>
    [TestFixture()]
    public class CallsRoundTripTests
    {
        /// <summary>
        /// Genuine get_entry_list request, extracted from logs.
        /// </summary>
        const string getEntryListRequest = "{\"session\":\"21979a8c187226ee8d3aeeea40cea7a0\",\"module_name\":\"Calls\",\"query\":\"assigned_user_id = 'efb46b6b-1f42-1183-9ef7-59ef08735655'\",\"order_by\":\"date_start DESC\",\"offset\":0,\"select_fields\":[\"id\",\"name\",\"description\",\"date_start\",\"date_end\",\"date_modified\",\"duration_minutes\",\"duration_hours\"],\"max_results\":\"0\",\"deleted\":false,\"favorites\":false}";

        /// <summary>
        /// Genuine result of a get_entry_list call, extracted from logs, slightly massaged for size.
        /// </summary>
        const string getEntryListResult = "{\"result_count\":1,\"total_count\":\"45\",\"next_offset\":2,\"entry_list\":[{\"id\":\"8aa192d2-a848-bc4a-aea1-b08a5e9194dc\",\"module_name\":\"Calls\",\"name_value_list\":{\"id\":{\"name\":\"id\",\"value\":\"8aa192d2-a848-bc4a-aea1-b08a5e9194dc\"},\"name\":{\"name\":\"name\",\"value\":\"KS10 Appointment created in Outlook\"},\"description\":{\"name\":\"description\",\"value\":\"  \n\"},\"date_start\":{\"name\":\"date_start\",\"value\":\"2018-04-28 21:00:00\"},\"date_end\":{\"name\":\"date_end\",\"value\":\"2018-04-28 21:30:00\"},\"date_modified\":{\"name\":\"date_modified\",\"value\":\"2018-04-27 13:36:56\"},\"duration_minutes\":{\"name\":\"duration_minutes\",\"value\":\"30\"},\"duration_hours\":{\"name\":\"duration_hours\",\"value\":\"0\"}}}]}";

        /// <summary>
        /// A single entry extracted from the above list.
        /// </summary>
        const string getEntryResult = "{\"id\":\"8aa192d2-a848-bc4a-aea1-b08a5e9194dc\",\"module_name\":\"Calls\",\"name_value_list\":{\"id\":{\"name\":\"id\",\"value\":\"8aa192d2-a848-bc4a-aea1-b08a5e9194dc\"},\"name\":{\"name\":\"name\",\"value\":\"KS10 Appointment created in Outlook\"},\"description\":{\"name\":\"description\",\"value\":\"  \n\"},\"date_start\":{\"name\":\"date_start\",\"value\":\"2018-04-28 21:00:00\"},\"date_end\":{\"name\":\"date_end\",\"value\":\"2018-04-28 21:30:00\"},\"date_modified\":{\"name\":\"date_modified\",\"value\":\"2018-04-27 13:36:56\"},\"duration_minutes\":{\"name\":\"duration_minutes\",\"value\":\"30\"},\"duration_hours\":{\"name\":\"duration_hours\",\"value\":\"0\"}}}";

        string[] setEntryExpected = new string[] 
        { ""
        };

        public CallsRoundTripTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [Test()]
        public void CRMToOutlookTestMany()
        {
            InboundCallTransducer transducer = new InboundCallTransducer(new TestableItemManager());

            var result = transducer.JsonToItems(getEntryListResult);

            Assert.AreEqual(1, result.Count, 0, "Should read one record.");
            Assert.AreEqual("KS10 Appointment created in Outlook", result[0].Subject);
        }

        [Test()]
        public void CRMToOutlookTest1()
        {
            InboundCallTransducer transducer = new InboundCallTransducer(new TestableItemManager());

            var result = transducer.JsonToItem(getEntryResult);
            Assert.AreEqual("KS10 Appointment created in Outlook", result.Subject);
            Assert.AreEqual(30, result.Duration);
        }
    }
}
