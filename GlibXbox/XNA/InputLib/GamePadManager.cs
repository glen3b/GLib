using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Glib.XNA.InputLib
{
    /// <summary>
    /// A class containing DPad related Xbox 360 controller events.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class DPadEvents
    {
        internal DPadEvents()
        {
        }

        /// <summary>
        /// An event fired when the left DPad key is pressed.
        /// </summary>
        public event EventHandler LeftArrowPressed;

        /// <summary>
        /// An event fired when the left DPad key is released.
        /// </summary>
        public event EventHandler LeftArrowReleased;

        /// <summary>
        /// An event fired when the right DPad key is pressed.
        /// </summary>
        public event EventHandler RightArrowPressed;

        /// <summary>
        /// An event fired when the right DPad key is released.
        /// </summary>
        public event EventHandler RightArrowReleased;

        /// <summary>
        /// An event fired when the up DPad key is pressed.
        /// </summary>
        public event EventHandler UpArrowPressed;

        /// <summary>
        /// An event fired when the up DPad key is released.
        /// </summary>
        public event EventHandler UpArrowReleased;

        /// <summary>
        /// An event fired when the down DPad key is pressed.
        /// </summary>
        public event EventHandler DownArrowPressed;

        /// <summary>
        /// An event fired when the down DPad key is released.
        /// </summary>
        public event EventHandler DownArrowReleased;

        internal void FireEvents(GamePadDPad currentButtonState, GamePadDPad lastButtonState)
        {
            if (LeftArrowPressed != null && currentButtonState.Left == ButtonState.Pressed && lastButtonState.Left == ButtonState.Released)
            {
                LeftArrowPressed(this, EventArgs.Empty);
            }
            else if (LeftArrowReleased != null && lastButtonState.Left == ButtonState.Pressed && currentButtonState.Left == ButtonState.Released)
            {
                LeftArrowReleased(this, EventArgs.Empty);
            }

            if (RightArrowPressed != null && currentButtonState.Right == ButtonState.Pressed && lastButtonState.Right == ButtonState.Released)
            {
                RightArrowPressed(this, EventArgs.Empty);
            }
            else if (RightArrowReleased != null && lastButtonState.Right == ButtonState.Pressed && currentButtonState.Right == ButtonState.Released)
            {
                RightArrowReleased(this, EventArgs.Empty);
            }

            if (UpArrowPressed != null && currentButtonState.Up == ButtonState.Pressed && lastButtonState.Up == ButtonState.Released)
            {
                UpArrowPressed(this, EventArgs.Empty);
            }
            else if (UpArrowReleased != null && lastButtonState.Up == ButtonState.Pressed && currentButtonState.Up == ButtonState.Released)
            {
                UpArrowReleased(this, EventArgs.Empty);
            }

            if (DownArrowPressed != null && currentButtonState.Down == ButtonState.Pressed && lastButtonState.Down == ButtonState.Released)
            {
                DownArrowPressed(this, EventArgs.Empty);
            }
            else if (DownArrowReleased != null && lastButtonState.Down == ButtonState.Pressed && currentButtonState.Down == ButtonState.Released)
            {
                DownArrowReleased(this, EventArgs.Empty);
            }

        }
    }

    /// <summary>
    /// A class containing button related Xbox 360 controller events.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class ControllerButtonEvents
    {
        internal ControllerButtonEvents()
        {

        }

        /// <summary>
        /// An event fired when the A button is pressed.
        /// </summary>
        public event EventHandler AButtonPressed;

        /// <summary>
        /// An event fired when the B button is pressed.
        /// </summary>
        public event EventHandler BButtonPressed;

        /// <summary>
        /// An event fired when the B button is released.
        /// </summary>
        public event EventHandler BButtonReleased;

        /// <summary>
        /// An event fired when the A button is released.
        /// </summary>
        public event EventHandler AButtonReleased;

        /// <summary>
        /// An event fired when the left shoulder is pressed.
        /// </summary>
        public event EventHandler LeftShoulderPressed;

        /// <summary>
        /// An event fired when the Y button is pressed.
        /// </summary>
        public event EventHandler YButtonPressed;

        /// <summary>
        /// An event fired when the Y button is released.
        /// </summary>
        public event EventHandler YButtonReleased;

        /// <summary>
        /// An event fired when the X button is pressed.
        /// </summary>
        public event EventHandler XButtonPressed;

        /// <summary>
        /// An event fired when the X button is released.
        /// </summary>
        public event EventHandler XButtonReleased;

        /// <summary>
        /// An event fired when the right shoulder is pressed.
        /// </summary>
        public event EventHandler RightShoulderPressed;

        /// <summary>
        /// An event fired when the left shoulder is released.
        /// </summary>
        public event EventHandler LeftShoulderReleased;

        /// <summary>
        /// An event fired when the right shoulder is released.
        /// </summary>
        public event EventHandler RightShoulderReleased;

        internal void FireEvents(GamePadButtons currentButtonState, GamePadButtons lastButtonState)
        {
            //TODO: Finish
            if (AButtonPressed != null && currentButtonState.A == ButtonState.Pressed && lastButtonState.A == ButtonState.Released)
            {
                AButtonPressed(this, EventArgs.Empty);
            }
            else if (AButtonReleased != null && currentButtonState.A == ButtonState.Released && lastButtonState.A == ButtonState.Pressed)
            {
                AButtonReleased(this, EventArgs.Empty);
            }

            if (BButtonPressed != null && currentButtonState.B == ButtonState.Pressed && lastButtonState.B == ButtonState.Released)
            {
                BButtonPressed(this, EventArgs.Empty);
            }
            else if (BButtonReleased != null && currentButtonState.B == ButtonState.Released && lastButtonState.B == ButtonState.Pressed)
            {
                BButtonReleased(this, EventArgs.Empty);
            }

            if (YButtonPressed != null && currentButtonState.Y == ButtonState.Pressed && lastButtonState.Y == ButtonState.Released)
            {
                YButtonPressed(this, EventArgs.Empty);
            }
            else if (YButtonReleased != null && currentButtonState.Y == ButtonState.Released && lastButtonState.Y == ButtonState.Pressed)
            {
                YButtonReleased(this, EventArgs.Empty);
            }

            if (XButtonPressed != null && currentButtonState.X == ButtonState.Pressed && lastButtonState.X == ButtonState.Released)
            {
                XButtonPressed(this, EventArgs.Empty);
            }
            else if (XButtonReleased != null && currentButtonState.X == ButtonState.Released && lastButtonState.X == ButtonState.Pressed)
            {
                XButtonReleased(this, EventArgs.Empty);
            }

            if (LeftShoulderPressed != null && currentButtonState.LeftShoulder == ButtonState.Pressed && lastButtonState.LeftShoulder == ButtonState.Released)
            {
                LeftShoulderPressed(this, EventArgs.Empty);
            }
            else if (LeftShoulderReleased != null && currentButtonState.LeftShoulder == ButtonState.Released && lastButtonState.LeftShoulder == ButtonState.Pressed)
            {
                LeftShoulderReleased(this, EventArgs.Empty);
            }

            if (RightShoulderPressed != null && currentButtonState.RightShoulder == ButtonState.Pressed && lastButtonState.RightShoulder == ButtonState.Released)
            {
                RightShoulderPressed(this, EventArgs.Empty);
            }
            else if (RightShoulderReleased != null && currentButtonState.RightShoulder == ButtonState.Released && lastButtonState.RightShoulder == ButtonState.Pressed)
            {
                RightShoulderReleased(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// A manager of GamePad related information. This class cannot be inherited.
    /// </summary>
    public sealed class GamePadManager
    {
        #region Static Methods, properties, and access of class

        private static GamePadManager _playerOne = new GamePadManager(PlayerIndex.One);

        /// <summary>
        /// Gets the GamePadManager representing player one's controller.
        /// </summary>
        public static GamePadManager One
        {
            get { return _playerOne; }
        }

        /// <summary>
        /// Gets the GamePadManager for the specified player's controller.
        /// </summary>
        /// <param name="player">The PlayerIndex of the player to retrieve a GamePadManager for.</param>
        /// <returns>The GamePadManager of the specified player's controller.</returns>
        public static GamePadManager GetManager(PlayerIndex player)
        {
            switch (player)
            {
                case PlayerIndex.One:
                    return One;
                case PlayerIndex.Two:
                    return Two;
                case PlayerIndex.Three:
                    return Three;
                case PlayerIndex.Four:
                    return Four;
                default:
                    throw new NotImplementedException("Support for the specified player is not implemented.");
            }
        }

        private static GamePadManager _playerTwo = new GamePadManager(PlayerIndex.Two);

        /// <summary>
        /// Gets the GamePadManager representing player two's controller.
        /// </summary>
        public static GamePadManager Two
        {
            get { return _playerTwo; }
        }

        private static GamePadManager _playerThree = new GamePadManager(PlayerIndex.Three);

        /// <summary>
        /// Gets the GamePadManager representing player three's controller.
        /// </summary>
        public static GamePadManager Three
        {
            get { return _playerThree; }
        }

        private static GamePadManager _playerFour = new GamePadManager(PlayerIndex.Four);

        /// <summary>
        /// Gets the GamePadManager representing player four's controller.
        /// </summary>
        public static GamePadManager Four
        {
            get { return _playerFour; }
        }
        

        #endregion

        private PlayerIndex _player;

        /// <summary>
        /// Gets the player whos control is associated with this GamePadManager.
        /// </summary>
        public PlayerIndex Player
        {
            get { return _player; }
        }

        private ControllerButtonEvents _buttons = new ControllerButtonEvents();

        /// <summary>
        /// Gets a class containing button-related Xbox 360 controller events.
        /// </summary>
        public ControllerButtonEvents Buttons
        {
            get { return _buttons; }
        }


        private DPadEvents _dpad = new DPadEvents();

        /// <summary>
        /// Gets a class containing DPad-related Xbox 360 controller events.
        /// </summary>
        public DPadEvents DPad
        {
            get { return _dpad; }
        }
        

        /// <summary>
        /// An event fired when the left joystick has moved.
        /// </summary>
        public event EventHandler LeftJoystickMoved;

        /// <summary>
        /// An event fired when the left trigger has moved.
        /// </summary>
        public event EventHandler LeftTriggerMoved;

        /// <summary>
        /// An event fired when the right trigger has moved.
        /// </summary>
        public event EventHandler RightTriggerMoved;

        /// <summary>
        /// An event fired when the right joystick has moved.
        /// </summary>
        public event EventHandler RightJoystickMoved;

        /// <summary>
        /// An event fired when the GamePad has been disconnected.
        /// </summary>
        public event EventHandler GamePadDisconnected;

        /// <summary>
        /// An event fired when the GamePad has been connected.
        /// </summary>
        public event EventHandler GamePadConnected;

        /// <summary>
        /// An event fired after the completion of the update of this GamePadManager, but before Last is assigned.
        /// </summary>
        public event EventHandler Updated;

        /// <summary>
        /// Update the GamePadManager.
        /// </summary>
        internal void Update()
        {
            _currentGamepadState = GamePad.GetState(_player);
            if (LeftJoystickMoved != null && _currentGamepadState.ThumbSticks.Left != _lastGamepadState.ThumbSticks.Left)
            {
                LeftJoystickMoved(this, EventArgs.Empty);
            }

            if (RightJoystickMoved != null && _currentGamepadState.ThumbSticks.Right != _lastGamepadState.ThumbSticks.Right)
            {
                RightJoystickMoved(this, EventArgs.Empty);
            }

            if (GamePadConnected != null && _currentGamepadState.IsConnected && !_lastGamepadState.IsConnected)
            {
                GamePadConnected(this, EventArgs.Empty);
            }
            else if (GamePadDisconnected != null && _lastGamepadState.IsConnected && !_currentGamepadState.IsConnected)
            {
                GamePadDisconnected(this, EventArgs.Empty);
            }

            if (LeftTriggerMoved != null && _currentGamepadState.Triggers.Left != _lastGamepadState.Triggers.Left)
            {
                LeftTriggerMoved(this, EventArgs.Empty);
            }

            if (RightTriggerMoved != null && _currentGamepadState.Triggers.Right != _lastGamepadState.Triggers.Right)
            {
                RightTriggerMoved(this, EventArgs.Empty);
            }

            _dpad.FireEvents(_currentGamepadState.DPad, _lastGamepadState.DPad);
            _buttons.FireEvents(_currentGamepadState.Buttons, _lastGamepadState.Buttons);

            if (Updated != null)
            {
                Updated(this, EventArgs.Empty);
            }

            _lastGamepadState = _currentGamepadState;
        }

        private GamePadState _currentGamepadState = new GamePadState();

        /// <summary>
        /// Gets the current GamePad state for this player.
        /// </summary>
        public GamePadState Current
        {
            get { return _currentGamepadState; }
        }

        private GamePadState _lastGamepadState = new GamePadState();

        /// <summary>
        /// Gets the last GamePad state for this player.
        /// </summary>
        public GamePadState Last
        {
            get { return _lastGamepadState; }
        }
        

        private GamePadManager(PlayerIndex player)
        {
            _player = player;
        }
    }
}
