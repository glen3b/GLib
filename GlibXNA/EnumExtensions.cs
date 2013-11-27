using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Glib.XNA
{
#if XBOX
    /// <summary>
    /// A class that provides Xbox extensions on enums.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Test whether an enum has a flag.
        /// </summary>
        /// <param name="enumVal">The enum to test.</param>
        /// <param name="flag">The flag to check for.</param>
        /// <returns>Whether a has flag.</returns>
        public static bool HasFlag(this Enum enumVal, Enum flag)
        {
            ulong keysVal = Convert.ToUInt64(enumVal);
            ulong flagVal = Convert.ToUInt64(flag);

            return (keysVal & flagVal) == flagVal;
        }
    }
#endif
}
