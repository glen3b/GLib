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
        /// Creates a set of textures that represents a fade between two images.
        /// </summary>
        /// <param name="first">The first image.</param>
        /// <param name="last">The last image.</param>
        /// <param name="frameCount">The number of frames to include in the phase.</param>
        /// <returns>An array of textures which can be used to create a phase effect.</returns>
        public static Texture2D[] CreateFade(Texture2D first, Texture2D last, int frameCount)
        {
            if (first == null)
            {
                throw new ArgumentNullException("first");
            }

            if (last == null)
            {
                throw new ArgumentNullException("last");
            }

            if (!first.GraphicsDevice.Equals(last.GraphicsDevice))
            {
                throw new ArgumentException("The GraphicsDevice is not consistent between the provided textures.");
            }

            if (frameCount < 1)
            {
                throw new ArgumentOutOfRangeException("frameCount");
            }

            if (last.Width != first.Width || last.Height != first.Height)
            {
                throw new ArgumentException("The images must be the same size.");
            }

            Texture2D[] frames = new Texture2D[frameCount];
            Color[] initialData = new Color[first.Width * first.Height];
            Color[] finalData = new Color[last.Width * last.Height];
            first.GetData(initialData);
            last.GetData(finalData);

            //initialdata + difference = finaldata
            Vector4[] differences = new Vector4[initialData.Length];
            Vector4[] currentData = new Vector4[initialData.Length];

            for (int i = 0; i < differences.Length; i++)
            {
                differences[i] = finalData[i].ToVector4() - initialData[i].ToVector4();
                currentData[i] = initialData[i].ToVector4();
            }


            for (int frame = 0; frame < frameCount; frame++)
            {
                Texture2D image = new Texture2D(first.GraphicsDevice, first.Width, first.Height);
                Color[] imageData = new Color[image.Width * image.Height];

                for (int i = 0; i < initialData.Length; i++)
                {
                    imageData[i] = Color.FromNonPremultiplied(currentData[i]);
                    currentData[i] += differences[i] / frameCount.ToFloat();
                }

                image.SetData(imageData);

                frames[frame] = image;
            }

            return frames;
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
        /// Represents an effect of an image overlay.
        /// </summary>
        public enum OverlayType
        {
            /// <summary>
            /// Replaces pixels of the main image completely with overlay image pixels.
            /// </summary>
            Replace,
            /// <summary>
            /// Uses the average of the ARGB value of the pixels of the main image and of the overlay image.
            /// </summary>
            Merge,
            /// <summary>
            /// Uses the sum of the ARGB value of the pixels of the main image and of the overlay image.
            /// </summary>
            AddMerge
        }

        /// <summary>
        /// Overlays an image onto another image, positioning the overlay at the top left corner. The image overlay is done in-place.
        /// </summary>
        /// <param name="main">The image to overlay on to (in-place operation).</param>
        /// <param name="overlay">The image to overlay.</param>
        public static void OverlayImage(Texture2D main, Texture2D overlay)
        {
            OverlayImage(main, overlay, Point.Zero);
        }

        /// <summary>
        /// Overlays an image onto another image, positioning the overlay at the top left corner. The image overlay is done in-place.
        /// </summary>
        /// <param name="main">The image to overlay on to (in-place operation).</param>
        /// <param name="overlay">The image to overlay.</param>
        /// <param name="origin">The position of the overlay.</param>
        public static void OverlayImage(Texture2D main, Texture2D overlay, Point origin)
        {
            OverlayImage(main, overlay, origin, OverlayType.Replace);
        }

        /// <summary>
        /// Overlays an image onto another image. The image overlay is done in-place.
        /// </summary>
        /// <param name="main">The image to overlay on to (in-place operation).</param>
        /// <param name="overlay">The image to overlay.</param>
        /// <param name="origin">The position of the overlay.</param>
        /// <param name="overlayType">The effect of the overlay</param>
        /// <returns>An image of the same dimensions as the main image with all non-transparent pixels of the overlay image modifying pixels of the main image.</returns>
        public static void OverlayImage(Texture2D main, Texture2D overlay, Point origin, OverlayType overlayType)
        {
            if (main == null)
            {
                throw new ArgumentNullException("main");
            }

            if (overlay == null)
            {
                throw new ArgumentNullException("overlay");
            }

            if (!main.GraphicsDevice.Equals(overlay.GraphicsDevice))
            {
                throw new ArgumentException("The GraphicsDevice is not consistent between the provided textures.");
            }

            if (origin.X < 0 || origin.Y < 0)
            {
                throw new ArgumentException("The origin position must be at positive coordinates.");
            }

            if (overlay.Width > main.Width || overlay.Height > main.Height)
            {
                throw new ArgumentException("The overlay image must be smaller or the same size as the main image.");
            }

            Color[] mainData = new Color[main.Width * main.Height];
            Color[] overlayData = new Color[overlay.Width * overlay.Height];
            main.GetData(mainData);
            overlay.GetData(overlayData);
            for (int w = 0; w < main.Width; w++)
            {
                for (int h = 0; h < main.Height; h++)
                {
                    if (h < overlay.Height && w < overlay.Width && h + origin.Y < main.Height && w + origin.X < main.Width && h * overlay.Width + w < overlayData.Length && overlayData[h * overlay.Width + w] != Color.Transparent)
                    {
                        Color targetColor = overlayData[h * overlay.Width + w];
                        if (overlayType == OverlayType.Merge)
                        {
                            targetColor = Color.FromNonPremultiplied((targetColor.ToVector4() + mainData[(h + origin.Y) * main.Width + (w + origin.X)].ToVector4()) / 2.0F);
                        }
                        else if (overlayType == OverlayType.AddMerge)
                        {
                            targetColor = Color.FromNonPremultiplied(mainData[(h + origin.Y) * main.Width + (w + origin.X)].ToVector4() + targetColor.ToVector4());
                        }
                        mainData[(h + origin.Y) * main.Width + (w + origin.X)] = targetColor;
                    }
                }
            }

            main.SetData(mainData);
        }

        /// <summary>
        /// Crops the whitespace off of the specified image.
        /// Whitespace is treated as bounding transparency and other colors.
        /// </summary>
        /// <param name="original">The image to crop.</param>
        /// <param name="whitespaceColors">Colors (other than transparency) to treat as whitespace.</param>
        /// <returns>A cropped texture of potentially smaller dimensions.</returns>
        public static Texture2D CropWhitespace(Texture2D original, params Color[] whitespaceColors)
        {
            if (original == null)
            {
                throw new ArgumentNullException("original");
            }

            if (whitespaceColors == null)
            {
                whitespaceColors = new Color[0];
            }

            Color[] data = new Color[original.Height * original.Width];

            original.GetData(data);

            int whiterowsTop = 0;

            for (int y = 0; y < original.Height; y++)
            {
                bool fail = false;
                for (int x = 0; x < original.Width; x++)
                {
                    if (data[y * original.Width + x] == Color.Transparent || whitespaceColors.Contains(data[y * original.Width + x]))
                    {
                        fail = true;
                        break;
                    }
                }
                if (fail)
                {
                    break;
                }
                whiterowsTop++;
            }

            int whiterowsBottom = 0;

            for (int y = 0; y < original.Height; y++)
            {
                bool fail = false;
                for (int x = original.Width - 1; x >= 0; x--)
                {
                    if (data[y * original.Width + x] == Color.Transparent || whitespaceColors.Contains(data[y * original.Width + x]))
                    {
                        fail = true;
                        break;
                    }
                }
                if (fail)
                {
                    break;
                }
                whiterowsBottom++;
            }

            if (whiterowsTop + whiterowsBottom >= original.Height)
            {
                throw new ArgumentException("The specified image contains only whitespace.");
            }

            int whitecolumnsLeft = 0;

            for (int x = 0; x < original.Width; x++)
            {
                bool fail = false;
                for (int y = 0; y < original.Height; y++)
                {
                    if (data[y * original.Width + x] == Color.Transparent || whitespaceColors.Contains(data[y * original.Width + x]))
                    {
                        fail = true;
                        break;
                    }
                }
                if (fail)
                {
                    break;
                }
                whitecolumnsLeft++;
            }

            int whitecolumnsRight = 0;

            for (int x = 0; x < original.Width; x++)
            {
                bool fail = false;
                for (int y = original.Height - 1; y >= 0; y--)
                {
                    if (data[y * original.Width + x] == Color.Transparent || whitespaceColors.Contains(data[y * original.Width + x]))
                    {
                        fail = true;
                        break;
                    }
                }
                if (fail)
                {
                    break;
                }
                whitecolumnsRight++;
            }

            Texture2D newImg = new Texture2D(original.GraphicsDevice, original.Width - (whitecolumnsLeft + whitecolumnsRight), original.Height - (whiterowsTop + whiterowsBottom));
            Color[] dataFromOld = new Color[newImg.Width * newImg.Height];
            original.GetData<Color>(dataFromOld, whitecolumnsLeft * original.Width + whiterowsTop, newImg.Height * newImg.Width);
            newImg.SetData(dataFromOld);

            return newImg;
        }

        /// <summary>
        /// Replaces the specified colors in the specified image with other colors.
        /// The color replacement is done in-place.
        /// </summary>
        /// <param name="image">The image to replace the colors in (done in place).</param>
        /// <param name="colors">A dictionary specifying colors to replace and what colors to use instead.</param>
        public void ReplaceColors(Texture2D image, IDictionary<Color, Color> colors)
        {
            Color[] data = new Color[image.Width * image.Height];
            image.GetData(data);

            foreach (var colorMatch in colors)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    if (colorMatch.Key == data[i])
                    {
                        data[i] = colorMatch.Value;
                    }
                }
            }

            image.SetData(data);
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
            List<Vector2> points = GetOuterCirclePoints(new Vector2(Math.Ceiling(radius).ToFloat(), Math.Ceiling(radius).ToFloat()), radius, 0.08F);

            for (int w_en = 0; w_en < diameter; w_en++)
            {
                for (int h_en = 0; h_en < diameter; h_en++)
                {
                    foreach (Vector2 point in points)
                    {
                        //(Math.Ceiling(point.X).Round() == w_en && Math.Ceiling(point.Y).Round() == h_en) ||
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
            List<Vector2> points = GetCirclePoints(new Vector2(Math.Ceiling(radius).ToFloat(), Math.Ceiling(radius).ToFloat()), radius, 0.8f);

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

        private Texture2D _whitePixel;

        /// <summary>
        /// Gets a one by one texture which is a white pixel. Cached internally, so it is only created once.  It is not safe to modify this texture reference without breaking other code.
        /// </summary>
        public Texture2D WhitePixel
        {
            get
            {
                if (_whitePixel == null)
                {
                    _whitePixel = CreateSquare(1, Color.White);
                }
                return _whitePixel;
            }
        }

        /// <summary>
        /// Returns a new <see cref="Texture2D"/> object with the same color data as the original.
        /// Mipmap values are not copied over. Null arguments are handled, and passing null will result in returning a null value.
        /// If the tag object on the original texture implements <see cref="IClonable"/>, it will be cloned as well.
        /// </summary>
        /// <param name="original">The original texture which should be cloned.</param>
        /// <returns>A new <see cref="Texture2D"/> instance with the same color data as the original.</returns>
        public static Texture2D Clone(Texture2D original)
        {
            if (original == null)
            {
                return null;
            }

            // Mipmap defaults to false
            Texture2D clone = new Texture2D(original.GraphicsDevice, original.Width, original.Height, false, original.Format);
            clone.Tag = original.Tag is ICloneable ? ((ICloneable)original.Tag).Clone() : original.Tag;
            Color[] data = new Color[clone.Width * clone.Height];
            original.GetData(data);
            clone.SetData(data);

            return clone;
        }

        private Texture2D _transparentPixel;

        /// <summary>
        /// Gets a one by one texture which is a transparent pixel. Cached internally, so it is only created once. It is not safe to modify this texture reference without breaking other code.
        /// </summary>
        public Texture2D TransparentPixel
        {
            get
            {
                if (_transparentPixel == null)
                {
                    _transparentPixel = CreateSquare(1, Color.Transparent);
                }
                return _transparentPixel;
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
