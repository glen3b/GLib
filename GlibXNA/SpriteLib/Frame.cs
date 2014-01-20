using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// Represents a frame of a <see cref="SpriteSheet"/>.
    /// </summary>
    public class Frame
    {
        private Texture2D _texture;

        /// <summary>
        /// Gets or sets the texture of this frame.
        /// </summary>
        public virtual Texture2D Texture
        {
            get { return _texture; }
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException();
                }
                _texture = value;
            }
        }

        private Vector2 _origin = Vector2.Zero;

        /// <summary>
        /// Gets or sets the origin of the frame.
        /// </summary>
        public virtual Vector2 Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }

        private Vector2 _scale = Vector2.One;

        /// <summary>
        /// Gets or sets the scale of the frame.
        /// </summary>
        public virtual Vector2 Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        private Rectangle? _drawRegion;

        /// <summary>
        /// Gets or sets the draw region.
        /// </summary>
        /// <remarks>
        /// Does not account for scale.
        /// </remarks>
        public Rectangle? DrawRegion
        {
            get { return _drawRegion; }
            set { _drawRegion = value; }
        }

        /// <summary>
        /// Creates a frame.
        /// </summary>
        /// <param name="image">The image which is this frame.</param>
        public Frame(Texture2D image)
            : this(image, null)
        {

        }

        /// <summary>
        /// Creates a frame.
        /// </summary>
        /// <param name="image">The image which contains this frame.</param>
        /// <param name="drawRegion">The region of the image to render as this frame.</param>
        public Frame(Texture2D image, Rectangle? drawRegion) : this(image, drawRegion, Vector2.One) { }

        /// <summary>
        /// Creates a frame.
        /// </summary>
        /// <param name="image">The image which contains this frame.</param>
        /// <param name="drawRegion">The region of the image to render as this frame.</param>
        /// <param name="scale">The scale of the frame.</param>
        public Frame(Texture2D image, Rectangle? drawRegion, Vector2 scale)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }
            _texture = image;
            _drawRegion = drawRegion;
            _scale = scale;
        }
    }
}
