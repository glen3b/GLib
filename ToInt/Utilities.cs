using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glib
{
    /// <summary>
    /// A static class providing convenience utilities on types.
    /// </summary>
    /// <typeparam name="T">The type to provide utilities of.</typeparam>
    public static class TypeUtils<T>
    {
        /// <summary>
        /// Gets a predicate that will always return a true value.
        /// </summary>
        public static Predicate<T> TruePredicate
        {
            get
            {
                return new Predicate<T>(trueValue);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the specified type is numeric.
        /// </summary>
        public static bool IsNumeric
        {
            get
            {
                return Utils.NumericTypes.Contains(typeof(T));
            }
        }

        /// <summary>
        /// Gets a predicate that will always return a false value.
        /// </summary>
        public static Predicate<T> FalsePredicate
        {
            get
            {
                return new Predicate<T>(falseValue);
            }
        }

        private static bool trueValue(T input)
        {
            return true;
        }

        private static bool falseValue(T input)
        {
            return false;
        }
    }

    /// <summary>
    /// A static class providing convenience utilities.
    /// </summary>
    public static class Utils
    {
        
        /// <summary>
        /// Gets an array of numerical types.
        /// </summary>
        public static Type[] NumericTypes{
         get{
             return new Type[] {
            typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong),
            typeof(float), typeof(decimal), typeof(double), typeof(sbyte), typeof(byte)
        };
         }
        }
    }
}
