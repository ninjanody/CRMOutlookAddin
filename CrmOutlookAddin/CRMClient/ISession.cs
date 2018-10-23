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

using CrmOutlookAddin.Exceptions;

namespace CrmOutlookAddin.CRMClient
{
    /// <summary>
    /// An interface representing a session with a CRM server.
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// Close this session, assuming it to be open; if it is not open, do nothing.
        /// </summary>
        /// <returns>True if the session was not already closed.</returns>
        bool Close();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True if this session is open.</returns>
        bool IsOpen();

        /// <summary>
        /// Open this session with the configured server, assuming it to be closed; if it is already closed, does nothing.
        /// </summary>
        /// <remarks>
        /// Will throw exception if authentication fails, but the details are yet to be worked out.
        /// </remarks>
        /// <returns>True if the session was opened.</returns>
        bool Open();

        /// <summary>
        /// Transmit this value to the CRM server and return its response.
        /// </summary>
        /// <param name="v">The value to transmit</param>
        /// <returns>The response returned</returns>
        /// <exception cref="TransportLayerException">if communication with CRM fails completely.</exception>
        /// <exception cref="CRMException">if CRM reports an error.</exception>
        string Transmit(string v);
    }
}
