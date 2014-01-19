using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Glib
{
    /// <summary>
    /// A list of purely prime numbers.
    /// </summary>
    /// <typeparam name="T">A whole number numerical type.</typeparam>
    public class PrimeList<T> : Collection<T>
    {
        /// <summary>
        /// Insterts an item at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="item">The item.</param>
        protected override void InsertItem(int index, T item)
        {
            if (IsPrime(item))
            {
                base.InsertItem(index, item);
            }
        }

        /// <summary>
        /// Sets the item at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="item">The item.</param>
        protected override void SetItem(int index, T item)
        {
            if (IsPrime(item))
            {
                base.SetItem(index, item);
            }
        }

        /// <summary>
        /// Create a new prime number list.
        /// </summary>
        public PrimeList()
            : base()
        {
            if (typeof(T) == typeof(ulong) || typeof(T) == typeof(uint) || typeof(T) == typeof(ushort))
            {

            }
            else
            {
                throw new ArgumentException("Please use a UINT, ULONG, or USHORT type.");
            }

        }

        /// <summary>
        /// Make a new list with all prime numbers from startpoint to endpoint.
        /// </summary>
        /// <param name="startpoint">The inclusive lower number.</param>
        /// <param name="endpoint">The exclusive upper number.</param>
        public PrimeList(T startpoint, T endpoint)
            : this()
        {
            for (ulong si = ulong.Parse(startpoint.ToString()); si < ulong.Parse(endpoint.ToString()); si++)
            {
                Add(si.Cast<T>());
            }

        }

        /// <summary>
        /// Returns whether or not the specified number is prime.
        /// </summary>
        /// <param name="value">The number to check primeness.</param>
        /// <exception cref="ArgumentException">Thrown if value is not a valid number.</exception>
        /// <returns>Whether or not the specified number is prime</returns>
        public static bool IsPrime(object value)
        {
            ulong checkValue;
            ulong sqrt;
            try
            {
                checkValue = ulong.Parse(value.ToString());
                sqrt = Math.Sqrt(checkValue).ToUnsignedLong();
            }
            catch
            {
                throw new ArgumentException("'value' is an invalid number.");
            }

            if (checkValue == 0 || checkValue == 1)
            {
                return false;
            }
            if (checkValue == 2)
            {
                return true;
            }

            if (checkValue % 2 == 0)
            {
                return false;
            }

            for (ulong i = 3; i <= sqrt; i += 2)
            {
                if (checkValue % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
