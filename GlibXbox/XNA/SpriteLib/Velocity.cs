using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// Represents the velocity of a sprite.
    /// </summary>
    public class Velocity
    {
        /// <summary>
        /// Gets a Vector2 representing this Velocity.
        /// </summary>
        public Vector2 Vector
        {
            get
            {
                return new Vector2(XVelocity.HasValue ? XVelocity.Value : 0, YVelocity.HasValue ? YVelocity.Value : 0);
            }
        }

        /// <summary>
        /// The velocity on the X axis (null means no change).
        /// </summary>
        public float? XVelocity = null;

        /// <summary>
        /// The velocity on the Y axis (null means no change).
        /// </summary>
        public float? YVelocity = null;

        /// <summary>
        /// Construct a new velocity representing no change.
        /// </summary>
        public Velocity()
        {
        }

        /// <summary>
        /// Construct a new velocity representing the passed in values of XSpeed and YSpeed.
        /// </summary>
        public Velocity(float XSpeed, float YSpeed)
        {
            XVelocity = XSpeed;
            YVelocity = YSpeed;
        }
    }
}
