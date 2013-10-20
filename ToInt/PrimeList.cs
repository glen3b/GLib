using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glib
{
    /// <summary>
    /// A list of purely prime numbers.
    /// </summary>
    /// <typeparam name="T">A whole number numerical type.</typeparam>
    public class PrimeList<T> : List<T>
    {
        /// <summary>
        /// Create a new prime number list.
        /// </summary>
        public PrimeList() : base()
        {
            if(typeof(T) == typeof(ulong) || typeof(T) == typeof(uint) || typeof(T) == typeof(ushort)){

            }else{
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
            for(ulong si = ulong.Parse(startpoint.ToString());si < ulong.Parse(endpoint.ToString()); si++)
            {
                Add(si.Cast<T>());
            }

        }

        /// <summary>
        /// Access the number at the specified index in the array.
        /// </summary>
        /// <param name="index">The zero-based index in the list.</param>
        /// <returns>The number with the specified index in the list.</returns>
        public new T this[int index]
        {
            get { return base[index]; }
            set {
                if (!IsPrime(value))
                {
                    throw new ArgumentException("Number " + value + " is not prime");
                }
                else
                {
                    base[index] = value;
                }
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

        /// <summary>
        /// Add the specified item to the list.
        /// </summary>
        /// <param name="value">The item to add.</param>
        public new void Add(T value)
        {
            if (IsPrime(value))
            {
                base.Add(value);
            }
        }

        /// <summary>
        /// Insert an item at a specified index into the list.
        /// </summary>
        /// <param name="index">The index at which to insert the item.</param>
        /// <param name="value">The value to insert.</param>
        public new void Insert(int index, T value)
        {
            if (IsPrime(value))
            {
                base.Insert(index, value);
            }
        }

        /// <summary>
        /// Insert a range of values to this list beginning at the specified index.
        /// </summary>
        /// <param name="index">The index to begin inserting values at.</param>
        /// <param name="values">The enumerable of values to insert.</param>
        public new void InsertRange(int index, IEnumerable<T> values)
        {
            int trueI = 0;
            for (int i = 0; i < values.ToArray().Length; i++)
            {
                if (IsPrime(values.ElementAt(i)))
                {
                    base.Insert(index + trueI, values.ElementAt(i));
                    trueI++;
                }
            }
        }

        /// <summary>
        /// Add a range of values to this PrimeList.
        /// </summary>
        /// <param name="values">The values to add.</param>
        public new void AddRange(IEnumerable<T> values)
        {
            foreach (T value in values)
            {
                Add(value);
            }
        }
    }
}
