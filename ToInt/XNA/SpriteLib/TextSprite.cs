using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Glib.XNA.InputLib;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// An ISprite representing text.
    /// </summary>
    public class TextSprite : ISprite, IPositionable, ISizedScreenObject, ISizable
    {
        /// <summary>
        /// An event fired after every click of this TextSprite.
        /// </summary>
        public event EventHandler Clicked;

        private MouseState _lastMouseState = new MouseState();

        private bool _isSelected;

        /// <summary>
        /// Gets or sets a boolean indicating whether or not this is a selected TextSprite.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; }
        }

        private bool _isManualSelectable;

        /// <summary>
        /// Gets or sets a boolean indicating whether or not this TextSprite will be selected by ways other than the mouse cursor.
        /// If this is true, the mouse cursor will be ignored.
        /// </summary>
        public bool IsManuallySelectable
        {
            get { return _isManualSelectable; }
            set { _isManualSelectable = value; }
        }
        

        /// <summary>
        /// Update the TextSprite. Just calls the Updated event by default.
        /// </summary>
        /// <remarks>
        /// Uses the InputLib.Mouse.MouseManager.CurrentMouseState for mouse data.
        /// </remarks>
        public virtual void Update()
        {
            MouseState currentMouseState = MouseManager.CurrentMouseState;
            
            Vector2 msPos = new Vector2(currentMouseState.X, currentMouseState.Y);
            Vector2 oldMsPos = new Vector2(_lastMouseState.X, _lastMouseState.Y);
            if (Clicked != null && IsSelected && ( ( msPos.X >= X && msPos.X <= X + Width && msPos.Y >= Y && msPos.Y <= Y + Height && oldMsPos.X >= X && oldMsPos.X <= X + Width && oldMsPos.Y >= Y && oldMsPos.Y <= Y + Height && currentMouseState.LeftButton == ButtonState.Released && _lastMouseState.LeftButton == ButtonState.Pressed )))
            {
                Clicked(this, new EventArgs());
            }
            if (_isHoverable)
            {
                if (!HoverColor.HasValue || !NonHoverColor.HasValue)
                {
                    throw new InvalidOperationException("The hovering colors must be set to enable hovering.");
                }
                if (IsManuallySelectable)
                {
                    Color = IsSelected ? HoverColor.Value : NonHoverColor.Value;
                }
                else
                {
                    if (msPos.X >= X && msPos.X <= X + Width && msPos.Y >= Y && msPos.Y <= Y + Height)
                    {
                        //Intersecting.
                        IsSelected = true;
                        Color = HoverColor.Value;
                    }
                    else
                    {
                        //Not intersecting.
                        IsSelected = false;
                        Color = NonHoverColor.Value;
                    }
                }
            }
            _lastMouseState = currentMouseState;
            if (Updated != null)
            {
                Updated(this, new EventArgs());
            }
        }

        /// <summary>
        /// Whether or not to call Clicked events for keypresses of Enter.
        /// </summary>
        /// <remarks>
        /// For this to take effect, KeyboardManager must be updated.
        /// </remarks>
        public bool CallKeyboardClickEvent = true;

        /// <summary>
        /// The color to use when hovering over the TextSprite, if hovering is enabled.
        /// </summary>
        public Color? HoverColor;

        private bool _isHoverable;

        /// <summary>
        /// Gets or sets a boolean indicating whether or not to apply hover effects to this TextSprite.
        /// </summary>
        public bool IsHoverable
        {
            get { return _isHoverable; }
            set {
                _isHoverable = value;
                if (value)
                {
                    if (!HoverColor.HasValue)
                    {
                        HoverColor = Color.White;
                    }
                    if (!NonHoverColor.HasValue)
                    {
                        NonHoverColor = Color.Black;
                    }
                }
                else if(!value)
                {
                    HoverColor = null;
                    NonHoverColor = null;
                }
            }
        }
        

        /// <summary>
        /// The color to use when not hovering over the TextSprite, if hovering is enabled.
        /// </summary>
        public Color? NonHoverColor;

        /// <summary>
        /// An event called after every update of this TextSprite.
        /// </summary>
        public event EventHandler Updated = null;

        /// <summary>
        /// Construct a new TextSprite.
        /// </summary>
        public TextSprite(SpriteBatch sb, SpriteFont font, String text)
        {
            SpriteBatch = sb;
            Font = font;
            Text = text;
            KeyboardManager.KeyDown += new SingleKeyEventHandler(KeyboardManager_KeyDown);
        }

        void KeyboardManager_KeyDown(object source, SingleKeyEventArgs e)
        {
            if (_isHoverable && CallKeyboardClickEvent && _isSelected && e.Key == Keys.Enter && Clicked != null)
            {
                Clicked(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets the width of the TextSprite.
        /// </summary>
        public float Width
        {
            get
            {
                return Font.MeasureString(Text).X;
            }
        }

        /// <summary>
        /// Gets the height of the TextSprite.
        /// </summary>
        public float Height
        {
            get
            {
                return Font.MeasureString(Text).Y;
            }
        }

        /// <summary>
        /// Construct a new TextSprite.
        /// </summary>
        public TextSprite(SpriteBatch sb, Vector2 pos, SpriteFont font, String text) : this(sb, font, text)
        {
            Position = pos;
        }

        /// <summary>
        /// Construct a new TextSprite.
        /// </summary>
        public TextSprite(SpriteBatch sb, SpriteFont font, String text, Color color)
            : this(sb, font, text)
        {
            Color = color;
            NonHoverColor = color;
        }

        /// <summary>
        /// Construct a new TextSprite.
        /// </summary>
        public TextSprite(SpriteBatch sb, Vector2 pos, SpriteFont font, String text, Color color)
            : this(sb, pos, font, text)
        {
            Color = color;
            NonHoverColor = color;
        }

        /// <summary>
        /// The current X coordinate of the sprite.
        /// </summary>
        public float X
        {
            get
            {
                return Position.X;
            }
            set
            {
                _position.X = value;
            }
        }

        /// <summary>
        /// The current Y coordinate of the sprite.
        /// </summary>
        public float Y
        {
            get
            {
                return Position.Y;
            }
            set
            {
                _position.Y = value;
            }
        }

        /// <summary>
        /// The SpriteFont to use.
        /// </summary>
        public SpriteFont Font;

        /// <summary>
        /// The text of this text sprite.
        /// </summary>
        public String Text = "";

        /// <summary>
        /// The color to draw the text as.
        /// </summary>
        public Color Color = Color.Black;

        private Vector2 _position = Vector2.Zero;

        /// <summary>
        /// The position to draw the text.
        /// </summary>
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        

        /// <summary>
        /// The SpriteBatch to draw this text sprite to.
        /// </summary>
        public SpriteBatch SpriteBatch;

        /// <summary>
        /// The scale of this TextSprite.
        /// </summary>
        public Vector2 Scale = new Vector2(1);

        /// <summary>
        /// The rotation of the TextSprite.
        /// </summary>
        public SpriteRotation Rotation = new SpriteRotation();


        /// <summary>
        /// Draw this text sprite to the SpriteBatch.
        /// Does not begin or end the SpriteBatch.
        /// </summary>
        public void Draw()
        {
            SpriteBatch.DrawString(Font, Text, Position, Color, Rotation.Radians, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }
    }
}
