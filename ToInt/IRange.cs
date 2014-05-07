using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glib
{
    /// <summary>
    /// Represents a range of values.
    /// </summary>
    /// <typeparam name="T">The type of the values within the range.</typeparam>
    public interface IRange<T> where T : IComparable
    {
        /// <summary>
        /// Represents the inclusive start of the range.
        /// </summary>
        T Start { get; }
        /// <summary>
        /// Represents the exclusive end of the range.
        /// </summary>
        T End { get; }
        /// <summary>
        /// Determines whether the specified value is included within this range.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>Whether value is within the bounds of this range.</returns>
        bool Includes(T value);
        /// <summary>
        /// Determines whether the specified range of values is included within this range.
        /// </summary>
        /// <param name="range">The range of values to check.</param>
        /// <returns>Whether range is included within this range.</returns>
        bool Includes(IRange<T> range);
    }

    /// <summary>
    /// Represents a range of integers.
    /// </summary>
    public struct IntegerRange : IRange<int>
    {
        /// <summary>
        /// Computes the hash code of this object.
        /// </summary>
        /// <returns>This object's hash code.</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = (int)2166136261;
                // Suitable nullity checks etc, of course :)
                hash = hash * 16777619 ^ Start.GetHashCode();
                hash = hash * 16777619 ^ End.GetHashCode();
                return hash;
            }
        }

        private int _start;

        /// <summary>
        /// Gets the inclusive start of the range.
        /// </summary>
        public int Start
        {
            get { return _start; }
        }

        private int _end;

        /// <summary>
        /// Gets the exclusive end of the range.
        /// </summary>
        public int End
        {
            get { return _end; }
            set { _end = value; }
        }

        /// <summary>
        /// Creates a range of integers.
        /// </summary>
        /// <param name="start">The starting value.</param>
        /// <param name="end">The ending value.</param>
        public IntegerRange(int start, int end)
        {
            if (start >= end)
            {
                throw new ArgumentException("The start time is not compatible with the end time.");
            }
            _start = start;
            _end = end;
        }

        /// <summary>
        /// Determines whether the specified value is included within this range.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>Whether value is within the bounds of this range.</returns>
        public bool Includes(int value)
        {
            return value >= Start && value < End;
        }
        /// <summary>
        /// Determines whether the specified range of values is included within this range.
        /// </summary>
        /// <param name="range">The range of values to check.</param>
        /// <returns>Whether range is included within this range.</returns>
        public bool Includes(IRange<int> range)
        {
            return (Start <= range.Start) && (range.End <= End);
        }

        /// <summary>
        /// Determines whether these objects are equal.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns>Whether these objects represent equal ranges.</returns>
        public override bool Equals(object obj)
        {
            IRange<int> range = obj as IRange<int>;

            if (range == null)
            {
                return false;
            }

            return Start.Equals(range.Start) && End.Equals(range.End);
        }

        /// <summary>
        /// Returns a string representation of this integer range.
        /// </summary>
        /// <returns>A string representation of this range.</returns>
        public override string ToString()
        {
            return "IntegerRange{" + String.Format("{0} -> {1}", Start, End) + "}";
        }
    }

    /// <summary>
    /// Represents a range of <see cref="DateTime"/> instances.
    /// </summary>
    public struct DateRange : IRange<DateTime>
    {
        /// <summary>
        /// Creates a range of dates.
        /// </summary>
        /// <param name="start">The starting date.</param>
        /// <param name="end">The ending date.</param>
        public DateRange(DateTime start, DateTime end)
        {
            if (start >= end)
            {
                throw new ArgumentException("The start time is not compatible with the end time.");
            }
            _start = start;
            _end = end;
        }

        /// <summary>
        /// Computes the hash code of this object.
        /// </summary>
        /// <returns>This object's hash code.</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = (int)2166136261;
                // Suitable nullity checks etc, of course :)
                hash = hash * 16777619 ^ Start.GetHashCode();
                hash = hash * 16777619 ^ End.GetHashCode();
                return hash;
            }
        }

        private DateTime _start;

        /// <summary>
        /// Gets the inclusive start of the range.
        /// </summary>
        public DateTime Start
        {
            get { return _start; }
        }

        private DateTime _end;

        /// <summary>
        /// Gets the exclusive end of the range.
        /// </summary>
        public DateTime End
        {
            get { return _end; }
            set { _end = value; }
        }

        /// <summary>
        /// Determines whether the specified value is included within this range.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>Whether value is within the bounds of this range.</returns>
        public bool Includes(DateTime value)
        {
            return value >= Start && value < End;
        }
        /// <summary>
        /// Determines whether the specified range of values is included within this range.
        /// </summary>
        /// <param name="range">The range of values to check.</param>
        /// <returns>Whether range is included within this range.</returns>
        public bool Includes(IRange<DateTime> range)
        {
            return (Start <= range.Start) && (range.End <= End);
        }

        /// <summary>
        /// Returns a string representation of this date range.
        /// </summary>
        /// <returns>A string representation of this range.</returns>
        public override string ToString()
        {
            return "DateRange{" + String.Format("{0} -> {1}", Start, End) + "}";
        }

        /// <summary>
        /// Determines whether these objects are equal.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns>Whether these objects represent equal ranges.</returns>
        public override bool Equals(object obj)
        {
            IRange<DateTime> range = obj as IRange<DateTime>;

            if (range == null)
            {
                return false;
            }

            return range.Start == Start && range.End == End;
        }
    }
}
