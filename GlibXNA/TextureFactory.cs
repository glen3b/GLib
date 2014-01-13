using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Glib.XNA
{
    /// <summary>
    /// A class for creating textures with specific patterns.
    /// </summary>
    public class TextureFactory
    {
        private GraphicsDevice _graphics;

        /// <summary>
        /// Gets the GraphicsDevice used for creating textures.
        /// </summary>
        protected GraphicsDevice Graphics
        {
            get { return _graphics; }
        }


        /// <summary>
        /// Creates a new TextureFactory with the specified GraphicsDevice.
        /// </summary>
        /// <param name="device">The GraphicsDevice this TextureFactory will use to create Texture2D objects.</param>
        public TextureFactory(GraphicsDevice device)
        {
            _graphics = device;
        }

        /// <summary>
        /// Creates a texture using the specified function.
        /// </summary>
        /// <param name="width">The width of the new texture.</param>
        /// <param name="height">The height of the new texture.</param>
        /// <param name="colorDetermine">The function to use for determining the color of the specified point in the texture.</param>
        /// <returns>A new texture with the specified colors at the specified points.</returns>
        public Texture2D CreateTexture(int width, int height, Func<Point, Color> colorDetermine)
        {
            Texture2D retVal = new Texture2D(Graphics, width, height);
            Color[] data = new Color[width * height];
            for (int w_en = 0; w_en < width; w_en++)
            {
                for (int h_en = 0; h_en < height; h_en++)
                {
                    data[h_en * width + w_en] = colorDetermine.Invoke(new Point(w_en, h_en));
                }
            }
            retVal.SetData(data);
            return retVal;
        }

        /// <summary>
        /// Creates a hollow circle with the specified radius.
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="color">The circle color.</param>
        /// <returns>A texture containing a hollow circle.</returns>
        public Texture2D CreateHollowCircle(float radius, Color color)
        {
            int diameter = Math.Ceiling(radius * 2).ToInt();
            Texture2D retVal = new Texture2D(Graphics, diameter, diameter);
            Color[] data = Enumerable.Repeat<Color>(Color.Transparent, retVal.Width * retVal.Height).ToArray();
            List<Vector2> points = GetOuterCirclePoints(new Vector2(Math.Ceiling(radius).ToFloat(), Math.Ceiling(radius).ToFloat()), radius, 0.5f);

            for (int w_en = 0; w_en < diameter; w_en++)
            {
                for (int h_en = 0; h_en < diameter; h_en++)
                {
                    foreach (Vector2 point in points)
                    {
                        //(Math.Ceiling(point.X).Round() == w_en && Math.Ceiling(point.Y).Round() == h_en) ||
                        if (  (Math.Floor(point.X).Round() == w_en && Math.Floor(point.Y).Round() == h_en))
                        {
                            data[h_en * diameter + w_en] = color;
                        }
                    }
                }
            }

            retVal.SetData(data);

            return retVal;
        }

        /// <summary>
        /// Creates a solid circle with the specified radius.
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="color">The circle color.</param>
        /// <returns>A texture containing a solid circle.</returns>
        public Texture2D CreateCircle(float radius, Color color)
        {
            int diameter = Math.Ceiling(radius * 2).ToInt();
            Texture2D retVal = new Texture2D(Graphics, diameter, diameter);
            Color[] data = Enumerable.Repeat<Color>(Color.Transparent, retVal.Width * retVal.Height).ToArray();
            List<Vector2> points = GetCirclePoints(new Vector2(Math.Ceiling(radius).ToFloat(), Math.Ceiling(radius).ToFloat()), radius, 0.95f);

            for (int w_en = 0; w_en < diameter; w_en++)
            {
                for (int h_en = 0; h_en < diameter; h_en++)
                {
                    foreach (Vector2 point in points)
                    {
                        if (float.IsNaN(point.X) || float.IsNaN(point.Y))
                        {
                            continue;
                        }

                        if ((Math.Floor(point.X).Round() == w_en && Math.Floor(point.Y).Round() == h_en))
                        {
                            data[h_en * diameter + w_en] = color;
                        }
                    }
                }
            }

            retVal.SetData(data);

            return retVal;
        }

        /// <summary>
        /// Creates a white square of the specified size.
        /// </summary>
        /// <param name="size">The width and height of the square.</param>
        /// <returns>A white texture which is a square of the specified size.</returns>
        public Texture2D CreateSquare(int size)
        {
            return CreateSquare(size, Color.White);
        }

        /// <summary>
        /// Creates a square of the specified size and color.
        /// </summary>
        /// <param name="size">The width and height of the square.</param>
        /// <param name="color">The color of the square.</param>
        /// <returns>A texture which is a square of the specified size and color.</returns>
        public Texture2D CreateSquare(int size, Color color)
        {
            return CreateRectangle(size, size, color);
        }

        /// <summary>
        /// Gets the points making up the specified circle. 
        /// </summary>
        /// <param name="centerPosition">The center position of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="step">The difference between X values within the circle. The higher this value, the more expensive this operation is and the more precise the circle is.</param>
        /// <returns>A list of circle points.</returns>
#if WINDOWS
        protected static List<Vector2> GetCirclePoints(Vector2 centerPosition, float radius, float step = 6.5f)
#else
        protected static List<Vector2> GetCirclePoints(Vector2 centerPosition, float radius, float step)
#endif
        {
            List<Vector2> points = new List<Vector2>();
            float actualStep = step / radius;

            for (float currentRadius = radius; currentRadius > 0; currentRadius -= actualStep)
            {
                for (float x = centerPosition.X - currentRadius; x <= centerPosition.X + currentRadius; x += actualStep)
                {
                    /* Solve for y based on: x^2 + y^2 = r^2 at center 0, 0
                                             (x-centerX)^2 + (y-centerY)^2 = r^2
                                             y = SqRt(r^2 - (x-centerX)^2) + centerY  */

                    //First point's y coordinate - bottom half
                    float y = (float)(Math.Sqrt(Math.Pow(currentRadius, 2) - Math.Pow(x - centerPosition.X, 2)) + centerPosition.Y);

                    //Second point's y coordinate - top half
                    float y1 = -(y - centerPosition.Y) + centerPosition.Y;

                    points.Add(new Vector2(x, y));
                    points.Add(new Vector2(x, y1));
                }
            }

            return points;
        }

        /// <summary>
        /// Gets the points making up the circumference of the specified circle. 
        /// </summary>
        /// <param name="centerPosition">The center position of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="step">The difference between X values within the circle. The higher this value, the more expensive this operation is and the more precise the circle is.</param>
        /// <returns>A list of circle points.</returns>
#if WINDOWS
        protected static List<Vector2> GetOuterCirclePoints(Vector2 centerPosition, float radius, float step = 1.5f)
#else
        protected static List<Vector2> GetOuterCirclePoints(Vector2 centerPosition, float radius, float step)
#endif
        {
            List<Vector2> points = new List<Vector2>();
            float actualStep = step / radius;

            for (float x = centerPosition.X - radius; x <= centerPosition.X + radius; x += actualStep)
            {
                /* Solve for y based on: x^2 + y^2 = r^2 at center 0, 0
                                         (x-centerX)^2 + (y-centerY)^2 = r^2
                                         y = SqRt(r^2 - (x-centerX)^2) + centerY  */

                //First point's y coordinate - bottom half
                float y = (float)(Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(x - centerPosition.X, 2)) + centerPosition.Y);

                //Second point's y coordinate - top half
                float y1 = -(y - centerPosition.Y) + centerPosition.Y;

                points.Add(new Vector2(x, y));
                points.Add(new Vector2(x, y1));
            }

            return points;
        }

        /// <summary>
        /// Creates a hollow rectangle of the specified color and size.
        /// </summary>
        /// <param name="width">The width of the new rectangular texture.</param>
        /// <param name="height">The height of the new rectangular texture.</param>
        /// <param name="color">The color of the rectangle border.</param>
        /// <returns>A hollow rectangular with a "color" border and of the specified size.</returns>
        /// <remarks>
        /// The returned texture will have an equivalent height and width to the parameters passed in, not bordering (1 pixel greater size on each side) a rectangle of the specified size.
        /// </remarks>
        public Texture2D CreateHollowRectangle(int width, int height, Color color)
        {
            Texture2D returnVal = new Texture2D(Graphics, width, height);
            Color[] data = new Color[width * height];
            for (int w_en = 0; w_en < width; w_en++)
            {
                for (int h_en = 0; h_en < height; h_en++)
                {
                    data[h_en * width + w_en] = w_en == 0 || w_en == width - 1 || h_en == 0 || h_en == height - 1 ? color : Color.Transparent;
                }
            }
            returnVal.SetData(data);
            return returnVal;
        }

        /// <summary>
        /// Creates a hollow white rectangle of the specified size.
        /// </summary>
        /// <param name="width">The width of the new rectangular texture.</param>
        /// <param name="height">The height of the new rectangular texture.</param>
        /// <returns>A hollow rectangular with a white border color and of the specified size.</returns>
        public Texture2D CreateHollowRectangle(int width, int height)
        {
            return CreateHollowRectangle(width, height, Color.White);
        }

        /// <summary>
        /// Creates a white rectangle of the specified size.
        /// </summary>
        /// <param name="width">The width of the new rectangular texture.</param>
        /// <param name="height">The height of the new rectangular texture.</param>
        /// <returns>A white rectangular texture of the specified size.</returns>
        public Texture2D CreateRectangle(int width, int height)
        {
            return CreateRectangle(width, height, Color.White);
        }

        /// <summary>
        /// Gets a one by one texture which is a white pixel.
        /// </summary>
        /// <remarks>
        /// This operation is expensive, so make minimal calls to it.
        /// </remarks>
        public Texture2D WhitePixel
        {
            get
            {
                return CreateSquare(1);
            }
        }

        /// <summary>
        /// Creates a rectangle of the specified size and color.
        /// </summary>
        /// <param name="width">The width of the new rectangular texture.</param>
        /// <param name="height">The height of the new rectangular texture.</param>
        /// <param name="color">The color of the new rectangular texture.</param>
        /// <returns>A rectangular texture of the specified size and color.</returns>
        public Texture2D CreateRectangle(int width, int height, Color color)
        {
            Texture2D returnVal = new Texture2D(Graphics, width, height);
            Color[] whiteColors = new Color[width * height];
            for (int i = 0; i < whiteColors.Length; i++)
            {
                whiteColors[i] = color;
            }
            returnVal.SetData<Color>(whiteColors);
            return returnVal;
        }
    }
}
