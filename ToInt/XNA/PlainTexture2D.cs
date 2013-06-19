using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Glib.XNA
{
    /// <summary>
    /// A texture representing a single-color plain, by default a 1x1 white pixel
    /// </summary>
    public class PlainTexture2D : Texture2D
    {
        /// <summary>
        /// Create a new white pixel (as a PlainTexture2D).
        /// </summary>
        /// <param name="gd">The GraphicsDevice associated with this texture.</param>
        public PlainTexture2D(GraphicsDevice gd)
            : this(gd, 1,1)
        {
            
        }

        /// <summary>
        /// Create a new sets of pixels of the specified color.
        /// </summary>
        /// <param name="gd">The GraphicsDevice associated with this texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="textureColor">The color of the texture.</param>
        public PlainTexture2D(GraphicsDevice gd, int width, int height, Color textureColor)
            : base(gd, width, height)
        {
            Color[] whiteColors = new Color[width * height];
            for (int i = 0; i < whiteColors.Length; i++)
            {
                whiteColors[i] = textureColor;
            }
            this.SetData<Color>(whiteColors);
        }

        /// <summary>
        /// Create a new set of white pixels of the specified size.
        /// </summary>
        /// <param name="gd">The GraphicsDevice associated with this texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="width">The width of the texture.</param>
        public PlainTexture2D(GraphicsDevice gd, int width, int height)
            : this(gd, width, height, Color.White)
        {
            
        }
    }
}
