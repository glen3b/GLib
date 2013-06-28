using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Glib.XNA.InputLib.Mouse
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
        /// An event called when there is a mouse click in this region.
        /// </summary>
        public event EventHandler MouseClick;

        /// <summary>
        /// Update this ScreenRegion, calling mouse events.
        /// </summary>
        public void Update()
        {
            bool isMouseInCurrent = false;
            bool isMouseInPast = false;
            isMouseInCurrent =
                MouseManager.CurrentMouseState.X >= Position.X &&
                MouseManager.CurrentMouseState.X <= Position.X + Width &&
                MouseManager.CurrentMouseState.Y >= Position.Y &&
                MouseManager.CurrentMouseState.Y <= Position.Y + Height;
            isMouseInPast =
                MouseManager.LastMouseState.X >= Position.X &&
                MouseManager.LastMouseState.X <= Position.X + Width &&
                MouseManager.LastMouseState.Y >= Position.Y &&
                MouseManager.LastMouseState.Y <= Position.Y + Height;

            if (!isMouseInPast && isMouseInCurrent && MouseEnter != null)
            {
                MouseEnter(this, EventArgs.Empty);
            }
            if (isMouseInPast && !isMouseInCurrent && MouseLeave != null)
            {
                MouseLeave(this, EventArgs.Empty);
            }
            if (MouseClick != null &&
                isMouseInCurrent &&
                MouseManager.CurrentMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                && MouseManager.LastMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                MouseClick(this, EventArgs.Empty);
            }
        }
    }
}
