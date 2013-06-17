using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Glib.WinForms.Shapes
{
    /// <summary>
    /// A triangle shape in windows forms.
    /// </summary>
    public class Triangle : Shape
    {
        /// <summary>
        /// The upper corner of the triangle.
        /// </summary>
        public Point PositionB { get; set; }
        /// <summary>
        /// The right hand corner of the triangle.
        /// </summary>
        public Point PositionC { get; set; }

        /// <summary>
        /// Create a new triangle.
        /// </summary>
        /// <param name="posA">Point A of the triangle.</param>
        /// <param name="posB">Point B of the triangle.</param>
        /// <param name="posC">Point C of the triangle.</param>
        /// <param name="color">The color of the shape.</param>
        public Triangle(Point posA, Point posB, Point posC, Color color) : base(posA.X,posA.Y,color)
        {
            PositionB = posB;
            PositionC = posC;
        }

        /// <summary>
        /// Draw the triangle to the specified graphics object.
        /// </summary>
        /// <param name="gfx">The graphics device to draw to.</param>
        /// <param name="color">The color to draw the shape.</param>
        /// <param name="fill">Whether or not to fill the shape.</param>
        public override void Draw(Graphics gfx, Color color, bool fill)
        {
            if (fill)
            {
                gfx.FillPolygon(new SolidBrush(color), new Point[] { Position, PositionB, PositionC, Position });
            }
            else
            {
                gfx.DrawPolygon(new Pen(color), new Point[] { Position, PositionB, PositionC, Position });
            }
        }
    }
}
