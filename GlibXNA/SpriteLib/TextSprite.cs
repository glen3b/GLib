using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Glib.XNA.InputLib;
using System.Diagnostics;
using Microsoft.Xna.Framework.GamerServices;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// An ISprite representing text.
    /// </summary>
    [DebuggerDisplay("Text = {Text}")]
    public class TextSprite : ISprite, IPositionable, ISizedScreenObject, ISizable
    {
        private float _layerDepth = 0;

        /// <summary>
        /// Gets or sets the layer depth at which to render the <see cref="TextSprite"/>.
        /// </summary>
        public float LayerDepth
        {
            get { return _layerDepth; }
            set
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _layerDepth = value;

            }
        }

        /// <summary>
        /// An event fired when the text of this TextSprite changes.
        /// </summary>
        public event EventHandler TextChanged;

#if WINDOWS
        /// <summary>
        /// An event fired when this <see cref="TextSprite"/> is dragged with the mouse.
        /// </summary>
        public event EventHandler<DragEventArgs> Dragged;

        private Vector2 mousePosDiff;

        /// <summary>
        /// Gets or sets a boolean indicating whether this <see cref="TextSprite"/> is draggable with the mouse.
        /// </summary>
        public bool IsDraggable
        {
            get;
            set;
        }
#endif

        private TextShadow _shadow;

        /// <summary>
        /// Gets or sets the shadow of this <see cref="TextSprite"/>.
        /// </summary>
        public TextShadow Shadow
        {
            get { return _shadow; }
            set { _shadow = value; }
        }


        /// <summary>
        /// The color that, if applicable, this TextSprite should be shadowed with.
        /// </summary>
        [Obsolete("Use Shadow instead.")]
        public Color? ShadowColor
        {
            get
            {
                return Shadow == null ? (Color?)null : (Color?)Shadow.ShadowColor;
            }
            set
            {
                if (!value.HasValue)
                {
                    Shadow = null;
                }

                if (Shadow == null)
                {
                    Shadow = new TextShadow(this, value.Value);
                }
                else
                {
                    Shadow.ShadowColor = value.Value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a boolean indicating whether or not this TextSprie is shadowed.
        /// </summary>
        [Obsolete("Use Shadow instead.")]
        public bool IsShadowed
        {
            get { return Shadow != null; }
            set
            {
                Shadow = value ? (Shadow == null ? new TextShadow(this) : Shadow) : null;
            }
        }

        /// <summary>
        /// Draw this text sprite to the SpriteBatch.
        /// Does not begin or end the SpriteBatch.
        /// </summary>
        public void Draw()
        {
            if (Visible)
            {
                if (Shadow != null)
                {
                    Shadow.ShadowedObject = this;
                    Shadow.Draw();
                }
                SpriteBatch.DrawString(Font, Text, Position, Color, Rotation.Radians, Vector2.Zero, Scale, SpriteEffects.None, _layerDepth);
            }
        }



        internal void FireClicked()
        {
            if (Pressed != null)
            {
                Pressed(this, EventArgs.Empty);
            }
        }

        private bool _visible = true;

        /// <summary>
        /// Gets or sets a boolean indicating whether or not this TextSprite is visible.
        /// </summary>
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }


#if WINDOWS
        /// <summary>
        /// An event fired after every click of this TextSprite.
        /// </summary>
        [Obsolete("Please use Pressed instead, it is a more general name that exists on Xbox.")]
        public event EventHandler Clicked
        {
            add
            {
                Pressed += value;
            }
            remove
            {
                Pressed -= value;
            }
        }
#endif

        /// <summary>
        /// An event fired after every click or keyboard selection of this TextSprite.
        /// </summary>
        public event EventHandler Pressed;

#if WINDOWS
        private MouseState _lastMouseState = new MouseState();
#endif

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

        private Sprite _parentSprite = null;

        /// <summary>
        /// Gets or sets the parent sprite (such as a button image) of this TextSprite.
        /// If not null, all selection, click, and positioning logic logic will be performed relative to this Sprite.
        /// This includes centering this TextSprite to the specified ParentSprite upon set.
        /// </summary>
        public Sprite ParentSprite
        {
            get { return _parentSprite; }
            set
            {
                if (value != _parentSprite)
                {
                    if (value == null)
                    {
                        Position = _parentSprite.Position;
                    }
                    else
                    {
                        value.Moved += _parentSprMoved;
                        Position = new Vector2((value.X - (value.Origin.X * value.Scale.X)) + (value.Width / 2 - Width / 2), (value.Y - (value.Origin.Y * value.Scale.Y)) + (value.Height / 2 - Height / 2));
                    }

                    if (_parentSprite != null)
                    {
                        _parentSprite.Moved -= _parentSprMoved;
                    }

                    _parentSprite = value;
                }
            }
        }


        /// <summary>
        /// Update the TextSprite.
        /// </summary>
        /// <remarks>
        /// Uses the InputLib.MouseManager.CurrentMouseState for mouse data.
        /// </remarks>
        public virtual void Update()
        {
#if WINDOWS
            MouseState currentMouseState = MouseManager.CurrentMouseState;

            Vector2 msPos = new Vector2(currentMouseState.X, currentMouseState.Y);
            Vector2 oldMsPos = new Vector2(_lastMouseState.X, _lastMouseState.Y);
#endif

            float actualX = X;
            float actualY = Y;
            float actualW = Width;
            float actualH = Height;
            if (_parentSprite != null)
            {
                actualX = _parentSprite.X - (_parentSprite.Origin.X * _parentSprite.Scale.X);
                actualY = _parentSprite.Y - (_parentSprite.Origin.Y * _parentSprite.Scale.Y);
                actualW = _parentSprite.Width;
                actualH = _parentSprite.Height;
            }

#if WINDOWS
            if ((Visible && IsSelected && ((msPos.X >= actualX && msPos.X <= actualX + actualW && msPos.Y >= actualY && msPos.Y <= actualY + actualH && oldMsPos.X >= actualX && oldMsPos.X <= actualX + actualW && oldMsPos.Y >= actualY && oldMsPos.Y <= actualY + actualH && currentMouseState.LeftButton == ButtonState.Released && _lastMouseState.LeftButton == ButtonState.Pressed))) && !XnaExtensions.IsGuideVisible)
            {
                FireClicked();
            }
#endif

            if (_isHoverable)
            {
                if (!HoverColor.HasValue || !NonHoverColor.HasValue)
                {
                    throw new InvalidOperationException("The hovering colors must be set to enable hovering.");
                }
                if (IsManuallySelectable)
                {
                    Color = (IsSelected ? HoverColor.Value : NonHoverColor.Value);
                }
                else
                {
#if WINDOWS
                    if ((_parentSprite != null ? _parentSprite.Visible && _parentSprite.Intersects(msPos) : (msPos.X >= X && msPos.X <= X + Width && msPos.Y >= Y && msPos.Y <= Y + Height) && Visible) && !XnaExtensions.IsGuideVisible)
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
#else
                    throw new InvalidOperationException("It is impossible to select a TextSprite with a mouse on Xbox.");
#endif
                }
            }

#if WINDOWS
            if (IsDraggable)
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed && _lastMouseState.LeftButton == ButtonState.Released && (_parentSprite != null ? _parentSprite.Visible && _parentSprite.Intersects(msPos) : (msPos.X >= X && msPos.X <= X + Width && msPos.Y >= Y && msPos.Y <= Y + Height) && Visible) && !XnaExtensions.IsGuideVisible)
                {
                    //Dragging mouse, need to calculate mouse position difference from our position.
                    mousePosDiff = MouseManager.MousePositionable.Position - new Vector2(actualX, actualY);

                }
                else if (currentMouseState.LeftButton == _lastMouseState.LeftButton && currentMouseState.LeftButton == ButtonState.Pressed && !XnaExtensions.IsGuideVisible)
                {
                    //Continuing drag.
                    if (!DragEventCanceled())
                    {
                        Position = MouseManager.MousePositionable.Position - mousePosDiff;
                    }
                }
            }
#endif

#if WINDOWS
            _lastMouseState = currentMouseState;
#endif
            if (Updated != null)
            {
                Updated(this, EventArgs.Empty);
            }
        }

