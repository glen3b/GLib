using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// A simple class for storing a mapping of ISprites to positions on the screen.
    /// It is strongly suggested that the implementation of ISprite has a Location/Position property instead of using this class.
    /// </summary>
    [Obsolete("Use an ISprite which implements IPositionable instead of this class.")]
    public class SpritePositionMapping : List<KeyValuePair<ISprite, Vector2>>
    {
        /// <summary>
        /// Finds the position of a certain sprite.
        /// </summary>
        /// <param name="spr">The sprite to find the position of.</param>
        /// <returns>The position of the sprite, or a blank Vector2 if not found</returns>
        [Obsolete("Please use the ISprite indexer instead of this method.")]
        public Vector2 FindPosition(ISprite spr)
        {
            return this[findIndex(spr)].Value;
        }

        /// <summary>
        /// Remove the position of the specified ISprite from this SpritePositionMapping.
        /// </summary>
        /// <param name="spr">The sprite to remove.</param>
        public void Remove(ISprite spr)
        {
            RemoveAt(findIndex(spr));
        }

        private int findIndex(ISprite spr)
        {
            for (int i = 0; i < Length; i++)
            {
                KeyValuePair<ISprite, Vector2> kvp = this[i];
                if (kvp.Key == spr)
                {
                    return i;
                }
            }
            throw new IndexOutOfRangeException("ISprite not found.");
        }

        /// <summary>
        /// Get and set the position of the specified ISprite.
        /// </summary>
        /// <param name="sprite">The ISprite you want to change the position of.</param>
        /// <returns>The position of the given ISprite.</returns>
        public Vector2 this[ISprite sprite]
        {
            get { return this[findIndex(sprite)].Value; }
            set
            {
                this[findIndex(sprite)] = new KeyValuePair<ISprite, Vector2>(sprite, value);
            }
        }

        /// <summary>
        /// The number of items in this SpritePositionMapping.
        /// </summary>
        /// <remarks>
        /// The same thing as Count.
        /// </remarks>
        public int Length
        {
            get
            {
                return Count;
            }
        }

        /// <summary>
        /// Move the given sprite to a new position.
        /// </summary>
        /// <param name="spr">The sprite to move.</param>
        /// <param name="newpos">The new position of the sprite.</param>
        [Obsolete("Please use the ISprite indexer instead of this method.")]
        public void MoveSprite(ISprite spr, Vector2 newpos)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Key == spr)
                {
                    this[i] = new KeyValuePair<ISprite,Vector2>(spr, newpos);
                }
            }
        }
    }
}
