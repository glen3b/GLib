using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// A texture representing a set of pixels of the same color, be default 1x1 white.
    /// </summary>
    public class WhitePixel2D : Texture2D
    {
        /// <summary>
        /// Create a new white dot.
        /// </summary>
        /// <param name="gd">The GraphicsDevice associeated with this texture.</param>
        public WhitePixel2D(GraphicsDevice gd)
            : this(gd, 1,1)
        {
            
        }

        /// <summary>
        /// Create a new dot.
        /// </summary>
        /// <param name="gd">The GraphicsDevice associeated with this texture.</param>
        /// <param name="height">The height of the white pixel.</param>
        /// <param name="width">The width of the white pixel.</param>
        /// <param name="textureColor">The color of the texture.</param>
        public WhitePixel2D(GraphicsDevice gd, int width, int height, Color textureColor)
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
        /// Create a new white dot.
        /// </summary>
        /// <param name="gd">The GraphicsDevice associeated with this texture.</param>
        /// <param name="height">The height of the white pixel.</param>
        /// <param name="width">The width of the white pixel.</param>
        public WhitePixel2D(GraphicsDevice gd, int width, int height)
            : this(gd, width, height, Color.White)
        {
            
        }
    }
}
