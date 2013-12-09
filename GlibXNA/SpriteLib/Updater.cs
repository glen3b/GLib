using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// A base class for generic update logic using a SpriteWrapper.
    /// Extend this class to implement sprite updating logic if you are using a SpriteWrapper.
    /// </summary>
    [Obsolete()]
    public abstract class Updater
    {
        internal SpriteWrapper _sw;

        /// <summary>
        /// Construct a new Updater associated with this SpriteWrapper.
        /// </summary>
        /// <param name="sw">The SpriteWrapper to associate this Updater with.</param>
        public Updater(SpriteWrapper sw)
        {
            _sw = sw;
        }

        /// <summary>
        /// Remove an ISprite from the parent SpriteWrapper.
        /// </summary>
        /// <param name="remove">The ISprite to remove.</param>
        protected void RemoveSprite(ISprite remove)
        {
            _sw.Remove(remove);
            _sw.i--;
        }

        /// <summary>
        /// Add an ISprite to the parent SpriteWrapper.
        /// </summary>
        /// <param name="sprite">The ISprite to add.</param>
        protected void AddSprite(ISprite sprite)
        {
            _sw.Add(sprite);
        }

        /// <summary>
        /// Get all ISprites in the parent SpriteWrapper.
        /// </summary>
        /// <returns>An enumerable of all the ISprites managed by the parent SpriteWrapper.</returns>
        protected IEnumerable<ISprite> GetAllSprites()
        {
            return _sw.Sprites;
        }

        /// <summary>
        /// Update this ISprite.
        /// </summary>
        /// <param name="updating">The ISprite to update.</param>
        public abstract void UpdateSprite(ISprite updating);
    }
}
