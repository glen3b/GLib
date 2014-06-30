using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glib
{
    /// <summary>
    /// Compares string instances in a case-insensitive manner.
    /// </summary>
    public sealed class CaseInsensitiveStringComparison : IEqualityComparer<String>
    {
        private CaseInsensitiveStringComparison() { }

        private static CaseInsensitiveStringComparison _instance = new CaseInsensitiveStringComparison();

        /// <summary>
        /// Gets the singleton instance of this case-insensitive string comparison utility.
        /// </summary>
        public static IEqualityComparer<String> Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Determines if two strings are equal, ignoring case.
        /// </summary>
        /// <param name="x">The first string.</param>
        /// <param name="y">The second string.</param>
        /// <returns>If the two strings are equal disregarding case of the characters they contain.</returns>
        public bool Equals(string x, string y)
        {
            if (x == null)
            {
                return y == null;
            }
            if (y == null)
            {
                return x == null;
            }

            return x.Equals(y, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Gets a case-insensitive hash code of the specified string.
        /// </summary>
        /// <param name="obj">The string for which the hash code will be computed.</param>
        /// <returns>The hash code of the uppercase converted version of the specified string, or 0 if the string is null.</returns>
        public int GetHashCode(string obj)
        {
            return obj == null ? 0 : obj.ToUpperInvariant().GetHashCode();
        }
    }
}
