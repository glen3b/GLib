using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Glib.XNA;

namespace Glib.XNA.InputLib
{
    /// <summary>
    /// Represents event arguments for the movement of a joystick on an Xbox controller.
    /// </summary>
    public class JoystickMovedEventArgs : EventArgs
    {
        /// <summary>
        /// Creates a new instance of <see cref="JoystickMovedEventArgs"/>.
        /// </summary>
        /// <param name="joystick">The identifier for the joystick.</param>
        /// <param name="prevPos">The previous joystick position.</param>
        /// <param name="currentPos">The current joystick position.</param>
        public JoystickMovedEventArgs(LeftRight joystick, Vector2 prevPos, Vector2 currentPos)
        {
            _joystick = joystick;
            PreviousPosition = prevPos;
            CurrentPosition = currentPos;
        }

        /// <summary>
        /// Gets the previous position of the joystick.
        /// </summary>
        public Vector2 PreviousPosition { get; private set; }

        /// <summary>
        /// Gets the current position of the joystick.
        /// </summary>
        public Vector2 CurrentPosition { get; private set; }

        /// <summary>
        /// Gets the difference between the current joystick position and the previous joystick position.
        /// </summary>
        public Vector2 Difference
        {
            get { return CurrentPosition - PreviousPosition; }
        }

        private LeftRight _joystick;

        /// <summary>
        /// Gets the joystick that moved (left or right joystick).
        /// </summary>
        public LeftRight Joystick
        {
            get { return _joystick; }
        }
        

    }
}
