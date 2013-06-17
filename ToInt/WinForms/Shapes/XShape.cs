using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Glib.WinForms.Shapes
{
    /// <summary>
    /// A figure which is an X.
    /// </summary>
    public class XShape : Shape
    {
        private int _x1;

        private int _y1;

        private int _distanceBetweenX;

        /// <summary>
        /// The distance between the top two corners of the X (along the X axis).
        /// </summary>
        public int XDistance
        {
            get
            {
                return _distanceBetweenX;
            }
            set
            {
                _distanceBetweenX = value;
            }
        }

        /// <summary>
        /// The second X coordinate of the X.
        /// </summary>
        public int X1
        {
            get
            {
                int num = this._x1;
                return num;
            }
            set
            {
                this._x1 = value;
            }
        }

        /// <summary>
        /// Gets the X, Y, X1, and Y1 positions of the XShape.
        /// </summary>
        public new Point[] Position
        {
            get
            {
                return new Point[] { new Point(X,Y), new Point(X1, Y1) };
            }
        }

        /// <summary>
        /// The second Y coordinate of the X.
        /// </summary>
        public int Y1
        {
            get
            {
                int num = this._y1;
                return num;
            }
            set
            {
                this._y1 = value;
            }
        }

        /// <summary>
        /// Create a new X shape.
        /// </summary>
        /// <param name="x">The first X position.</param>
        /// <param name="y">The first Y position.</param>
        /// <param name="x1">The second X position.</param>
        /// <param name="y1">The second Y position.</param>
        /// <param name="distanceBetweenX">The distance between the top two points of the X shape.</param>
        /// <param name="color">The color of the X.</param>
        public XShape(int x, int y, int x1, int y1, int distanceBetweenX, Color color)
            : base(x, y, color)
        {
            this._x1 = x1;
            this._y1 = y1;
            this._distanceBetweenX = distanceBetweenX;
        }

        /// <summary>
        /// Draw the X to the specified graphics.
        /// </summary>
        /// <param name="gfx">The graphics to draw the X to.</param>
        /// <param name="color">The color to draw the X with.</param>
        /// <param name="fill">(Ignored parameter, cannot fill an X)</param>
        public override void Draw(Graphics gfx, Color color, bool fill = false)
        {
            gfx.DrawLine(new Pen(color), X, Y, this._x1, this._y1);
            gfx.DrawLine(new Pen(color), X + this._distanceBetweenX, Y, X, this._y1);
        }
    }
}
