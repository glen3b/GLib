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
    /// <param name="source">The SpriteManager that is intercepting the key event.</param>
    /// <param name="e">The <seealso cref="KeyEventArgs">KeyEventArgs</seealso> for this event.</param>
    public delegate void KeyEventHandler(object source, KeyEventArgs e);

    /// <summary>
    /// Handle when a keyboard event happens for a single key.
    /// </summary>
    /// <param name="source">The source of the event.</param>
    /// <param name="e">The <seealso cref="SingleKeyEventArgs">SingleKeyEventArgs</seealso> for this event.</param>
    public delegate void SingleKeyEventHandler(object source, SingleKeyEventArgs e);

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
