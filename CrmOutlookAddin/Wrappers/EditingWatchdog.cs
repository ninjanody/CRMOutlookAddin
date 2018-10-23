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
    using Exceptions;
    using System;
    using Utils;

    /// <summary>
    /// A watchdog which if not patted often enough sets the state of its wrapper to 'Pending'
    /// </summary>
    /// <remarks>
    /// The 'Editing' state is special in so much as we are not told when the user ceases editing.
    /// So we operate a timeout.
    /// </remarks>
    public class EditingWatchdog : Watchdog
    {
        /// <summary>
        /// The wrapper for which I am a watchdog
        /// </summary>
        private AbstractItem watched;

        public EditingWatchdog(AbstractItem wrapper) : base("EW", new TimeSpan(0, 2, 0))
        {
            this.watched = wrapper;
        }

        public override void Bark()
        {
            try
            {
                watched.SetPending();
            }
            catch (BadStateTransition bst)
            {
                Log.Warn(bst.Message);
            }
        }
    }
}
