using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// An enum representing a type of an angle measurement.
    /// </summary>
    public enum AngleType
    {
        /// <summary>
        /// An angle in radians.
        /// </summary>
        Radians,
        /// <summary>
        /// An angle in degrees.
        /// </summary>
        Degrees,
        /// <summary>
        /// An angle in gradians.
        /// </summary>
        Gradians
    }

    /// <summary>
    /// A structure representing the rotation of a sprite.
    /// </summary>
    public struct SpriteRotation
    {
        /// <summary>
        /// Gets or sets the rotation of the sprite in degrees.
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

        private float _degrees;

        /// <summary>
        /// Initialize a new SpriteRotation with the specified value of degrees.
        /// </summary>
        /// <param name="degrees">The number of degrees to initialize this SpriteRotation to</param>
        public SpriteRotation(float degrees)
        {
            _degrees = degrees % 360;
        }

        /// <summary>
        /// Initialize a new SpriteRotation with the specified value using the specified unit.
        /// </summary>
        /// <param name="value">The value to initialize this SpriteRotation to.</param>
        /// <param name="measurementType">The type of angle that value represents.</param>
        public SpriteRotation(float value, AngleType measurementType)
        {
            if (measurementType == AngleType.Radians)
            {
                _degrees = MathHelper.ToDegrees(value);
            }
            else if(measurementType == AngleType.Degrees)
            {
                _degrees = value % 360;
            }
            else if (measurementType == AngleType.Gradians)
            {
                _degrees = value / .9f;
            }
            else
            {
                throw new NotImplementedException("The specified AngleType has not been implemented.");
            }
        }

        /// <summary>
        /// Gets or sets the rotation of the Sprite in gradians (AKA gons, grads, or grades).
        /// </summary>
        public float Gradians
        {
            get
            {
                return .9f * _degrees;
            }
            set
            {
                _degrees = value / .9f;
            }
        }



        /// <summary>
        /// Gets or sets he rotation of the sprite in radians.
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
