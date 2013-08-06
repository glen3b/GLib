using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Glib
{

    /// <summary>
    /// A random number generator generating only unique random numbers.
    /// </summary>
    public class UniqueRandom : Random
    {
        private List<int> _generatedNumber = new List<int>();
        private List<double> _generatedDecimal = new List<double>();

        /// <summary>
        /// Create a new UniqueRandom random number generator.
        /// </summary>
        public UniqueRandom() : base()
        {
        }

        /// <summary>
        /// Create a new UniqueRandom random number generator with the specified seed.
        /// </summary>
        /// <param name="seed">The seed of the random number generator.</param>
        public UniqueRandom(int seed)
            : base(seed)
        {
        }


        /// <summary>
        /// Reset the list of generated numbers (integers, doubles, and bytes).
        /// </summary>
        public void Reset()
        {
            ResetInts();
            ResetDoubles();
        }

        /// 
        /// 
        /// <summary>
        /// Reset the list of generated integers.
        /// </summary>
        public void ResetInts()
        {
            this._generatedNumber.Clear();
        }

        /// <summary>
        /// Reset the list of generated doubles.
        /// </summary>
        public void ResetDoubles()
        {
            _generatedDecimal.Clear();
        }

        /// <summary>
        /// Generate a random unique number between the two specified values.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the number to be generated.</param>
        /// <param name="maxValue">The exclusive upper bound of the number to be generated.</param>
        /// <returns>A random number, unique within this instance, between the two specified values.</returns>
        public override int Next(int minValue, int maxValue)
        {
            bool flag;
            int num = 0;
            do
            {
                num = base.Next(minValue, maxValue);
                flag = _generatedNumber.Contains(num);
            }
            while (flag);
            this._generatedNumber.Add(num);
            int num1 = num;
            return num1;
        }

        /// <summary>
        /// Generate a random unique number lower than the specified maximum.
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the number to be generated.</param>
        /// <returns>A random number, unique within this instance, less than the specified value.</returns>
        public override int Next(int maxValue)
        {
            return Next(0, maxValue);
        }

        /// <summary>
        /// Generate a random, unique number.
        /// </summary>
        /// <returns>A random, unique number to this instance.</returns>
        public override int Next()
        {
            int num;
            do
            {
                num = base.Next();
            } while (_generatedNumber.Contains(num));
            _generatedNumber.Add(num);
            return num;
        }

        /// <summary>
        /// Generate a random, unique double between 0.0 and 1.0.
        /// </summary>
        /// <returns>A random, unique double to this instance.</returns>
        public override double NextDouble()
        {
            double num;
            do
            {
                num = base.NextDouble();
            } while (_generatedDecimal.Contains(num));
            _generatedDecimal.Add(num);
            return num;
        }

        /// <summary>
        /// Fill the elements of a specified array with bytes of non-unique random numbers.
        /// </summary>
        /// <param name="buffer">An array of bytes to contain random numbers.</param>
        public override void NextBytes(byte[] buffer)
        {
            base.NextBytes(buffer);
        }
    }

    /// <summary>
    /// Multiple type-to-type conversion methods acting as extensions on object.
    /// </summary>
    public static class ObjectExtensions
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
        /// Convert an array to a string.
        /// </summary>
        /// <param name="array">The array to convert.</param>
        /// <param name="delimiter">The delimiter between elements.</param>
        /// <returns>All elements of the array delimited by delimiter in a string.</returns>
        public static string ToArrayString(this Array array, string delimiter = " ")
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
        /// Returns a private property Value from a given Object. Uses Reflection.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If obj is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">If the property is not found.</exception>
        /// <typeparam name="T">Type of the Property</typeparam>
        /// <param name="obj">Object from where the Property Value is returned</param>
        /// <param name="propName">Propertyname as string.</param>
        /// <returns>PropertyValue</returns>
        public static T GetPrivatePropertyValue<T>(this object obj, string propName)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            PropertyInfo pi = obj.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (pi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));
            return (T)pi.GetValue(obj, null);
        }

        /// <summary>
        /// Returns a private field Value from a given Object. Uses Reflection.
        /// Throws an ArgumentOutOfRangeException if the Property is not found.
        /// </summary>
        /// <typeparam name="T">Type of the Property</typeparam>
        /// <param name="obj">Object from where the Property Value is returned</param>
        /// <param name="propName">Propertyname as string.</param>
        /// <returns>PropertyValue</returns>
        public static T GetPrivateFieldValue<T>(this object obj, string propName)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            Type t = obj.GetType();
            FieldInfo fi = null;
            while (fi == null && t != null)
            {
                fi = t.GetField(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                t = t.BaseType;
            }
            if (fi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Field {0} was not found in Type {1}", propName, obj.GetType().FullName));
            return (T)fi.GetValue(obj);
        }

        /// <summary>
        /// Sets a _private_ Property Value from a given Object. Uses Reflection.
        /// Throws an ArgumentOutOfRangeException if the Property is not found.
        /// </summary>
        /// <typeparam name="T">Type of the Property</typeparam>
        /// <param name="obj">Object from where the Property Value is set</param>
        /// <param name="propName">Propertyname as string.</param>
        /// <param name="val">Value to set.</param>
        /// <returns>PropertyValue</returns>
        public static void SetPrivatePropertyValue<T>(this object obj, string propName, T val)
        {
            Type t = obj.GetType();
            if (t.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) == null)
                throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));
            t.InvokeMember(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance, null, obj, new object[] { val });
        }

        /// <summary>
        /// Set a private Property Value on a given Object. Uses Reflection.
        /// </summary>
        /// <typeparam name="T">Type of the Property</typeparam>
        /// <param name="obj">Object from where the Property Value is returned</param>
        /// <param name="propName">Propertyname as string.</param>
        /// <param name="val">the value to set</param>
        /// <exception cref="ArgumentOutOfRangeException">if the Property is not found</exception>
        public static void SetPrivateFieldValue<T>(this object obj, string propName, T val)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            Type t = obj.GetType();
            FieldInfo fi = null;
            while (fi == null && t != null)
            {
                fi = t.GetField(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                t = t.BaseType;
            }
            if (fi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Field {0} was not found in Type {1}", propName, obj.GetType().FullName));
            fi.SetValue(obj, val);
        }

        /// <summary>
        /// Call a private method on an object through reflection.
        /// </summary>
        /// <remarks>
        /// A public method will not be included in the method search.
        /// </remarks>
        /// <param name="objToCallOn">The object to call a method on.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="methodParams">The parameters to pass to the method.</param>
        /// <returns>The result of the function.</returns>
        public static object CallPrivateMethod(this object objToCallOn, string methodName, params object[] methodParams)
        {
            MethodInfo dynMethod = objToCallOn.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            return dynMethod.Invoke(objToCallOn, methodParams);
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
            if (o is IConvertible)
            {
                return (T)Convert.ChangeType(o, typeof(T));
            }
            else
            {
                return (T)o;
            }
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
