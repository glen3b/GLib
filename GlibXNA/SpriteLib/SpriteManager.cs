using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Glib.XNA.InputLib;
using System.Diagnostics;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// Manages multiple <seealso cref="Sprite"/> objects on the same SpriteBatch.
    /// </summary>
    [DebuggerDisplay("Count = {Count}")]
    public class SpriteManager : ISprite, ISpriteBatchManagerSprite, ITimerSprite, ICollection<Sprite>
    {
        /// <summary>
        /// Gets the list of <seealso cref="Sprite"/>s managed by this SpriteManager.
        /// </summary>
        [Obsolete("SpriteManager now implements ICollection, please use that instead.")]
        public List<Sprite> Sprites
        {
            get
            {
                return _sprites;
            }
        }


        private List<Sprite> _sprites = new List<Sprite>();
        private SpriteBatch _sb = null;

        /// <summary>
        /// Gets the <seealso cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> drawn to.
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get
            {
                return _sb;
            }
        }

        private int _i = 0;
        
        /// <summary>
        /// Get or set the <seealso cref="Sprite"/> with the specified index in the Sprites list.
        /// </summary>
        /// <remarks>
        /// Returns null if the index is out of bounds of the array, instead of throwing an exception.
        /// </remarks>
        /// <param name="index">The index in the Sprites list.</param>
        /// <returns>The <seealso cref="Sprite"/> with the specified index in the Sprites list.</returns>
        public Sprite this[int index]
        {
            get
            {
                if (index < Count)
                {
                    return _sprites[index];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                _sprites[index] = value;
            }
        }

        /// <summary>
        /// Add a new <seealso cref="Sprite"/> with the specified position and texture, and returns it.
        /// </summary>
        /// <param name="position">The position of the new <seealso cref="Sprite"/>.</param>
        /// <param name="texture">The texture of the new <seealso cref="Sprite"/>.</param>
        /// <returns>The created Sprite.</returns>
        public Sprite AddNewSprite(Microsoft.Xna.Framework.Vector2 position, Texture2D texture)
        {
            Sprite sprToAdd = new Sprite(texture, position, _sb);
            Add(sprToAdd);
            return sprToAdd;
        }

        /// <summary>
        /// Remove a <seealso cref="Sprite"/> from this SpriteManager.
        /// Safe to call during Update or Draw (or from their corresponding events).
        /// </summary>
        /// <param name="spr">The <seealso cref="Sprite"/> to remove.</param>
        internal void RemoveSelf(Sprite spr)
        {
            Remove(spr);
            _i--;
        }

        /// <summary>
        /// Add a <seealso cref="Sprite"/> to this SpriteManager.
        /// </summary>
        /// <param name="spr">The <seealso cref="Sprite"/> to add.</param>
        public void Add(Sprite spr)
        {
            spr.SpriteManager = this;
            _sprites.Add(spr);
        }

        /// <summary>
        /// Remove a given <seealso cref="Sprite"/>, that is NOT the <seealso cref="Sprite"/> being updated.
        /// </summary>
        /// <param name="spr">The <seealso cref="Sprite"/> to remove.</param>
        public bool Remove(Sprite spr)
        {
            return _sprites.Remove(spr);
        }

        /// <summary>
        /// Construct a new SpriteManager.
        /// </summary>
        /// <param name="sb">The SpriteBatch to use.</param>
        /// <param name="sprites">The <seealso cref="Sprite"/>s to add to the SpriteManager.</param>
        public SpriteManager(SpriteBatch sb, params Sprite[] sprites)
        {
            _sb = sb;
            foreach (Sprite s in sprites)
            {
                s.SpriteBatch = sb;
                s.SpriteManager = this;
                _sprites.Add(s);
            }
        }

        /// <summary>
        /// Draw all <seealso cref="Sprite"/>s managed by this SpriteManager.
        /// </summary>
        public void Draw()
        {
            _sb.Begin();
            DrawNonAuto();
            _sb.End();
        }

        /// <summary>
        /// Draw all <seealso cref="Sprite"/>s managed by this SpriteManager, without opening or closing the SpriteBatch.
        /// </summary>
        public virtual void DrawNonAuto()
        {
            for (_i = 0; _i < _sprites.Count; _i++)
            {
                _sprites[_i].DrawNonAuto();
            }
        }

        /// <summary>
        /// Update all <seealso cref="Sprite"/>s managed by this <seealso cref="SpriteManager"/>.
        /// </summary>
        /// <remarks>
        /// Does not call Update(GameTime) on subclasses of Sprite implementing ITimerSprite.
        /// The MouseState for click checking is InputLib.Mouse.MouseManager.CurrentMouseState.
        /// </remarks>
        public virtual void Update()
        {
            for (_i = 0; _i < _sprites.Count; _i++ )
            {
                    this[_i].Update();
            }
        }

        /// <summary>
        /// Update all <seealso cref="Sprite"/>s managed by this <seealso cref="SpriteManager"/>, calling Update(GameTime) on ITimerSprites where neccesary.
        /// </summary>
        /// <remarks>
        /// Uses InputLib.Mouse.MouseManager.CurrentMouseState.
        /// </remarks>
        /// <param name="gameTime">The current game time.</param>
        public virtual void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            for (_i = 0; _i < _sprites.Count; _i++)
            {
                if (this[_i] is ITimerSprite)
                {
                    this[_i].Cast<ITimerSprite>().Update(gameTime);
                }
                else
                {
                    this[_i].Update();
                }
            }
        }

        /// <summary>
        /// Removes all Sprites from this <seealso cref="SpriteManager"/>.
        /// </summary>
        public void Clear()
        {
            _sprites.Clear();
        }

        /// <summary>
        /// Determines whether the specified <seealso cref="Sprite"/> is in this <seealso cref="SpriteManager"/>.
        /// </summary>
        /// <param name="item">The <seealso cref="Sprite"/> to locate in this <seealso cref="SpriteManager"/>.</param>
        /// <returns>Whether or not this <seealso cref="SpriteManager"/> contains the specified <seealso cref="Sprite"/>.</returns>
        public bool Contains(Sprite item)
        {
            return _sprites.Contains(item);
        }

        /// <summary>
        /// Copies the entire <seealso cref="SpriteManager"/> into the specified array, beginning at the specified index.
        /// </summary>
        /// <param name="array">The one-dimensional array of <seealso cref="Sprite"/>s to copy into.</param>
        /// <param name="arrayIndex">The zero-based index in the array at which copying begins.</param>
        public void CopyTo(Sprite[] array, int arrayIndex)
        {
            _sprites.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of <seealso cref="Sprite"/>s in this <seealso cref="SpriteManager"/>.
        /// </summary>
        public int Count
        {
            get { return _sprites.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether or not this <seealso cref="SpriteManager"/> is read-only.
        /// </summary>
        /// <remarks>
        /// For a <seealso cref="SpriteManager"/>, this value is always false.
        /// </remarks>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Returns an enumerator that iterates through this <seealso cref="SpriteManager"/>.
        /// </summary>
        /// <returns>An enumerator that iterates through this <seealso cref="SpriteManager"/>.</returns>
        public IEnumerator<Sprite> GetEnumerator()
        {
            return _sprites.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through this <seealso cref="SpriteManager"/>.
        /// </summary>
        /// <returns>An enumerator that iterates through this <seealso cref="SpriteManager"/>.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _sprites.GetEnumerator();
        }
    }
}
