using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// A class representing the rotation of a sprite.
    /// </summary>
    public class SpriteRotation
    {
        /// <summary>
        /// The rotation of the sprite in degrees.
        /// </summary>
        public float Degrees{
            get
            {
                return _degrees;
            }
            set
            {
                _degrees = value % 360;
            }
        }

        /// <summary>
        /// Convert this rotation to a Vector2 through XnaExtensions.
        /// </summary>
        /// <returns>This rotation as a Vector2.</returns>
        public Vector2 AsVector()
        {
            return Radians.AngleToVector();
        }

        /// <summary>
        /// Add two SpriteRotations.
        /// </summary>
        /// <param name="x">The first SpriteRotation to add.</param>
        /// <param name="y">The second SpriteRotation to add.</param>
        /// <returns>A SpriteRotation representing the combined rotation values of x any y.</returns>
        public static SpriteRotation operator +(SpriteRotation x, SpriteRotation y){
            return new SpriteRotation(x.Degrees + y.Degrees);
        }

        /// <summary>
        /// Compare two SpriteRotations.
        /// </summary>
        /// <param name="x">The first SpriteRotation to compare.</param>
        /// <param name="y">The second SpriteRotation to compare.</param>
        /// <returns>Whether or not x is less than y.</returns>
        public static bool operator <(SpriteRotation x, SpriteRotation y)
        {
            return x.Degrees < y.Degrees;
        }

        /// <summary>
        /// Compare two SpriteRotations.
        /// </summary>
        /// <param name="x">The first SpriteRotation to compare.</param>
        /// <param name="y">The second SpriteRotation to compare.</param>
        /// <returns>Whether or not x is less than or equal to y.</returns>
        public static bool operator <=(SpriteRotation x, SpriteRotation y)
        {
            return x.Degrees <= y.Degrees;
        }

        /// <summary>
        /// Compare two SpriteRotations.
        /// </summary>
        /// <param name="x">The first SpriteRotation to compare.</param>
        /// <param name="y">The second SpriteRotation to compare.</param>
        /// <returns>Whether or not x is greater than or equal to y.</returns>
        public static bool operator >=(SpriteRotation x, SpriteRotation y)
        {
            return x.Degrees >= y.Degrees;
        }

        /// <summary>
        /// Compare two SpriteRotations.
        /// </summary>
        /// <param name="x">The first SpriteRotation to compare.</param>
        /// <param name="y">The second SpriteRotation to compare.</param>
        /// <returns>Whether or not x is greater than y.</returns>
        public static bool operator >(SpriteRotation x, SpriteRotation y)
        {
            return x.Degrees > y.Degrees;
        }

        /// <summary>
        /// Subtract a SpriteRotation and a float representing a value in DEGREES.
        /// </summary>
        /// <param name="x">The SpriteRotation to subtract from.</param>
        /// <param name="y">The number of degrees to subtract from the SpriteRotation.</param>
        /// <returns>A SpriteRotation representing x.Degrees - y.</returns>
        public static SpriteRotation operator -(SpriteRotation x, float y)
        {
            return new SpriteRotation(x.Degrees - y);
        }

        /// <summary>
        /// Add a SpriteRotation and a float representing a value in DEGREES.
        /// </summary>
        /// <param name="x">The SpriteRotation to add to.</param>
        /// <param name="y">The number of degrees to add to the SpriteRotation.</param>
        /// <returns>A SpriteRotation representing the combined rotation values of x any y.</returns>
        public static SpriteRotation operator +(SpriteRotation x, float y)
        {
            return new SpriteRotation(x.Degrees + y);
        }


        /// <summary>
        /// Subtract two SpriteRotations.
        /// </summary>
        /// <param name="x">The base SpriteRotation.</param>
        /// <param name="y">The SpriteRotation to subtract from the base.</param>
        /// <returns>A SpriteRotation representing x.Degrees - y.Degrees.</returns>
        public static SpriteRotation operator -(SpriteRotation x, SpriteRotation y)
        {
            return new SpriteRotation(x.Degrees - y.Degrees);
        }

        private float _degrees = 0f;

        /// <summary>
        /// Initialiaze a new SpriteRotation with a value of 0 degrees.
        /// </summary>
        public SpriteRotation()
        {
            
        }

        /// <summary>
        /// Initialize a new SpriteRotation with the specified value of degrees.
        /// </summary>
        /// <param name="degrees">The number of degrees to initialize this SpriteRotation to</param>
        public SpriteRotation(float degrees)
        {
            Degrees = degrees;
        }

        /// <summary>
        /// Initialize a new SpriteRotation with the specified value using the specified unit.
        /// </summary>
        /// <param name="value">The value to initialize this SpriteRotation to.</param>
        /// <param name="useRadians">Whether or not to interpret value as radians.</param>
        public SpriteRotation(float value, bool useRadians)
        {
            if (useRadians)
            {
                Radians = value;
            }
            else
            {
                Degrees = value;
            }
        }

        /// <summary>
        /// The rotation of the sprite in radians.
        /// </summary>
        public float Radians
        {
            get
            {
                return MathHelper.ToRadians(Degrees);
            }
            set
            {
                Degrees = MathHelper.ToDegrees(value);
            }
        }
    }
}
