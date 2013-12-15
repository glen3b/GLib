using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glib.XNA;
using Glib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// A shadow that shadows text.
    /// </summary>
    public class TextShadow : Shadow<TextSprite>
    {
        /// <summary>
        /// Creates a shadow of a <see cref="TextSprite"/>.
        /// </summary>
        /// <param name="shadowed">The shadowed object.</param>
        /// <param name="relativeShadowPos">The position of the shadow relative to the <see cref="TextSprite"/>.</param>
        /// <param name="shadowColor">The color of the shadow.</param>
        public TextShadow(TextSprite shadowed, Vector2 relativeShadowPos, Color shadowColor) : base(shadowed, relativeShadowPos)
        {
            ShadowColor = shadowColor;
        }

        private Color _shadowColor;

        /// <summary>
        /// Gets or sets the color of the shadow.
        /// </summary>
        public Color ShadowColor
        {
            get { return _shadowColor; }
            set { _shadowColor = value; }
        }

        /// <summary>
        /// Creates a shadow of a <see cref="TextSprite"/>.
        /// </summary>
        /// <param name="shadowed">The shadowed object.</param>
        public TextShadow(TextSprite shadowed) : this(shadowed, Vector2.One)
        {

        }

        /// <summary>
        /// Creates a shadow of a <see cref="TextSprite"/>.
        /// </summary>
        /// <param name="shadowed">The shadowed object.</param>
        /// <param name="shadowColor">The color of the shadow.</param>
        public TextShadow(TextSprite shadowed, Color shadowColor) : this(shadowed, Vector2.One, shadowColor)
        {

        }

        /// <summary>
        /// Creates a shadow of a <see cref="TextSprite"/>.
        /// </summary>
        /// <param name="shadowed">The shadowed object.</param>
        /// <param name="relativeShadowPos">The position of the shadow relative to the <see cref="TextSprite"/>.</param>
        public TextShadow(TextSprite shadowed, Vector2 relativeShadowPos) : this(shadowed, relativeShadowPos, new Color(byte.MaxValue-shadowed.Color.R, byte.MaxValue-shadowed.Color.G, byte.MaxValue-shadowed.Color.B, shadowed.Color.A))
        {

        }

        /// <summary>
        /// Draws the shadow of the shadowed object.
        /// </summary>
        public override void Draw()
        {
            if (ShadowedObject == null)
            {
                throw new InvalidOperationException();
            }

            if (!ShadowedObject.Visible)
            {
                return;
            }

            ShadowedObject.SpriteBatch.DrawString(ShadowedObject.Font, ShadowedObject.Text, Position, ShadowColor, ShadowedObject.Rotation.Radians, Vector2.Zero, ShadowedObject.Scale, SpriteEffects.None, 1);
        }
    }
}
