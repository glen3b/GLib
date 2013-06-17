using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Glib.WinForms.Shapes
{
    /// <summary>
    /// A basic shape.
    /// </summary>
    public abstract class Shape
    {
        private int _x;

        private int _y;

        private Color _color;

        /// <summary>
        /// The color of the shape.
        /// </summary>
        public Color Color
        {
            get
            {
                Color color = this._color;
                return color;
            }
            set
            {
                this._color = value;
            }
        }

        /// <summary>
        /// The X position of the shape.
        /// </summary>
        public int X
        {
            get
            {
                int num = this._x;
                return num;
            }
            set
            {
                this._x = value;
            }
        }

        /// <summary>
        /// Gets or sets the position of the shape.
        /// </summary>
        public Point Position
        {
            get
            {
                return new Point(X, Y);
            }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
        /// The Y position of the shape.
        /// </summary>
        public int Y
        {
            get
            {
                int num = this._y;
                return num;
            }
            set
            {
                this._y = value;
            }
        }

        /// <summary>
        /// Create a new shape.
        /// </summary>
        /// <param name="x">The X position of the shape.</param>
        /// <param name="y">The Y position of the shape.</param>
        /// <param name="color">The color of the shape.</param>
        public Shape(int x, int y, Color color)
        {
            this._x = x;
            this._y = y;
            this._color = color;
        }

        /// <summary>
        /// Draw the shape to the specified graphics object with the default color.
        /// </summary>
        /// <param name="gfx">The graphics device to draw to.</param>
        /// <param name="fill">Whether or not to fill the shape.</param>
        public virtual void Draw(Graphics gfx, bool fill = false)
        {
            Draw(gfx, _color, fill);
        }

        /// <summary>
        /// Draw the shape to the specified graphics object.
        /// </summary>
        /// <param name="gfx">The graphics device to draw to.</param>
        /// <param name="color">The color to draw the shape.</param>
        /// <param name="fill">Whether or not to fill the shape.</param>
        public abstract void Draw(Graphics gfx, Color color, bool fill);
    }
}
