using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// Represents a collection of <see cref="Frame"/>s.
    /// </summary>
    public sealed class FrameCollection : Collection<Frame>
    {
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
