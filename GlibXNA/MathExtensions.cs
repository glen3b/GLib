using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glib.XNA
{
    /// <summary>
    /// An internal class providing mathematical functionality present in the stantard Glib assembly.
    /// </summary>
    internal static class MathExtensions
    {
        internal static float RaiseToPower(this float doubleToRaise, float power)
        {
            return Convert.ToSingle(Math.Pow(Convert.ToDouble(doubleToRaise), Convert.ToDouble(power)));
        }

        internal static double RaiseToPower(this double doubleToRaise, double power)
        {
            return Math.Pow(doubleToRaise, power);
        }

        internal static float ToFloat(this Object valueToConvert)
        {
            return Convert.ToSingle(valueToConvert);
        }

        internal static int ToInt(this Object valueToConvert)
        {
            return Convert.ToInt32(valueToConvert);
        }

        internal static T Cast<T>(this object casted)
        {
            return (T)casted;
        }

        internal static int Round(this decimal number)
        {
            return Math.Round(number).ToInt();
        }

        internal static int Round(this float number)
        {
            return Math.Round(number).ToInt();
        }

        internal static int Round(this double number)
        {
            return Math.Round(number).ToInt();
        }
    }
}
