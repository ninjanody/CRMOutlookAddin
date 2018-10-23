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
namespace CrmOutlookAddin.Utils
{
    using System;

    /// <summary>
    /// An object whichbarks if it isn't patted regularly enough.
    /// </summary>
    public abstract class Watchdog : RepeatingProcess
    {
        /// <summary>
        /// The interval after the last pat at which I bark.
        /// </summary>
        private readonly TimeSpan timeout;

        /// <summary>
        /// When I was last patted
        /// </summary>
        private DateTime lastPat = DateTime.Now;

        /// <summary>
        /// Create a new instance of watchdog with this name and timeout.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="timeout">The timeout.</param>
        public Watchdog(string name, long timeout) : this(name, new TimeSpan(timeout)) { }

        public Watchdog(string name, TimeSpan timeout) : base(name)
        {
            this.Interval = new TimeSpan(timeout.Ticks / 10);
            this.timeout = timeout;
        }

        /// <summary>
        /// Do whatever I need to do when my timeout has elapsed.
        /// </summary>
        public abstract void Bark();

        /// <summary>
        /// Reset my countdown start time.
        /// </summary>
        public void Pat()
        {
            this.lastPat = DateTime.Now;
        }

        /// <summary>
        /// If my timeout has elapsed, bark and stop.
        /// </summary>
        internal override void PerformIteration()
        {
            if (lastPat + timeout > DateTime.Now)
            {
                this.Bark();
                this.Stop();
            }
        }
    }
}
