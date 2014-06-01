using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Glib.XNA
{
    /// <summary>
    /// An object which can dynamically manage relative coordinates on a screen, expressed as a percentage of the width and height.
    /// </summary>
    public class RelativeCoordinateManager
    {
        private float _width;

        /// <summary>
        /// Gets the width of the managed screen.
        /// </summary>
        public float Width
        {
            get { return _width; }
            private set {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("The width must be greater than zero. Value: " + value);
                }
                _width = value; }
        }

        private float _height;

        /// <summary>
        /// Gets the height of the managed screen.
        /// </summary>
        public float Height
        {
            get { return _height; }
            private set {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("The height must be greater than zero. Value: " + value);
                }
                _height = value; }
        }

        /// <summary>
        /// Creates a relative coordinate manager instance.
        /// </summary>
        /// <param name="width">The width of the managed screen.</param>
        /// <param name="height">The height of the managed screen.</param>
        public RelativeCoordinateManager(float width, float height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Creates a relative coordinate manager instance.
        /// </summary>
        /// <param name="size">The vector containing dimensions of the managed screen.</param>
        public RelativeCoordinateManager(Vector2 size)
            : this(size.X, size.Y)
        {
        }

        private Vector2 _offset = Vector2.Zero;

        /// <summary>
        /// Gets or sets the offset in generated positions. Changes to this value will not affect/reposition tracked objects.
        /// </summary>
        public Vector2 PositionOffset
        {
            get { return _offset; }
            set { _offset = value; }
        }
        

        /// <summary>
        /// Creates a relative coordinate manager instance.
        /// </summary>
        /// <param name="viewport">The viewport containing dimensions of the managed screen.</param>
        public RelativeCoordinateManager(Viewport viewport) : this(viewport.Width, viewport.Height)
        {
        }

        /// <summary>
        /// Gets an absolute position from a relative position.
        /// </summary>
        /// <param name="x">The relative position along the X axis, a decimal value from 0 to 1.</param>
        /// <param name="y">The relative position along the Y axis, a decimal value from 0 to 1.</param>
        /// <returns>A vector representing the absolute position with the specified relative coordinates with the width and height as known by this managed instance.</returns>
        public Vector2 GetAbsolutePosition(float x, float y)
        {
            return PositionOffset + new Vector2(MathHelper.Clamp(x, 0, 1) * Width, MathHelper.Clamp(y, 0, 1) * Height);
        }

        /// <summary>
        /// Gets an absolute position from a relative position.
        /// </summary>
        /// <param name="position">The relative position.</param>
        /// <returns>A vector representing the absolute position with the specified relative coordinates with the width and height as known by this managed instance.</returns>
        public Vector2 GetAbsolutePosition(Vector2 position)
        {
            return GetAbsolutePosition(position.X, position.Y);
        }

        /// <summary>
        /// Gets a relative position from an absolute position.
        /// </summary>
        /// <param name="x">The absolute position along the X axis, a decimal value from 0 to width.</param>
        /// <param name="y">The absolute position along the Y axis, a decimal value from 0 to height.</param>
        /// <returns>A vector representing the relative position with the specified absolute coordinates with the width and height as known by this managed instance.</returns>
        public Vector2 GetRelativePosition(float x, float y)
        {
            return new Vector2(MathHelper.Clamp(x, 0, Width) / Width, MathHelper.Clamp(y, 0, Height) / Height) - PositionOffset;
        }

        /// <summary>
        /// Positions an object to relative coordinates, tracking it such that upon a size change of this instance, the position will change.
        /// After invocation, the tracked positionable object will have had its position changed at least once.
        /// </summary>
        /// <param name="tracked">The positionable object to track.</param>
        /// <param name="x">The relative position along the X axis, a decimal value from 0 to 1.</param>
        /// <param name="y">The relative position along the Y axis, a decimal value from 0 to 1.</param>
        public void Position(IPositionable tracked, float x, float y)
        {
            if (tracked == null)
            {
                throw new ArgumentNullException("tracked");
            }

            Vector2 position = new Vector2(MathHelper.Clamp(x, 0, 1), MathHelper.Clamp(y, 0, 1));
            _trackedToRelativePositions[tracked] = position;
            tracked.Position = GetAbsolutePosition(position);
        }

        /// <summary>
        /// Sets the size of the screen as known by this instance, updating the positions of all tracked objects.
        /// </summary>
        /// <param name="width">The new width of the screen.</param>
        /// <param name="height">The new height of the screen.</param>
        public void SetSize(float width, float height)
        {
            // Properties do validation
            Width = width;
            Height = height;

            foreach (var item in _trackedToRelativePositions)
            {
                item.Key.Position = GetAbsolutePosition(item.Value);
            }
        }

        // Really the best solution since IPositionables have no unique ID
        private IDictionary<IPositionable, Vector2> _trackedToRelativePositions = new Dictionary<IPositionable, Vector2>();
    }
}
