using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glib.XNA.SpriteLib;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;

namespace Glib.XNA.InputLib
{
    /// <summary>
    /// An enum representing an Xbox 360 controller input type.
    /// </summary>
    public enum InputType
    {
        /// <summary>
        /// The left thumbstick.
        /// </summary>
        LeftJoystick,
        /// <summary>
        /// The right joystick.
        /// </summary>
        RightJoystick,
        /// <summary>
        /// The DPad.
        /// </summary>
        DPad
    }

    /// <summary>
    /// A class representing a way to select and deselect buttons (TextSprites) using a GamePad.
    /// </summary>
    /// <remarks>
    /// This class depends on the GamePadManager. Ensure you have an InputManagerComponent in your Game before using this class.
    /// </remarks>
    public class GamePadButtonEnumerator
    {
        private TextSprite[,] _allButtons;

        /// <summary>
        /// Gets a two-dimensional array of all of the buttons to enumerate through.
        /// </summary>
        /// <remarks>
        /// This array is in [row, column] format.
        /// </remarks>
        public TextSprite[,] AllButtons
        {
            get { return _allButtons; }
        }

        /// <summary>
        /// An event fired when a button is pressed.
        /// </summary>
        /// <remarks>
        /// The source of this event will be marked as the button that was pressed.
        /// </remarks>
        public event EventHandler ButtonPress;

        /// <summary>
        /// The type of input to acknowledge for selecting various buttons.
        /// </summary>
        public InputType Input;

        private float _axisDifference;

        /// <summary>
        /// The difference along the X or Y axis required on a GamePad joystick to trigger movement (after delay).
        /// </summary>
        /// <remarks>
        /// This value is ignored if Input is DPad.
        /// </remarks>
        public float AxisDifference
        {
            get { return _axisDifference; }
            set {
                if (value <= 0 || value > 1)
                {
                    throw new ArgumentException("AxisDifference must be greater than zero and less than or equal to 1.");
                }
                _axisDifference = value;
            }
        }

        private TimeSpan _delay;

        /// <summary>
        /// Gets or sets the delay that the user must wait while holding an input button before the selection takes effect.
        /// </summary>
        public TimeSpan Delay
        {
            get { return _delay; }
            set {
                if (value < TimeSpan.Zero)
                {
                    throw new ArgumentException("Delay must be greater than zero.");
                }
                _delay = value;
            }
        }

        private Buttons _submitButton;

        /// <summary>
        /// Gets or sets the button to use for triggering a ButtonPress event.
        /// </summary>
        public Buttons SubmitButton
        {
            get { return _submitButton; }
            set {
                if (value == Buttons.BigButton)
                {
                    throw new ArgumentException("SubmitButton must not be the BigButton.");
                }
                else if (value == Buttons.DPadDown || value == Buttons.DPadLeft || value == Buttons.DPadRight || value == Buttons.DPadUp)
                {
                    throw new ArgumentException("SubmitButton must be a non-DPad button.");
                }

                _submitButton = value;
            }
        }
        

        /// <summary>
        /// If not null, the sound to play when the button selection changes.
        /// </summary>
        public SoundEffect SelectionSound = null;

        /// <summary>
        /// Creates a new GamePadButtonEnumerator with the specified buttons and input type, using an axis difference of 0.6, a delay of 250 milliseconds, player one's GamePad, and the A button.
        /// </summary>
        /// <param name="allButtons">All of the buttons to include in the GamePadButtonEnumerator.</param>
        /// <param name="inputType">The type of input to accept for GamePad-based button enumeration.</param>
        public GamePadButtonEnumerator(TextSprite[,] allButtons, InputType inputType) : this(allButtons, inputType, 0.6f, TimeSpan.FromMilliseconds(250), Buttons.A, PlayerIndex.One)
        {
            
        }

        /// <summary>
        /// Creates a new GamePadButtonEnumerator with the specified buttons and parameters.
        /// </summary>
        /// <param name="allButtons">All of the buttons to include in the GamePadButtonEnumerator.</param>
        /// <param name="inputType">The type of input to accept for GamePad-based button enumeration.</param>
        /// <param name="axisDifference">The difference along the X or Y axis required on a GamePad joystick to trigger movement (after delay). This parameter does nothing if inputType is DPad.</param>
        /// <param name="delay">The time required to pass between button selection changes. This parameter does nothing if inputType is DPad.</param>
        /// <param name="submitButton">The button on the GamePad controller which must be pushed to trigger a ButtonPress event.</param>
        /// <param name="player">The PlayerIndex to accept GamePad input from.</param>
        public GamePadButtonEnumerator(TextSprite[,] allButtons, InputType inputType, float axisDifference, TimeSpan delay, Buttons submitButton, PlayerIndex player)
        {
            if (allButtons == null)
            {
                throw new ArgumentNullException("allButtons");
            }
            else if (allButtons.GetLength(0) < 1 || allButtons.GetLength(1) < 1)
            {
                throw new ArgumentException("allButtons must have a length in both dimensions.");
            }
            if (axisDifference <= 0 || axisDifference > 1)
            {
                throw new ArgumentException("axisDifference must be greater than zero and less than or equal to 1.");
            }
            if (delay < TimeSpan.Zero)
            {
                throw new ArgumentException("delay must be greater than zero.");
            }
            if (submitButton == Buttons.BigButton)
            {
                throw new ArgumentException("submitButton must not be the BigButton.");
            }
            else if (submitButton == Buttons.DPadDown || submitButton == Buttons.DPadLeft || submitButton == Buttons.DPadRight || submitButton == Buttons.DPadUp)
            {
                throw new ArgumentException("submitButton must be a non-DPad button.");
            }
            foreach (TextSprite ts in allButtons)
            {
                ts.IsManuallySelectable = true;
                ts.IsSelected = false;
            }
            for (int i = 0; i < allButtons.GetLength(1); i++)
            {
                if (allButtons[0, i] != null)
                {
                    allButtons[0, i].IsSelected = true;
                    _rowCurrent = 0;
                    _columnCurrent = i;
                    break;
                }
                if (i == allButtons.GetLength(1) - 1)
                {
                    throw new ArgumentException("The allButtons array must contain at least one non-null button in each row and column.");
                }
            }
            _delay = delay;
            _allButtons = allButtons;
            _axisDifference = axisDifference;
            _submitButton = submitButton;
            this.Player = player;
            Input = inputType;
        }

