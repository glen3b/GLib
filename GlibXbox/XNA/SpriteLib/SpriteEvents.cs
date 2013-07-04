using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Microsoft.Xna.Framework;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// Handle when a sprite is going to be moved. This is a cancellable event.
    /// </summary>
    /// <param name="source">The sprite that is undergoing an attempt to be moved.</param>
    /// <param name="e">The <seealso cref="SpriteMoveEventArgs">SpriteMoveEventArgs</seealso> for this event.</param>
    public delegate void SpriteMoveEventHandler(object source, SpriteMoveEventArgs e);
}
