using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// A sprite which renders a rectangular hitbox around a screen object.
    /// </summary>
    /// <remarks>
    /// Intended for debugging purposes.
    /// </remarks>
    public class HitboxVisualizer : ISprite
    {
        /// <summary>
        /// Renders the hitbox.
        /// </summary>
        public void Draw()
        {
            if (!Visible || Texture == null || SpriteBatch == null || Object == null)
            {
                return;
            }
            SpriteBatch.Draw(Texture, GetTopLeft() - Vector2.One, Color.White);
        }

        /// <summary>
        /// Gets the top left corner of the tracked object.
        /// </summary>
        /// <returns>The top left hand corner of the tracked object.</returns>
        protected Vector2 GetTopLeft()
        {
            return Object.Position - ( (Object is IOriginPositionable ? (Object as IOriginPositionable).Origin : Vector2.Zero) * (Object is IScaled ? (Object as IScaled).Scale : Vector2.One) );
        }

        /// <summary>
        /// Creates a hitbox visualizer for the specified object, with a navy colored outline.
        /// </summary>
        /// <param name="obj">The object to render a rectangular hitbox around.</param>
        /// <param name="batch">The <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> to render the hitbox to.</param>
        public HitboxVisualizer(ISizedScreenObject obj, SpriteBatch batch)
            : this(obj, batch, Color.Navy)
        {
        }

        /// <summary>
        /// Creates a hitbox visualizer for the specified object.
        /// </summary>
        /// <param name="obj">The object to render a rectangular hitbox around.</param>
        /// <param name="batch">The <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> to render the hitbox to.</param>
        /// <param name="outline">The outline color to use.</param>
        public HitboxVisualizer(ISizedScreenObject obj, SpriteBatch batch, Color outline)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            if (batch == null)
            {
                throw new ArgumentNullException("batch");
            }
            _outlineColor = outline;
            _object = obj;
            _isVisible = true;
            Outlines = new Dictionary<Point, Texture2D>();
            _spriteBatch = batch;
            _textureCreator = new TextureFactory(batch.GraphicsDevice);
        }

        private Color _outlineColor;

        /// <summary>
        /// Gets or sets the color to render the outlines add.
        /// </summary>
        /// <remarks>
        /// Setting this property will wipe the texture cache.
        /// </remarks>
        public Color OutlineColor
        {
            get { return _outlineColor; }
            set
            {
                if (value != _outlineColor)
                {
                    _outlineColor = value;
                    Outlines.Clear();
                }
            }
        }


        private bool _isVisible;

        /// <summary>
        /// Gets or sets a boolean indicating whether this sprite is visible and performing applications.
        /// </summary>
        public bool Visible
        {
            get { return _isVisible; }
            set { _isVisible = value; }
        }

        /// <summary>
        /// Gets the texture for the current frame.
        /// </summary>
        public Texture2D Texture { get; private set; }

        /// <summary>
        /// Gets the dictionary mapping integral sizes of the tracked object to outline images.
        /// </summary>
        public Dictionary<Point, Texture2D> Outlines { get; private set; }

        private SpriteBatch _spriteBatch;

        /// <summary>
        /// Gets or sets the <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> to render the hitbox to.
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get { return _spriteBatch; }
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException();
                }
                _spriteBatch = value;
                _textureCreator = new TextureFactory(value.GraphicsDevice);
            }
        }

        private ISizedScreenObject _object;

        /// <summary>
        /// Gets or sets the object to render the hitbox around.
        /// </summary>
        public ISizedScreenObject Object
        {
            get { return _object; }
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException();
                }
                _object = value;
            }
        }

        private TextureFactory _textureCreator;

        /// <summary>
        /// Updates the texture of the hitbox.
        /// </summary>
        public void Update()
        {
            if (!Visible || Object == null || _textureCreator == null)
            {
                return;
            }

            if (Outlines == null)
            {
                Outlines = new Dictionary<Point, Texture2D>();
            }

            //Size is +2 to surround (but not cover) object
            Point currentSize = new Point(Object.Width.Round() + 2, Object.Height.Round() + 2);
            Texture2D image;
            if (!Outlines.TryGetValue(currentSize, out image) || image == null)
            {
                //Calculate image
                image = _textureCreator.CreateHollowRectangle(currentSize.X, currentSize.Y, OutlineColor);
                //Add image to cache
                Outlines[currentSize] = image;
            }
            //Set the temporary variable
            Texture = image;
        }
    }
}
