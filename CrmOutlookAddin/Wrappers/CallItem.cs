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
    using Core;
    using System;
    using Outlook = Microsoft.Office.Interop.Outlook;

    /// <summary>
    /// A wrapper which wraps an appointment which will be considered by CRM to be a 'Call'.
    /// </summary>
    public class CallItem : AbstractAppointmentItem
    {
        public CallItem(Outlook.AppointmentItem item) : base(item)
        {
        }

        public override string DistinctFields
        {
            get
            {
                return $"subject: '{this.Subject}'; start: '{this.Start}'";
            }
        }

        public override bool Synchronisable
        {
            get
            {
                return SyncDirection.AllowOutbound(Properties.Settings.Default.SyncCalls);
            }
        }

        public override void CacheItem()
        {
            throw new NotImplementedException();
        }
    }
}
