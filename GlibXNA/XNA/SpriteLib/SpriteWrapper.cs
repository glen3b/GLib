using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// A class for managing multiple instances of any implementation of ISprite using the same SpriteBatch easily.
    /// </summary>
    public class SpriteWrapper : ISprite, ITimerSprite
    {
        private List<ISprite> _sprites;
        private Updater _updater = null;

        /// <summary>
        /// The updater to use when updating ISprites in this SpriteWrapper.
        /// </summary>
        public Updater Updater {

            get
            {
                return _updater;
            }
            set
            {
                _updater = value;
                _updater._sw = this;
            }


        }
        private SpriteBatch _sb;
        internal int i = 0;

        /// <summary>
        /// A collection of all ISprites managed by this SpriteWrapper.
        /// </summary>
        public ICollection<ISprite> Sprites
        {
            get
            {
                return _sprites;
            }
        }
        /// <summary>
        /// Add a new ISprite to this SpriteManager.
        /// </summary>
        /// <param name="spr">The ISprite to add.</param>
        public void Add(ISprite spr)
        {
            _sprites.Add(spr);
        }

        /// <summary>
        /// Create a new SpriteWrapper.
        /// </summary>
        /// <param name="spritestart">The Collection of ISprites to initialize Sprites with.</param>
        /// <param name="sb">The SpriteBatch to use.</param>
        public SpriteWrapper(SpriteBatch sb, params ISprite[] spritestart)
        {
            if (sb == null)
            {
                throw new ArgumentNullException("sb");
            }
            if (spritestart == null)
            {
                throw new ArgumentNullException("spritestart");
            }
            _sb = sb;
            _sprites = (spritestart == null ? new List<ISprite>() : spritestart.ToList());
        }

        /// <summary>
        /// Create a new SpriteWrapper.
        /// </summary>
        /// <param name="spritestart">The Collection of ISprites to initialize Sprites with.</param>
        /// <param name="sb">The SpriteBatch to use.</param>
        /// <param name="updates">The Updater to use for updating ISprite objects (in addition to the Update() method).</param>
        public SpriteWrapper(ICollection<ISprite> spritestart, SpriteBatch sb, Updater updates) : this(sb, spritestart.ToArray<ISprite>())
        {
            if (updates == null)
            {
                throw new ArgumentNullException("updates");
            }
            Updater = updates;
        }

        /// <summary>
        /// Remove an ISprite from this SpriteWrapper.
        /// </summary>
        /// <param name="spr">The ISprite to remove.</param>
        public void Remove(ISprite spr)
        {
            _sprites.Remove(spr);
        }

        /// <summary>
        /// Get or set the ISprite with the specified index in the Sprites collection.
        /// </summary>
        /// <param name="index">The index in the Sprites collection.</param>
        /// <returns>The ISprite with the specified index in the Sprites collection.</returns>
        public ISprite this[int index]
        {
            get
            {
                return Sprites.ToArray()[index];
            }
            set
            {
                List<ISprite> sprites = Sprites.ToList();
                sprites[index] = value;
                _sprites = sprites;
            }
        }

        /// <summary>
        /// Update all ISprites managed by this SpriteWrapper, in addition to using the Updater associated with this SpriteWrapper if applicable.
        /// </summary>
        public void Update()
        {
            if (Updater != null)
            {
                for (i = 0; i < _sprites.Count; i++)
                {
                    this[i].Update();
                    this.Updater.UpdateSprite(this[i]);
                }
            }
            else
            {
                for (i = 0; i < _sprites.Count; i++)
                {
                    this[i].Update();
                }
            }
        }

        /// <summary>
        /// Update all ISprites managed by this SpriteWrapper, in addition to using the Updater associated with this SpriteWrapper if applicable.
        /// This overload will also call the Update(GameTime) overload of ITimerSprite objects.
        /// </summary>
        /// <param name="gt">The GameTime object to use in updating ITimerSprites.</param>
        public void Update(Microsoft.Xna.Framework.GameTime gt)
        {
            if (Updater != null)
            {
                for (i = 0; i < _sprites.Count; i++)
                {
                    if (this[i].GetType().GetInterfaces().Contains(typeof(ITimerSprite)))
                    {
                        ((ITimerSprite)this[i]).Update(gt);
                    }
                    else
                    {
                        this[i].Update();
                    }
                    this.Updater.UpdateSprite(this[i]);
                }
            }
            else
            {
                for (i = 0; i < _sprites.Count; i++)
                {
                    if (this[i].GetType().GetInterfaces().Contains(typeof(ITimerSprite)))
                    {
                        ((ITimerSprite)this[i]).Update(gt);
                    }
                    else
                    {
                        this[i].Update();
                    }
                }
            }
        }

        /// <summary>
        /// Draws all sprites, and handles the SpriteBatch opening and closing.
        /// This assumes the implementation of ISprite in use will not handle the SpriteBatch beginning/ending by itself.
        /// If the type also implements ISpriteBatchManagerSprite, the DrawNonAuto() method will be called on the object instead of Draw().
        /// </summary>
        public void Draw()
        {
            try
            {
                _sb.Begin();
            }
            catch { }
            DrawNonAuto();
                _sb.End();
        }

        /// <summary>
        /// Draw all sprites without handling the SpriteBatch for you.
        /// If the type in use implements ISpriteBatchManagerSprite, the DrawNonAuto() method will be called on the object instead of Draw().
        /// </summary>
        public void DrawNonAuto()
        {
            for(int i = 0; i < _sprites.Count; i++)
            {
                ISprite spr = _sprites[i];
                if (spr is ISpriteBatchManagerSprite)
                {
                    ((ISpriteBatchManagerSprite)spr).DrawNonAuto();
                }
                else
                {
                    spr.Draw();
                }
            }
        }
    }
}
