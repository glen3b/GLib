using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// Represents a collection of <see cref="Frame"/>s.
    /// </summary>
    public sealed class FrameCollection : Collection<Frame>
    {
        /// <summary>
        /// Generates a collection of frames from a sprite sheet.
        /// </summary>
        /// <param name="sprites">The bounding boxes of the sprites within the sprite sheet.</param>
        /// <param name="spritesheet">The sprite sheet to load from.</param>
        /// <returns>A frame collection.</returns>
        public static FrameCollection FromSpriteSheet(Texture2D spritesheet, params Rectangle[] sprites)
        {
            if (spritesheet == null)
            {
                throw new ArgumentNullException("spritesheet");
            }

            if (sprites == null)
            {
                throw new ArgumentNullException("sprites");
            }

            FrameCollection collection = new FrameCollection();

            for (int i = 0; i < sprites.Length; i++)
            {
                collection.Add(new Frame(spritesheet, sprites[i]));
            }

            return collection;
        }


        /// <summary>
        /// Generates a collection of frames from a sprite sheet with identical sprite sizes.
        /// </summary>
        /// <param name="size">The size of a single sprite.</param>
        /// <param name="spritesheet">The sprite sheet to load from.</param>
        /// <returns>A frame collection.</returns>
        public static FrameCollection FromSpriteSheet(Texture2D spritesheet, Point size)
        {
            if (spritesheet == null)
            {
                throw new ArgumentNullException("spritesheet");
            }

            if (size.X <= 0 || size.Y <= 0)
            {
                throw new ArgumentException("The size of a sprite must be positive.");
            }

            FrameCollection collection = new FrameCollection();

            for (int y = 0; y + size.Y <= spritesheet.Height; y += size.Y)
            {
                for (int x = 0; x + size.X <= spritesheet.Width; x += size.X)
                {
                    collection.Add(new Frame(spritesheet, new Rectangle(x, y, size.X, size.Y)));
                }
            }


            return collection;
        }

        /// <summary>
        /// Replaces the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to replace.</param>
        /// <param name="item">The new value for the element at the specified index.</param>
        protected override void SetItem(int index, Frame item)
        {
            if (item == null)
            {
                throw new InvalidOperationException("A null frame cannot be added to a FrameCollection.");
            }

            base.SetItem(index, item);
        }

        /// <summary>
        /// Inserts an element into the <see cref="FrameCollection"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which the item should be inserted.</param>
        /// <param name="item">The object to insert.</param>
        protected override void InsertItem(int index, Frame item)
        {
            if (item == null)
            {
                throw new InvalidOperationException("A null frame cannot be added to a FrameCollection.");
            }

            base.InsertItem(index, item);
        }
    }
}
