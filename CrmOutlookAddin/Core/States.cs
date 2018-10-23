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
    /// <summary>
    /// States a Wrapper object can be in with regard to transmission and synchronisation
    /// with CRM.
    /// </summary>
    public enum States
    {
        /// <summary>
        /// New and as yet unassigned.
        /// </summary>
        New,

        /// <summary>
        /// This is a new Wrapper wrapping an Outlook item which has just been created.
        /// </summary>
        NewFromOutlook,

        /// <summary>
        /// This is a Wrapper representing an outlook item which was present when
        /// Outlook was started.
        /// </summary>
        PresentAtStartup,

        /// <summary>
        /// This is a Wrapper object representing a outlook item which has just been
        /// created from a CRM item.
        /// </summary>
        NewFromCRM,

        /// <summary>
        /// A change has been registered on the item wrapped, but we're not clear that
        /// the edit is completed.
        /// </summary>
        Editing,

        /// <summary>
        /// A change has been registered on the item wrapped and we think it's complete, but it has
        /// not been transmitted.
        /// </summary>
        Pending,

        /// <summary>
        /// This Wrapper has been queued for transmission but has not yet been
        /// transmitted.
        /// </summary>
        Queued,

        /// <summary>
        /// The Outlook item associated with this Wrapper has been transmitted,
        /// but no confirmation has yet been received that it has been accepted.
        /// </summary>
        Transmitted,

        /// <summary>
        /// The Outlook item associated with this Wrapper has been transmitted
        /// and accepted by CRM.
        /// </summary>
        Synced,

        /// <summary>
        /// A state is put into state PendingDeletion if it is not found in CRM at
        /// one synchronisation run; if it is not found in the subsequent run and is
        /// still in state PendingDeletion, then it should be deleted.
        /// </summary>
        PendingDeletion,

        /// <summary>
        /// The Wrapper is in an invalid state and should never be synced.
        /// </summary>
        Invalid
    }
}
