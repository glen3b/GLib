using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// A basic class that represents a position. Intented to be used when an IPositionable is needed but you only have a Vector2.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class PositionRepresentation : IPositionable
    {
        private Vector2 _pos;

        /// <summary>
        /// Gets or sets the X position of the Position object.
        /// </summary>
        public float X
        {
            get
            {
                return _pos.X;
            }
            set
            {
                _pos.X = value;
            }
        }
        /// <summary>
        /// Gets or sets the Y position of the Position object.
        /// </summary>
        public float Y
        {
            get
            {
                return _pos.Y;
            }
            set
            {
                _pos.Y = value;
            }
        }

        /// <summary>
        /// Create a new PositionRepresentation.
        /// </summary>
        /// <param name="pos">The position to represent.</param>
        public PositionRepresentation(Vector2 pos)
        {
            _pos = pos;
        }

        /// <summary>
        /// Gets or sets the underlying position.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return _pos;
            }
            set
            {
                _pos = value;
            }
        }
    }
}
