using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Glib.WinForms.Shapes
{
    /// <summary>
    /// A circle shape.
    /// </summary>
    public class Circle : Shape
    {
        private int _diameter;

        /// <summary>
        /// The diameter of the circle.
        /// </summary>
        public int Diameter
        {
            get
            {
                int num = this._diameter;
                return num;
            }
            set
            {
                this._diameter = value;
            }
        }

        /// <summary>
        /// The radius of the circle.
        /// </summary>
        /// <remarks>
        /// Is approximate in getting since it converts directly to the diameter by integer division.
        /// </remarks>
        public int Radius
        {
            get
            {
                return _diameter / 2;
            }
            set
            {
                _diameter = value * 2;
            }
        }

        /// <summary>
        /// Create a new circle with the specified position, diameter, and color.
        /// </summary>
        /// <param name="x">The X position of the circle.</param>
        /// <param name="y">The Y position of the circle.</param>
        /// <param name="diameter">The diameter of the circle.</param>
        /// <param name="color">The color of the circle.</param>
        public Circle(int x, int y, int diameter, Color color)
            : base(x, y, color)
        {
            this._diameter = diameter;
        }

        /// <summary>
        /// Create a new circle with the specified position, diameter, and color.
        /// </summary>
        /// <param name="pos">The position of the circle.</param>
        /// <param name="diameter">The diameter of the circle.</param>
        /// <param name="color">The color of the circle.</param>
        public Circle(Point pos, int diameter, Color color)
            : this(pos.X, pos.Y, diameter, color)
        {
        }

        /// <summary>
        /// Draw the circle to the specified graphics object..
        /// </summary>
        /// <param name="gfx">The graphics device to draw to.</param>
        /// <param name="color">The color to draw the shape.</param>
        /// <param name="fill">Whether or not to fill the shape.</param>
        public override void Draw(Graphics gfx, Color color, bool fill = true)
        {
            bool hollow = !fill;
            if (hollow)
            {
                gfx.DrawEllipse(new Pen(color), X, Y, this._diameter, this._diameter);
            }
            else
            {
                gfx.FillEllipse(new SolidBrush(color), X, Y, this._diameter, this._diameter);
            }
        }
        /// <summary>
        /// Draw this circle to the specified graphics object with the default color.
        /// </summary>
        /// <param name="gfx">The graphics device to draw to.</param>
        /// <param name="fill">Whether or not to fill the shape.</param>
        public override void Draw(Graphics gfx, bool fill = true)
        {
            base.Draw(gfx, fill);
        }
    }
}