        /// <summary>
        /// Gets the player to receive GamePad input from.
        /// </summary>
        public PlayerIndex Player { get; private set; }

        private TimeSpan _elapsedLeftTime = TimeSpan.Zero;
        private TimeSpan _elapsedRightTime = TimeSpan.Zero;
        private TimeSpan _elapsedUpTime = TimeSpan.Zero;
        private TimeSpan _elapsedDownTime = TimeSpan.Zero;
        private int _rowCurrent = 0;
        private int _columnCurrent = 0;
        private GamePadState _lastState = new GamePadState();

        /// <summary>
        /// Move the current selection by 1 button, resetting to the top or side if neccesary.
        /// </summary>
        protected void MoveSelection(Direction dir)
        {
            _allButtons[_rowCurrent, _columnCurrent].IsSelected = false;
            switch (dir)
            {
                case Direction.Top:
                    do
                    {
                        _rowCurrent--;
                        if (_rowCurrent < 0)
                        {
                            _rowCurrent = _allButtons.GetLength(0) - 1;
                        }
                    } while (_allButtons[_rowCurrent, _columnCurrent] == null);
                    break;
                case Direction.Bottom:
                    do
                    {
                        _rowCurrent++;
                        if (_rowCurrent >= _allButtons.GetLength(0))
                        {
                            _rowCurrent = 0;
                        }
                    } while (_allButtons[_rowCurrent, _columnCurrent] == null);
                    break;
                case Direction.Left:
                    do{
                        _columnCurrent--;
                        if (_columnCurrent < 0)
                        {
                            _columnCurrent = _allButtons.GetLength(1) - 1;
                        }
                    } while (_allButtons[_rowCurrent, _columnCurrent] == null);
                    break;
                case Direction.Right:
                    do
                    {
                        _columnCurrent++;
                        if (_columnCurrent >= _allButtons.GetLength(1))
                        {
                            _columnCurrent = 0;
                        }
                    } while (_allButtons[_rowCurrent, _columnCurrent] == null);
                    break;
            }
            _allButtons[_rowCurrent, _columnCurrent].IsSelected = true;
            if (SelectionSound != null)
            {
                SelectionSound.Play();
            }
        }

        /// <summary>
        /// Update this GamePadButtonEnumerator.
        /// </summary>
        /// <param name="gt">The current GameTime.</param>
        public virtual void Update(GameTime gt)
        {
            GamePadState current = GamePadManager.GetManager(Player).Current;
            bool isJoystick = Input == InputType.LeftJoystick || Input == InputType.RightJoystick;
            if (isJoystick)
            {
                Vector2 joystick = Input == InputType.LeftJoystick ? current.ThumbSticks.Left : (Input == InputType.RightJoystick ? current.ThumbSticks.Right : Vector2.Zero);
                if (joystick.Y >= _axisDifference)
                {
                    _elapsedUpTime += gt.ElapsedGameTime;
                }
                else if (joystick.Y <= -_axisDifference)
                {
                    _elapsedDownTime += gt.ElapsedGameTime;
                }
                if (joystick.X >= _axisDifference)
                {
                    _elapsedRightTime += gt.ElapsedGameTime;
                }
                else if (joystick.X <= -_axisDifference)
                {
                    _elapsedLeftTime += gt.ElapsedGameTime;
                }

                if (_elapsedUpTime >= _delay)
                {
                    _elapsedUpTime = TimeSpan.Zero;
                    MoveSelection(Direction.Top);
                }
                if (_elapsedDownTime >= _delay)
                {
                    _elapsedDownTime = TimeSpan.Zero;
                    MoveSelection(Direction.Bottom);
                }
                if (_elapsedLeftTime >= _delay)
                {
                    _elapsedLeftTime = TimeSpan.Zero;
                    MoveSelection(Direction.Left);
                }
                if (_elapsedRightTime >= _delay)
                {
                    _elapsedRightTime = TimeSpan.Zero;
                    MoveSelection(Direction.Right);
                }
            }
            else
            {
                //We know for sure it is DPad.
                if (_lastState.DPad.Left == ButtonState.Released && current.DPad.Left == ButtonState.Pressed)
                {
                    MoveSelection(Direction.Left);
                }
                if (_lastState.DPad.Right == ButtonState.Released && current.DPad.Right == ButtonState.Pressed)
                {
                    MoveSelection(Direction.Right);
                }
                if (_lastState.DPad.Up == ButtonState.Released && current.DPad.Up == ButtonState.Pressed)
                {
                    MoveSelection(Direction.Top);
                }
                if (_lastState.DPad.Down == ButtonState.Released && current.DPad.Down == ButtonState.Pressed)
                {
                    MoveSelection(Direction.Bottom);
                }
            }
            if (ButtonPress != null && _lastState.IsButtonUp(_submitButton) && current.IsButtonDown(_submitButton))
            {
                ButtonPress(_allButtons[_rowCurrent, _columnCurrent], EventArgs.Empty);
            }

            _lastState = current;
        }
    }
}
