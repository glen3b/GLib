using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glib.Collections
{
    /// <summary>
    /// Provides extension methods on collections.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Determines whether the specified enumerable contains any of the specified elements.
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="enumerable">The enumerable to check containment in.</param>
        /// <param name="elements">The elements to check containment of.</param>
        /// <returns>Whether any of the elements are present in the enumerable.</returns>
        public static bool ContainsAny<T>(this IEnumerable<T> enumerable, params T[] elements)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (elements == null) throw new ArgumentNullException("elements");

            foreach (T item in elements)
            {
                if (enumerable.Contains(item)) { return true; }
            }

            return false;
        }

        /// <summary>
        /// Determines whether the specified arrays' contents are equal.
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="enumerable">The left-hand side array.</param>
        /// <param name="elements">The right-hand side array.</param>
        /// <returns>Whether the contents of the arrays are equal.</returns>
        public static bool ContentsEqual<T>(this T[] enumerable, params T[] elements)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (elements == null) throw new ArgumentNullException("elements");
            if (enumerable.Length != elements.Length) return false;

            for (int i = 0; i < enumerable.Length; i++)
            {
                if (enumerable[i] == null ? elements[i] != null : !enumerable[i].Equals(elements[i])) return false;
            }

            return true;
        }

        /// <summary>
        /// Determines whether the specified enumerable contains all of the specified elements.
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="enumerable">The enumerable to check containment in.</param>
        /// <param name="elements">The elements to check containment of.</param>
        /// <returns>Whether all of the elements are present in the enumerable.</returns>
        public static bool ContainsAll<T>(this IEnumerable<T> enumerable, params T[] elements)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (elements == null) throw new ArgumentNullException("elements");

            foreach (T item in elements)
            {
                if (!enumerable.Contains(item)) { return false; }
            }

            return true;
        }
    }
}
