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
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    /// <summary>
    ///     Utility functions mainly concerned with the formatting of strings.
    /// </summary>
    public class StringUtils
    {
        /// <summary>
        ///     True if `expression` is a number, else false.
        /// </summary>
        /// <remarks>
        ///     Taken from https://stackoverflow.com/questions/1130698/checking-if-an-object-is-a-number-in-c-sharp
        /// </remarks>
        /// <param name="expression">The object to test.</param>
        /// <returns>True if `expression` is a number, else false.</returns>
        public static bool IsNumber(object expression)
        {
            if (expression == null)
                return false;

            double number;
            return double.TryParse(
                Convert.ToString(expression, CultureInfo.InvariantCulture),
                NumberStyles.Any,
                NumberFormatInfo.InvariantInfo,
                out number);
        }

        /// <summary>
        ///     Produce, from these fields, a canonical string.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Sometimes we need to resolve CRM items to Outlook items where we don't
        ///         have a foreign id on either side; so we need to resolve on distinct fields. An object
        ///         will be deemed to match if the values of all the distinct fields on both sides match
        ///         (note that, for Contacts, we need to do something more sophisticated than this, since
        ///         a contact should probably be considered to be 'the same' if either the email address
        ///         or the phone number match).
        ///     </para>
        ///     <para>
        ///         The canonical name of a field is all lower case, with words separated by
        ///         underscores. The canonical value of a field is as follows:
        ///     </para>
        ///     <para>
        ///         for a string, the string surrounded by single quotes, with each embedded single
        ///         quote prefixed by a backslash character, and each embedded backslash character preceded
        ///         by an additional backslash character;
        ///     </para>
        ///     <para>for an integer, the base 10 representation of the integer;</para>
        ///     <para>for a real number, the base 10 decimal representation of the number;</para>
        ///     <para>for a date, the ISO8601 UTC representation of the date, surrounded by single quotes.</para>
        ///     <para>
        ///         TODO: does not yet implement backslash escapes.
        ///     </para>
        /// </remarks>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static string CanonicaliseFields(Dictionary<string, object> fields)
        {
            var bob = new StringBuilder();

            foreach (var key in fields.Keys.OrderBy(x => x))
            {
                string value;
                var v = fields[key];

                if (IsNumber(v))
                    value = $"{v}";
                else if (v is DateTime)
                    value = $"'{((DateTime) v).ToUniversalTime():s}'";
                else
                    value = $"'{v}'";

                bob.Append($"{key.ToLower()}: {value}; ");
            }

            return bob.ToString();
        }
    }
}