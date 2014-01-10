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
        /// <param name="joystick">The identifier for the joystick. -1 is left, 1 is right.</param>
        /// <param name="prevPos">The previous joystick position.</param>
        /// <param name="currentPos">The current joystick position.</param>
        public JoystickMovedEventArgs(int joystick, Vector2 prevPos, Vector2 currentPos)
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

        private int _joystick;

        /// <summary>
        /// Gets the joystick that moved (-1 is left and 1 is right joystick).
        /// </summary>
        public int Joystick
        {
            get { return _joystick; }
        }

        /// <summary>
        /// Gets a value indicating if the joystick that moved was the right joystick.
        /// </summary>
        public bool IsRightJoystick
        {
            get
            {
                return Joystick == 1;
            }
        }

        /// <summary>
        /// Gets a value indicating if the joystick that moved was the left joystick.
        /// </summary>
        public bool IsLeftJoystick
        {
            get
            {
                return Joystick == -1;
            }
        }
    }
}
