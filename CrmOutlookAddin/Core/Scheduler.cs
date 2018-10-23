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
    using System;
    using CrmOutlookAddin.Logging;

    /// <summary>
    ///     The single scheduler.
    /// </summary>
    public class Scheduler
    {
        /// <summary>
        ///     My underlying instance.
        /// </summary>
        private static readonly Lazy<Scheduler> lazy =
            new Lazy<Scheduler>(() => new Scheduler());

        /// <summary>
        ///     A lock on creating new items.
        /// </summary>
        private object creationLock = new object();

        /// <summary>
        ///     A log, to log stuff to.
        /// </summary>
        private Log log = Log.Instance;

        /// <summary>
        ///     A public accessor for my instance.
        /// </summary>
        public static Scheduler Instance => lazy.Value;
    }
}