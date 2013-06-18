using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// A texture representing a 1x1 white pixel.
    /// </summary>
    public class WhitePixel2D : Texture2D
    {
        /// <summary>
        /// Create a new white dot.
        /// </summary>
        /// <param name="gd">The GraphicsDevice associeated with this texture.</param>
        public WhitePixel2D(GraphicsDevice gd)
            : base(gd, 1, 1)
        {
            this.SetData<Color>(new Color[] { Color.White });
        }
    }
}
