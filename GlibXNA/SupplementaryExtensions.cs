using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glib
{
    /// <summary>
    /// A class providing extension methods that are present in the core library, which cannot be used on Xbox projects.
    /// </summary>
    #if WINDOWS
    internal static class SupplementaryExtensions
#else
    public static class SupplementaryExtensions
#endif
    {
        /// <summary>
        /// Convert the specified object to an integer.
        /// </summary>
        /// <param name="o">The object to convert to an integer.</param>
        /// <returns>The integer representation of the specified object.</returns>
        public static int ToInt(this object o)
        {
            return Convert.ToInt32(o);
        }

        /// <summary>
        /// Convert an array to a string, using space as a delimiter.
        /// </summary>
        /// <param name="array">The array to convert.</param>
        /// <returns>All elements of the array delimited by delimiter in a string.</returns>
        public static string ToArrayString(this Array array)
        {
            return array.ToArrayString(" ");
        }

        /// <summary>
        /// Convert an array to a string.
        /// </summary>
        /// <param name="array">The array to convert.</param>
        /// <param name="delimiter">The delimiter between elements.</param>
        /// <returns>All elements of the array delimited by delimiter in a string.</returns>
        public static string ToArrayString(this Array array, string delimiter)
        {
            StringBuilder builder = new StringBuilder();
            /*
            int dim = 0;
            bool b;
            do{
                b = true;
                try{
                    array.GetLength(dim);
                }catch{
                    b = false;
                    break;
                }
                dim++;
            }while(b);
            if (dim > 1)
            {
                
                for (int cDim = 0; cDim < dim; cDim++)
                {
                    builder.Append("{");
                    for (int cIndex = 0; cIndex < array.GetLength(dim); cIndex++)
                    {
                        builder.AppendFormat("{0}{1}", array.GetValue(cDim, cIndex), delimiter);
                    }
                    builder.Append("}");
                }
                new String(builder.ToString().Reverse()).R
                return builder.ToString().Substring(0, builder.Length - delimiter.Length);
            }
            else
            {
             */
            foreach (object o in array)
            {
                builder.AppendFormat("{0}{1}", o, delimiter);
            }
            return builder.ToString().Substring(0, builder.Length - delimiter.Length);
            //}

        }

        /// <summary>
        /// Format the specified string, using String.Format(String, Object[]).
        /// </summary>
        /// <param name="s">String.Format(String, Object[])'s first argument.</param>
        /// <param name="formatElements">The array of objects to go into String.Format(String, Object[])'s 2nd argument.</param>
        /// <returns>The formatted string.</returns>
        public static string Format(this string s, params object[] formatElements)
        {
            return String.Format(s, formatElements);
        }

        /// <summary>
        /// Format the specified string, using String.Format(String, Object[]), and sending the results to output.
        /// </summary>
        /// <param name="s">String.Format(String, Object[])'s first argument.</param>
        /// <param name="output">The string to output the results to.</param>
        /// <param name="formatElements">The array of objects to go into String.Format(String, Object[])'s 2nd argument.</param>
        public static void Format(this string s, out string output, params object[] formatElements)
        {
            output = String.Format(s, formatElements);
        }


        /// <summary>
        /// Raise the specified number to a power.
        /// </summary>
        /// <param name="d">The number to raise to a power.</param>
        /// <param name="power">The power to raise a number to.</param>
        /// <returns>The number risen to a power.</returns>
        public static double RaiseToPower(this double d, double power)
        {
            return Math.Pow(d, power);
        }

        /// <summary>
        /// Raise the specified number to the specified power.
        /// </summary>
        /// <param name="d">The number to raise to a power.</param>
        /// <param name="power">The power to raise the number to.</param>
        /// <returns>The number risen to the specified power.</returns>
        public static float RaiseToPower(this float d, double power)
        {
            return Math.Pow(d, power).ToFloat();
        }

        /// <summary>
        /// Raise the specified number to the specified power.
        /// </summary>
        /// <param name="d">The number to raise to a power.</param>
        /// <param name="power">The power to raise the number to.</param>
        /// <returns>The number risen to the specified power.</returns>
        public static short RaiseToPower(this short d, double power)
        {
            return Math.Pow(d, power).ToShort();
        }

        /// <summary>
        /// Raise the specified number to the specified power.
        /// </summary>
        /// <param name="d">The number to raise to a power.</param>
        /// <param name="power">The power to raise the number to.</param>
        /// <returns>The number risen to the specified power.</returns>
        public static int RaiseToPower(this int d, double power)
        {
            return Math.Pow(d, power).ToInt();
        }

        /// <summary>
        /// Raise the specified number to the specified power.
        /// </summary>
        /// <param name="d">The number to raise to a power.</param>
        /// <param name="power">The power to raise the number to.</param>
        /// <returns>The number risen to the specified power.</returns>
        public static decimal RaiseToPower(this decimal d, double power)
        {
            return Math.Pow(d.ToDouble(), power).ToDecimal();
        }

        /// <summary>
        /// Raise the specified number to the specified power.
        /// </summary>
        /// <param name="d">The number to raise to a power.</param>
        /// <param name="power">The power to raise the number to.</param>
        /// <returns>The number risen to the specified power.</returns>
        public static long RaiseToPower(this long d, double power)
        {
            return Math.Pow(d, power).ToLong();
        }

        /// <summary>
        /// Convert the specified object into a decimal.
        /// </summary>
        /// <param name="o">The object to convert to a decimal.</param>
        /// <returns>The object represented as a decimal.</returns>
        public static decimal ToDecimal(this object o)
        {
            return Convert.ToDecimal(o);
        }
        /// <summary>
        /// Convert the specified object to a short.
        /// </summary>
        /// <param name="o">The object to convert.</param>
        /// <returns>The short representation of the specified object.</returns>
        public static short ToShort(this object o)
        {
            return Convert.ToInt16(o);
        }

        /// <summary>
        /// Convert the specified object to a long.
        /// </summary>
        /// <param name="o">The object to convert.</param>
        /// <returns>The long representation of the specified object.</returns>
        public static long ToLong(this object o)
        {
            return Convert.ToInt64(o);
        }

        /// <summary>
        /// Convert the specified object to a single-precision floating point number.
        /// </summary>
        /// <param name="o">The object to convert.</param>
        /// <returns>The object represented as a float.</returns>
        public static float ToFloat(this object o)
        {
            return Convert.ToSingle(o);
        }

        /// <summary>
        /// Convert the specified object to a double-precision floating point number.
        /// </summary>
        /// <param name="o">The object to convert.</param>
        /// <returns>The object represented as a double.</returns>
        public static double ToDouble(this object o)
        {
            return Convert.ToDouble(o);
        }

        /// <summary>
        /// Convert the specified object to a boolean.
        /// </summary>
        /// <param name="o">The object to convert.</param>
        /// <returns>The boolean representation of the specified object.</returns>
        public static bool ToBoolean(this object o)
        {
            return Convert.ToBoolean(o);
        }

        /// <summary>
        /// Convert the specified object to an unsigned short.
        /// </summary>
        /// <param name="o">The object to convert.</param>
        /// <returns>The ushort representation of the specified object.</returns>
        public static UInt16 ToUnsignedShort(this object o)
        {
            return Convert.ToUInt16(o);
        }

        /// <summary>
        /// Convert the specified object to a uint.
        /// </summary>
        /// <param name="o">The object to convert.</param>
        /// <returns>This object represented as a UInt32.</returns>
        public static UInt32 ToUnsignedInt(this object o)
        {
            return Convert.ToUInt32(o);
        }

        /// <summary>
        /// Convert this object to an unsigned long.
        /// </summary>
        /// <param name="o">The object to convert.</param>
        /// <returns>The unsigned long representation of the specified object.</returns>
        public static UInt64 ToUnsignedLong(this object o)
        {
            return Convert.ToUInt64(o);
        }

        /// <summary>
        /// Round a float to the nearest whole number.
        /// </summary>
        /// <param name="f">The floating-point number to round</param>
        /// <returns>The number rounded to the nearest integer</returns>
        public static int Round(this float f)
        {
            return Math.Round(f).ToInt();
        }

        /// <summary>
        /// Round a double to the nearest whole number.
        /// </summary>
        /// <param name="d">The floating-point number to round</param>
        /// <returns>The number rounded to the nearest integer</returns>
        public static int Round(this double d)
        {
            return Math.Round(d).ToInt();
        }

        /// <summary>
        /// Round a decimal to the nearest whole number.
        /// </summary>
        /// <param name="d">The decimal number to round</param>
        /// <returns>The number rounded to the nearest integer</returns>
        public static int Round(this decimal d)
        {
            return Math.Round(d).ToInt();
        }

        /// <summary>
        /// Check if a type implements a given interface.
        /// </summary>
        /// <param name="cT">The type to check implementation of an interface on</param>
        /// <param name="t">The interface to check</param>
        /// <returns>Whether or not the object implements the given interface</returns>
        [Obsolete("Use the 'is' keyword instead.")]
        public static bool Implements(this Type cT, Type t)
        {
            return cT.GetInterfaces().Contains(t);
        }

        /// <summary>
        /// Cast this object to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to cast the object to.</typeparam>
        /// <param name="o">The object to cast.</param>
        /// <returns>The object casted to T.</returns>
        public static T Cast<T>(this object o)
        {
            if (o == null) return default(T);
            /*
            Type trueType = typeof(T);
            if (trueType == typeof(int))
            {
                return (T)(object)o.ToInt();
            }
            if (trueType == typeof(string))
            {
                return (T)(object)o.ToString();
            }
            */
            return (T)o;

            //return (T)o;
        }

        /// <summary>
        /// Cast this object to the specified type, and output the casted object to a variable.
        /// </summary>
        /// <typeparam name="T">The type to cast the object to.</typeparam>
        /// <param name="o">The object to cast.</param>
        /// <param name="castedObj">The object casted to T.</param>
        public static void Cast<T>(this object o, out T castedObj)
        {
            castedObj = o.Cast<T>();
        }
    }
}
