using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Glib.XNA.InputLib
{
    /// <summary>
    /// A static class calling keyboard related events when updated.
    /// </summary>
    public static class KeyboardManager
    {
        private static List<Keys> _knownDownKeys = new List<Keys>();

        /// <summary>
        /// Gets the last known state of the keyboard.
        /// </summary>
        /// <remarks>
        /// Current when accessed after Update().
        /// </remarks>
        public static KeyboardState State
        {
            get
            {
                return _lastState;
            }
        }

        /// <summary>
        /// An event called when a new key is pressed down.
        /// </summary>
        public static event SingleKeyEventHandler KeyDown;

        /// <summary>
        /// An event called when a key is lifted.
        /// </summary>
        public static event SingleKeyEventHandler KeyUp;

        /// <summary>
        /// An event called when a key is pressed and released.
        /// </summary>
        public static event SingleKeyEventHandler KeyPressed;

        /// <summary>
        /// The last known state of the keyboard.
        /// </summary>
        private static KeyboardState _lastState = new KeyboardState();

        private static KeyboardState _currentState;

        /// <summary>
        /// Update the KeyboardManager, calling the appropriate events.
        /// </summary>
        internal static void Update()
        {
            _currentState = Keyboard.GetState();

            //Key down
            if (KeyDown != null)
            {
                foreach (Keys k in _currentState.GetPressedKeys())
                {
                    if (_lastState.IsKeyUp(k))
                    {
                        KeyDown(null, new SingleKeyEventArgs(k));
                    }
                }
            }

            //Key up
            if (KeyUp != null)
            {
                foreach (Keys k in _lastState.GetPressedKeys())
                {
                    if (_currentState.IsKeyUp(k))
                    {
                        KeyUp(null, new SingleKeyEventArgs(k));
                    }
                }
            }

            //Key press
            if (KeyPressed != null)
            {
                foreach (Keys s in _currentState.GetPressedKeys())
                {
                    if (!_knownDownKeys.Contains(s))
                    {
                        _knownDownKeys.Add(s);
                    }
                }

                List<Keys> rm = new List<Keys>();
                foreach (Keys k in _knownDownKeys)
                {
                    if (_currentState.IsKeyUp(k))
                    {
                        KeyPressed(null, new SingleKeyEventArgs(k));
                        rm.Add(k);
                    }
                }
                foreach (Keys rk in rm)
                {
                    _knownDownKeys.Remove(rk);
                }
            }

            _lastState = _currentState;
        }
    }
}
