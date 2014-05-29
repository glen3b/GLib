using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// Represents the simplest possible complete bitmap wrapper.
    /// </summary>
    public class TextureWrapper : IDrawableComponent, ISizedScreenObject, ITexturable
    {
        /// <summary>
        /// Creates a texture wrapper instance.
        /// </summary>
        /// <param name="batch">The SpriteBatch to render onto.</param>
        /// <param name="texture">The texture to render.</param>
        public TextureWrapper(SpriteBatch batch, Texture2D texture)
        {
            SpriteBatch = batch;
            Texture = texture;
        }

        private Vector2 _position;

        /// <summary>
        /// Gets or sets the screen location at which to render the texture.
        /// </summary>
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        

        private Color _color = Color.White;

        /// <summary>
        /// Gets or sets the tint color of the rendered texture.
        /// </summary>
        public Color Tint
        {
            get { return _color; }
            set { _color = value; }
        }
        

        /// <summary>
        /// The SpriteBatch used for drawing the sprite.
        /// </summary>
        private SpriteBatch _spriteBatch;

        /// <summary>
        /// Gets or sets the <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> to render the texture to.
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
                if (value.IsDisposed)
                {
                    throw new ObjectDisposedException("SpriteBatch");
                }
                _spriteBatch = value;
            }
        }

        /// <summary>
        /// Renders the texture to the screen using the cached values.
        /// </summary>
        public void Draw()
        {
            this.SpriteBatch.Draw(Texture, Position, Tint);
        }

        private Texture2D _texture;

        /// <summary>
        /// Gets or sets the texture to render.
        /// </summary>
        public Texture2D Texture
        {
            get { return _texture; }
            set {
                if (value == null)
                {
                    throw new NullReferenceException();
                }
                if (value.IsDisposed)
                {
                    throw new ObjectDisposedException("Texture");
                }
                _texture = value; }
        }
        
        /// <summary>
        /// Gets the width of the underlying texture.
        /// </summary>
        public float Width
        {
            get { return Texture.Width; }
        }

        /// <summary>
        /// Gets the height of the underlying texture.
        /// </summary>
        public float Height
        {
            get { return Texture.Height; }
        }
    }
}
