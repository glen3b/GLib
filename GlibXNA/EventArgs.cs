using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Glib.XNA
{
    /// <summary>
    /// Handle when a keyboard event happens.
    /// </summary>
    /// <param name="sender">The SpriteManager that is intercepting the key event.</param>
    /// <param name="e">The <seealso cref="KeyEventArgs">KeyEventArgs</seealso> for this event.</param>
    public delegate void KeyEventHandler(object sender, KeyEventArgs e);

    /// <summary>
    /// Handle when a keyboard event happens for a single key.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <seealso cref="SingleKeyEventArgs">SingleKeyEventArgs</seealso> for this event.</param>
    public delegate void SingleKeyEventHandler(object sender, SingleKeyEventArgs e);

    /// <summary>
    /// Event arguments for a possible mouse drag.
    /// </summary>
    public class DragEventArgs : System.ComponentModel.CancelEventArgs
    {
        /// <summary>
        /// Gets the new position of the object.
        /// </summary>
        public Vector2 NewPosition { get; protected internal set; }

        /// <summary>
        /// Gets the old position of the object.
        /// </summary>
        public Vector2 OldPosition { get; protected internal set; }

        /// <summary>
        /// Create a new <see cref="DragEventArgs"/>, specifying an old position and a new position.
        /// </summary>
        /// <param name="newPos">The new position of the dragged object.</param>
        /// <param name="oldPos">The old position of the dragged object.</param>
        public DragEventArgs(Vector2 oldPos, Vector2 newPos)
        {
            OldPosition = oldPos;
            NewPosition = newPos;
        }
    }

    /// <summary>
    /// Event arguments for a sprite movement.
    /// </summary>
    public class SpriteMoveEventArgs : System.ComponentModel.CancelEventArgs
    {
        /// <summary>
        /// The old position of the sprite
        /// </summary>
        public readonly Vector2 OldPosition;

        /// <summary>
        /// The position the sprite is going to be moved to.
        /// </summary>
        public readonly Vector2 NewPosition;

        /// <summary>
        /// Create a new SpriteMoveEventArgs, specifying an old position and a new position.
        /// </summary>
        /// <param name="OldPos">The old position.</param>
        /// <param name="NewPos">The new position.</param>
        public SpriteMoveEventArgs(Vector2 OldPos, Vector2 NewPos)
        {
            this.OldPosition = OldPos;
            this.NewPosition = NewPos;
        }
    }

    /// <summary>
    /// Event arguments for a sprite click.
    /// </summary>
    public class SpriteClickEventArgs : EventArgs
    {
        /// <summary>
        /// The X coordinate of the click on the screen.
        /// </summary>
        public readonly int X;

        /// <summary>
        /// Whether or not this event is being called by a SpriteManager.
        /// </summary>
        public readonly bool CalledByManager;

        /// <summary>
        /// The Y coordinate of the click on the screen.
        /// </summary>
        public readonly int Y;

        /// <summary>
        /// The sprite that was clicked on.
        /// </summary>
        public readonly Glib.XNA.SpriteLib.Sprite Sprite;

        /// <summary>
        /// Create a new SpriteClickEventArgs, specifying a position and sprite.
        /// </summary>
        /// <param name="x">The X position of the click.</param>
        /// <param name="y">The Y position of the click.</param>
        /// <param name="spr">The sprite that was clicked on.</param>
        /// <param name="manager">Whether or not this event is being called by a SpriteManager.</param>
        public SpriteClickEventArgs(Glib.XNA.SpriteLib.Sprite spr, int x, int y, bool manager)
        {
            this.Sprite = spr;
            this.X = x;
            this.CalledByManager = manager;
            this.Y = y;
        }
    }

    /// <summary>
    /// Event arguments for a key event.
    /// </summary>
    public class KeyEventArgs : EventArgs
    {
        /// <summary>
        /// All the pressed keys in this event.
        /// </summary>
        public Keys[] PressedKeys = new Keys[0];

        /// <summary>
        /// Whether or not the given key is pressed in this event.
        /// </summary>
        /// <param name="k">The key to check if it is being pressed.</param>
        /// <returns>A boolean representing whether or not the given key is pressed.</returns>
        public bool KeyIsPressed(Keys k)
        {
            return PressedKeys.Contains(k);
        }

        /// <summary>
        /// A boolean representing whether or not the Alt modifier key is pressed.
        /// </summary>
        public bool Alt
        {
            get
            {
                return PressedKeys.Contains(Keys.LeftAlt) || PressedKeys.Contains(Keys.RightAlt);
            }
        }

        /// <summary>
        /// A boolean representing whether or not the Control modifier key is pressed.
        /// </summary>
        public bool Control
        {
            get
            {
                return PressedKeys.Contains(Keys.LeftControl) || PressedKeys.Contains(Keys.RightControl);
            }
        }

        /// <summary>
        /// A boolean representing whether or not the Shift modifier key is pressed.
        /// </summary>
        public bool Shift
        {
            get
            {
                return PressedKeys.Contains(Keys.LeftShift) || PressedKeys.Contains(Keys.RightShift);
            }
        }

        /// <summary>
        /// Construct a new KeyEventArgs.
        /// </summary>
        /// <param name="pressedKeys">The keys pressed for this event.</param>
        public KeyEventArgs(ICollection<Keys> pressedKeys)
        {
            PressedKeys = pressedKeys.ToArray();
        }
    }

    /// <summary>
    /// Event arguments for a single key event.
    /// </summary>
    public class SingleKeyEventArgs : EventArgs
    {
        private Keys _key;

        /// <summary>
        /// Gets the key associated with this event.
        /// </summary>
        public Keys Key {
        get{
            return _key;
        }
        }

        /// <summary>
        /// Construct a new SingleKeyEventArgs.
        /// </summary>
        /// <param name="pressedKey">The key associated with this event.</param>
        public SingleKeyEventArgs(Keys pressedKey)
        {
            _key = pressedKey;
        }
    }
}
