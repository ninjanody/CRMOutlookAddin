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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmOutlookAddin.Exceptions
{
    /// <summary>
    /// An exception which represents an error returned by CRM.
    /// </summary>
    public class CRMException : Exception
    {
        /// <summary>
        /// The CRM error number
        /// </summary>
        public readonly int Number;

        /// <summary>
        /// Create a new instance of CRMException.
        /// </summary>
        /// <param name="number">The error number reported by CRM.</param>
        /// <param name="message">The error message rerported by CRM.</param>
        internal CRMException(int number, string message) : base(message)
        {
            this.Number = number;
        }
    }
}
