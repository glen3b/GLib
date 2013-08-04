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
