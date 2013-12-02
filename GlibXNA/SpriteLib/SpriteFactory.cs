using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// A class to construct sprites easily.
    /// </summary>
    [Obsolete("Use an object initializer.")]
    public class SpriteBuilder
    {
        private Sprite _constructing;

        /// <summary>
        /// Make a new SpriteBuilder.
        /// </summary>
        public SpriteBuilder(Texture2D texture, Vector2 position, SpriteBatch sb)
        {
            _constructing = new Sprite(texture, position, sb);
        }

        /// <summary>
        /// Make a new SpriteBuilder.
        /// </summary>
        public SpriteBuilder(Microsoft.Xna.Framework.Content.ContentManager textures, String texturename, Vector2 position, SpriteBatch sb)
        {
            _constructing = new Sprite(textures.Load<Texture2D>(texturename), position, sb);
        }

        /// <summary>
        /// Set the DrawRegion of the Sprite.
        /// </summary>
        /// <param name="region">The new DrawRegion of the Sprite.</param>
        /// <returns>This SpriteBuilder.</returns>
        public SpriteBuilder SetDrawRegion(Rectangle region)
        {
            _constructing.DrawRegion = region;
            return this;
        }

        /// <summary>
        /// Remove the DrawRegion from the Sprite, drawing the entire Sprite.
        /// </summary>
        /// <returns>This SpriteBuilder.</returns>
        public SpriteBuilder RemoveDrawRegion()
        {
            _constructing.OnlyDrawRegion = false;
            return this;
        }

        /// <summary>
        /// Set the color of the Sprite under construction.
        /// </summary>
        /// <param name="c">The color to use.</param>
        /// <returns>This SpriteBuilder.</returns>
        public SpriteBuilder SetColor(Color c)
        {
            _constructing.Color = c;
            return this;
        }

        /// <summary>
        /// Set the width of the Sprite under construction via scaling.
        /// </summary>
        /// <param name="w">The new width.</param>
        /// <returns>This SpriteBuilder.</returns>
        public SpriteBuilder SetWidth(float w)
        {
            _constructing.Width = w;
            return this;
        }

        /// <summary>
        /// Set the <seealso cref="UpdateParamaters"/> for this Sprite.
        /// </summary>
        /// <param name="u">The UpdateParamaters to use for this Sprite.</param>
        /// <returns>This SpriteBuilder.</returns>
        public SpriteBuilder SetUpdateParamaters(UpdateParamaters u)
        {
            _constructing.UpdateParams = u;
            return this;
        }

        /// <summary>
        /// Set the viewport this Sprite should use.
        /// </summary>
        /// <param name="v">The Viewport this Sprite should use.</param>
        /// <returns>This SpriteBuilder.</returns>
        public SpriteBuilder SetViewport(Viewport v)
        {
            _constructing.UsedViewport = v;
            return this;
        }

        /// <summary>
        /// Set the velocity of the sprite.
        /// </summary>
        /// <param name="v">The new Sprite velocity.</param>
        /// <returns>This SpriteBuilder.</returns>
        [Obsolete("Please use SetSpeed instead.", true)]
        public SpriteBuilder SetVelocity(Velocity v)
        {
            if (v.XVelocity.HasValue)
            {
                _constructing.XSpeed = v.XVelocity.Value;
            }
            if (v.YVelocity.HasValue)
            {
                _constructing.YSpeed = v.YVelocity.Value;
            }
            return this;
        }

        /// <summary>
        /// Set the speed of this Sprite.
        /// </summary>
        /// <param name="xspeed">The speed in X.</param>
        /// <param name="yspeed">The speed in Y.</param>
        /// <returns>This SpriteBuilder.</returns>
        public SpriteBuilder SetSpeed(float xspeed, float yspeed)
        {
            _constructing.UpdateParams.UpdateX = true;
            _constructing.UpdateParams.UpdateY = true;
            _constructing.Speed = new Vector2(xspeed, yspeed);
            return this;
        }

        /// <summary>
        /// Register an event for when the Sprite is drawn.
        /// </summary>
        /// <param name="drawEvent">The SpriteEventHandler for the Drawn event.</param>
        /// <returns>This SpriteBuilder.</returns>
        public SpriteBuilder RegisterDrawEvent(EventHandler drawEvent)
        {
            _constructing.Drawn += drawEvent;
            return this;
        }

        /// <summary>
        /// Register an event for when the Sprite moves.
        /// </summary>
        /// <param name="moveEvent">The SpriteMoveEventHandler for the Move event.</param>
        /// <returns>This SpriteBuilder.</returns>
        public SpriteBuilder RegisterMoveEvent(SpriteMoveEventHandler moveEvent)
        {
            _constructing.Move += moveEvent;
            return this;
        }

        /// <summary>
        /// Return the Sprite under construction.
        /// </summary>
        /// <returns>The Sprite under construction.</returns>
        [Obsolete("Use the Built property instead.")]
        public Sprite Build()
        {
            return _constructing;
        }

        /// <summary>
        /// Gets the Sprite under construction.
        /// </summary>
        public Sprite Built
        {
            get
            {
                return _constructing;
            }
        }

        /// <summary>
        /// Set the height of the Sprite under construction via scaling.
        /// </summary>
        /// <param name="h">The new height.</param>
        /// <returns>This SpriteBuilder.</returns>
        public SpriteBuilder SetHeight(float h)
        {
            _constructing.Height = h;
            return this;
        }

        /// <summary>
        /// Register an update event handler for the Sprite under construction.
        /// </summary>
        /// <param name="updateEvent">The update event to register.</param>
        /// <returns>This SpriteBuilder.</returns>
        public SpriteBuilder RegisterUpdateEvent(EventHandler updateEvent)
        {
            _constructing.Updated += updateEvent;
            return this;
        }

        /// <summary>
        /// Set the scale of the Sprite under construction.
        /// </summary>
        /// <param name="scale">The scale to assign to the Sprite.</param>
        /// <returns>This SpriteBuilder.</returns>
        public SpriteBuilder SetScale(Vector2 scale)
        {
            _constructing.Scale = scale;
            return this;
        }

        /// <summary>
        /// Set the scale of the Sprite under construction.
        /// </summary>
        /// <param name="scalex">The X scale to assign to the Sprite.</param>
        /// <param name="scaley">The Y scale to assign to the Sprite.</param>
        /// <returns>This SpriteBuilder.</returns>
        public SpriteBuilder SetScale(float scalex, float scaley)
        {
            _constructing.Scale = new Vector2(scalex, scaley);
            return this;
        }

    }
}
