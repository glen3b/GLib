using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// A class calling keyboard related events when updated.
    /// </summary>
    public class KeyboardManager
    {
        /// <summary>
        /// Create a new KeyboardManager.
        /// </summary>
        public KeyboardManager()
        {

        }


        private List<Keys> _knownDownKeys = new List<Keys>();

        /// <summary>
        /// An event called when a new key is pressed down.
        /// </summary>
        public event SingleKeyEventHandler KeyDown;

        /// <summary>
        /// An event called when a key is lifted.
        /// </summary>
        public event SingleKeyEventHandler KeyUp;

        /// <summary>
        /// An event called when a key is pressed and released.
        /// </summary>
        public event SingleKeyEventHandler KeyPressed;

        /// <summary>
        /// The last known state of the keyboard.
        /// </summary>
        protected KeyboardState _lastState = new KeyboardState();

        /// <summary>
        /// Update the KeyboardManager, calling the appropriate events.
        /// </summary>
        public virtual void Update()
        {
            KeyboardState current = Keyboard.GetState();

            //Key down
            if (KeyDown != null)
            {
                foreach (Keys k in current.GetPressedKeys())
                {
                    if (_lastState.IsKeyUp(k))
                    {
                        KeyDown(this, new SingleKeyEventArgs(k));
                    }
                }
            }

            //Key up
            if (KeyUp != null)
            {
                foreach (Keys k in _lastState.GetPressedKeys())
                {
                    if (current.IsKeyUp(k))
                    {
                        KeyUp(this, new SingleKeyEventArgs(k));
                    }
                }
            }

            //Key press
            if (KeyPressed != null)
            {
                foreach (Keys s in current.GetPressedKeys())
                {
                    if (!_knownDownKeys.Contains(s))
                    {
                        _knownDownKeys.Add(s);
                    }
                }

                List<Keys> rm = new List<Keys>();
                foreach (Keys k in _knownDownKeys)
                {
                    if (current.IsKeyUp(k))
                    {
                        KeyPressed(this, new SingleKeyEventArgs(k));
                        rm.Add(k);
                    }
                }
                foreach (Keys rk in rm)
                {
                    _knownDownKeys.Remove(rk);
                }
            }

            _lastState = current;
        }
    }
}
