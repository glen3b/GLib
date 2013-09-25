using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

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
    [DebuggerDisplay("Radians = {Radians}")]
    public struct SpriteRotation
    {
        /// <summary>
        /// Determines whether this <see cref="SpriteRotation"/> equals the specified object.
        /// </summary>
        /// <param name="obj">The object to determine equivalence to.</param>
        /// <returns>Whether or not the specified object is equal.</returns>
        /// <remarks>
        /// For all logical purposes, this method only compares rotation values.
        /// This method will attempt to cast the specified object (note: this includes user conversions) to a SpriteRotation before comparing equality.
        /// </remarks>
        public override bool Equals(object obj)
        {
            SpriteRotation val;
            if (!(obj is SpriteRotation))
            {
                try
                {
                    val = (SpriteRotation)obj;
                }
                catch (InvalidCastException)
                {
                    return false;
                }
            }
            else
            {
                val = (SpriteRotation)obj;
            }
            return val.Degrees == this.Degrees;
        }

        /// <summary>
        /// Calculate a hash of this <see cref="SpriteRotation"/>.
        /// </summary>
        /// <returns>A hash code of this SpriteRotation.</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;

                //All point to same thing, just hash it all.
                hash = hash * 23 + this.Degrees.GetHashCode();
                hash = hash * 23 + this.Radians.GetHashCode();
                hash = hash * 23 + this.Gradians.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Gets a SpriteRotation representing zero degrees.
        /// </summary>
        public static SpriteRotation Zero
        {
            get
            {
                return new SpriteRotation(0);
            }
        }

        /// <summary>
        /// An event fired when the rotation value of this <see cref="SpriteRotation"/> changes.
        /// </summary>
        public event EventHandler ValueChanged;

        /// <summary>
        /// Returns a value indicating whether two SpriteRotations are not equal.
        /// </summary>
        /// <param name="a">The first SpriteRotation to compare.</param>
        /// <param name="b">The second SpriteRotation to compare.</param>
        /// <returns>A value indicating whether the two SpriteRotations are not equal.</returns>
        /// <remarks>
        /// Doesn't compare events.
        /// </remarks>
        public static bool operator !=(SpriteRotation a, SpriteRotation b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Returns a value indicating whether two SpriteRotations are equal.
        /// </summary>
        /// <param name="a">The first SpriteRotation to compare.</param>
        /// <param name="b">The second SpriteRotation to compare.</param>
        /// <returns>A value indicating whether the two SpriteRotations are equal.</returns>
        /// <remarks>
        /// Doesn't compare events.
        /// </remarks>
        public static bool operator ==(SpriteRotation a, SpriteRotation b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Returns a SpriteRotation representing the specified value in radians.
        /// </summary>
        /// <param name="radians">The number of radians to represent in the new SpriteRotation.</param>
        /// <returns>A new SpriteRotation representing the specified value in radians.</returns>
        public static SpriteRotation FromRadians(float radians)
        {
            return new SpriteRotation(radians, AngleType.Radians);
        }

        /// <summary>
        /// Returns a SpriteRotation representing the specified value in degrees.
        /// </summary>
        /// <param name="degrees">The number of degrees to represent in the new SpriteRotation.</param>
        /// <returns>A new SpriteRotation representing the specified value in degrees.</returns>
        public static SpriteRotation FromDegrees(float degrees)
        {
            return new SpriteRotation(degrees, AngleType.Degrees);
        }

        /// <summary>
        /// Gets or sets the rotation of the sprite in degrees.
        /// </summary>
        public float Degrees
        {
            get
            {
                return MathHelper.ToDegrees(Radians);
            }
            set
            {
                Radians = MathHelper.ToRadians(value);
            }
        }

        /// <summary>
        /// Gets or sets a vector representing this angle.
        /// </summary>
        /// <remarks>
        /// All logic of this is handled by XnaExtensions.
        /// </remarks>
        public Vector2 Vector
        {
            get
            {
                return Radians.AngleToVector();
            }
            set
            {
                Radians = value.ToAngle();
            }
        }

        /// <summary>
        /// Add two SpriteRotations.
        /// </summary>
        /// <param name="x">The first SpriteRotation to add.</param>
        /// <param name="y">The second SpriteRotation to add.</param>
        /// <returns>A SpriteRotation representing the combined rotation values of x any y.</returns>
        public static SpriteRotation operator +(SpriteRotation x, SpriteRotation y)
        {
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
            return new SpriteRotation(x.Degrees - y.Degrees, AngleType.Degrees);
        }

        private float _radians;

        /// <summary>
        /// Initialize a new SpriteRotation with the specified value of degrees.
        /// </summary>
        /// <param name="degrees">The number of degrees to initialize this SpriteRotation to</param>
        public SpriteRotation(float degrees)
            : this(degrees, AngleType.Degrees)
        {
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
                _radians = value;
            }
            else if (measurementType == AngleType.Degrees)
            {
                _radians = MathHelper.ToRadians(value);
            }
            else if (measurementType == AngleType.Gradians)
            {
                _radians = MathHelper.ToRadians(value / .9f);
            }
            else
            {
                throw new NotImplementedException("The specified AngleType has not been implemented.");
            }
            ValueChanged = null;
        }

        /// <summary>
        /// Gets or sets the rotation of the Sprite in gradians (AKA gons, grads, or grades).
        /// </summary>
        public float Gradians
        {
            get
            {
                return .9f * Degrees;
            }
            set
            {
                Degrees = value / .9f;
            }
        }



        /// <summary>
        /// Gets or sets the rotation of the sprite in radians.
        /// </summary>
        public float Radians
        {
            get
            {
                return _radians;
            }
            set
            {
                if (value != _radians)
                {
                    _radians = value;
                    if (ValueChanged != null)
                    {
                        ValueChanged(this, EventArgs.Empty);
                    }
                }
            }
        }
    }
}
