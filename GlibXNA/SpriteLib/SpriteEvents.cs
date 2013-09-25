using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// Handle when a sprite is going to be moved. This is a cancellable event.
    /// </summary>
    /// <param name="source">The sprite that is undergoing an attempt to be moved.</param>
    /// <param name="e">The <seealso cref="SpriteMoveEventArgs">SpriteMoveEventArgs</seealso> for this event.</param>
    public delegate void SpriteMoveEventHandler(object source, SpriteMoveEventArgs e);

    /// <summary>
    /// Handle when a sprite has been clicked on. Called immediately after Update() on a Sprite.
    /// </summary>
    /// <param name="source">The SpriteManager that is managing this sprite, or, if this event is being called by the sprite itself, the sprite being clicked on.</param>
    /// <param name="e">The <seealso cref="SpriteMoveEventArgs">SpriteMoveEventArgs</seealso> for this event.</param>
    public delegate void SpriteClickEventHandler(object source, SpriteClickEventArgs e);
}
