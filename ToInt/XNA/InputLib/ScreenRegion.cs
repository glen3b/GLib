using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;

namespace Glib.XNA.InputLib
{
    /// <summary>
    /// Represents a region on screen which can call mouse events.
    /// </summary>
    public class ScreenRegion : IPositionable, ISizable, ISizedScreenObject
    {
        /// <summary>
        /// Convert the specified ScreenRegion to a rectangle.
        /// </summary>
        /// <param name="a">The ScreenRegion to convert.</param>
        /// <returns>The rectangle representing the ScreenRegion.</returns>
        /// <remarks>
        /// The rectange is rounded, since the ScreenRegion supports floating point numbers.
        /// </remarks>
        public static explicit operator Rectangle(ScreenRegion a)
        {
            return new Rectangle(a.Position.X.Round(), a.Position.Y.Round(), a.Width.Round(), a.Height.Round());
        }

        /// <summary>
        /// Create a new ScreenRegion with the specified bounds and position.
        /// </summary>
        /// <param name="bounds">The bounds and position of the ScreenRegion.</param>
        public ScreenRegion(Rectangle bounds) : this(new Vector2(bounds.X, bounds.Y), new Vector2(bounds.Width, bounds.Height))
        {
        }

        /// <summary>
        /// Create a new ScreenRegion with the specified size and position.
        /// </summary>
        /// <param name="pos">The position of the ScreenRegion.</param>
        /// <param name="size">The size of the ScreenRegion.</param>
        public ScreenRegion(Vector2 pos, Vector2 size)
        {
            Size = size;
            Position = pos;
        }

        private Vector2 _size;

        /// <summary>
        /// Gets or sets the size of the ScreenRegion.
        /// </summary>
        public Vector2 Size
        {
            get { return _size; }
            set { _size = value; }
        }
        

        private Vector2 _position;

        /// <summary>
        /// Gets or sets the position of the ScreenRegion.
        /// </summary>
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        /// <summary>
        /// Gets or sets the width of the ScreenRegion.
        /// </summary>
        public float Width
        {
            get { return _size.X; }
            set { _size.X = value; }
        }

        /// <summary>
        /// Gets or sets the height of the ScreenRegion.
        /// </summary>
        public float Height
        {
            get { return _size.Y; }
            set { _size.Y = value; }
        }

        /// <summary>
        /// An event called when a mouse enters this ScreenRegion.
        /// </summary>
        public event EventHandler MouseEnter;

        /// <summary>
        /// An event called when a mouse leaves this ScreenRegion.
        /// </summary>
        public event EventHandler MouseLeave;

        /// <summary>
        /// An event called when there is a left mouse click in this region.
        /// </summary>
        public event EventHandler LeftMouseClick;

        /// <summary>
        /// An event called when there is a right mouse click (AKA a right click) in this region.
        /// </summary>
        public event EventHandler RightMouseClick;

        /// <summary>
        /// Update this ScreenRegion, calling mouse events.
        /// </summary>
        internal void Update()
        {
            bool isMouseInCurrent = false;
            bool isMouseInPast = false;
            isMouseInCurrent =
                !Guide.IsVisible &&
                MouseManager.CurrentMouseState.X >= _position.X &&
                MouseManager.CurrentMouseState.X <= _position.X + _size.X &&
                MouseManager.CurrentMouseState.Y >= _position.Y &&
                MouseManager.CurrentMouseState.Y <= _position.Y + _size.Y;
            isMouseInPast =
                !Guide.IsVisible &&
                MouseManager.LastMouseState.X >= _position.X &&
                MouseManager.LastMouseState.X <= _position.X + _size.X &&
                MouseManager.LastMouseState.Y >= _position.Y &&
                MouseManager.LastMouseState.Y <= _position.Y + _size.Y;

            if (!isMouseInPast && isMouseInCurrent && MouseEnter != null)
            {
                MouseEnter(this, EventArgs.Empty);
            }
            if (isMouseInPast && !isMouseInCurrent && MouseLeave != null)
            {
                MouseLeave(this, EventArgs.Empty);
            }

            if (LeftMouseClick != null &&
                isMouseInCurrent &&
                MouseManager.CurrentMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                && MouseManager.LastMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                LeftMouseClick(this, EventArgs.Empty);
            }

            if (RightMouseClick != null &&
                isMouseInCurrent &&
                MouseManager.CurrentMouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                && MouseManager.LastMouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                RightMouseClick(this, EventArgs.Empty);
            }

        }
    }
}
