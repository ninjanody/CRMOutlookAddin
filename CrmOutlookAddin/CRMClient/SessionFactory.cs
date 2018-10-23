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

using CrmOutlookAddin.Properties;

namespace CrmOutlookAddin.CRMClient
{
    /// <summary>
    ///     Creates sessions of the right kind. Not sure 'Factory' is the right name,
    ///     since there is at any one time only one session.
    /// </summary>
    public static class SessionFactory
    {
        private static ISession currentSession;

        private static readonly object Padlock = new object();

        /// <summary>
        ///     Get a session, creating it only if necessary (i.e., no current session exists).
        /// </summary>
        /// <returns>A session</returns>
        public static ISession GetSession()
        {
            ISession result;

            lock (Padlock)
            {
                result = currentSession ?? (Settings.Default.IsLDAPAuthentication
                             ? new LDAPAuthenticatedSession() as ISession
                             : new CRMAuthenticatedSession());

                result.Open();

                currentSession = result;
            }

            return result;
        }

        /// <summary>
        ///     Get a new session, closing any existing one first.
        /// </summary>
        /// <remarks>
        ///     Use case for this is when the user has made changes to settings.
        /// </remarks>
        /// <returns>A session.</returns>
        public static ISession NewSession()
        {
            lock (Padlock)
            {
                if (currentSession != null)
                {
                    currentSession.Close();
                    currentSession = null;
                }
            }
            return GetSession();
        }
    }
}