#if WINDOWS
        /// <summary>
        /// Determines whether the dragged event is cancelled.
        /// </summary>
        /// <returns>Whether the Dragged event is cancelled.</returns>
        protected bool DragEventCanceled()
        {
            if (Dragged == null) return false;

            DragEventArgs eventArgs = new DragEventArgs(Position, MouseManager.MousePositionable.Position - mousePosDiff);
            Dragged(this, eventArgs);
            return eventArgs.Cancel;
        }
#endif

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
            set
            {
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
                else if (!value)
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

            _parentSprMoved = new EventHandler(
            delegate(object src, EventArgs e)
            {
                if (_parentSprite != null)
                {
                    Position = new Vector2((_parentSprite.X - (_parentSprite.Origin.X * _parentSprite.Scale.X)) + (_parentSprite.Width / 2 - Width / 2), (_parentSprite.Y - (_parentSprite.Origin.Y * _parentSprite.Scale.Y)) + (_parentSprite.Height / 2 - Height / 2));
                }
            }
            );

            KeyboardManager.KeyDown += new SingleKeyEventHandler(KeyboardManager_KeyDown);
        }

        void KeyboardManager_KeyDown(object source, SingleKeyEventArgs e)
        {
            if (_isHoverable && CallKeyboardClickEvent && Visible && _isSelected && e.Key == Keys.Enter && !XnaExtensions.IsGuideVisible)
            {
                FireClicked();
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

        private EventHandler _parentSprMoved;

        /// <summary>
        /// Construct a new TextSprite.
        /// </summary>
        public TextSprite(SpriteBatch sb, Vector2 pos, SpriteFont font, String text)
            : this(sb, font, text)
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

        private string _text = "";

        /// <summary>
        /// Gets or sets the text of this TextSprite.
        /// </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("The Text of this TextSprite must not be null.", null as Exception);
                }
                if (value != _text)
                {
                    _text = value;
                    if (TextChanged != null)
                    {
                        TextChanged(this, EventArgs.Empty);
                    }
                }
            }
        }


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
        public Vector2 Scale = Vector2.One;

        /// <summary>
        /// The rotation of the TextSprite.
        /// </summary>
        public SpriteRotation Rotation = SpriteRotation.Zero;
    }
}
