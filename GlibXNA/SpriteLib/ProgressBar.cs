using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// Represents a progress bar.
    /// </summary>
    [DebuggerDisplay("Value = {Value}")]
    public class ProgressBar : Sprite
    {
        private int _denominator = 10;

        /// <summary>
        /// The object for locking when modifying variables relating to the value of the <see cref="ProgressBar"/>. The provided properties use this object by default.
        /// </summary>
        protected readonly object syncLock = new object();

        /// <summary>
        /// Gets or sets the highest possible value (denominator) of the progress bar.
        /// </summary>
        public int Denominator
        {
            get
            {
                lock (syncLock)
                {
                    return _denominator;
                }
            }
            set
            {
                lock (syncLock)
                {
                    if (value != _denominator)
                    {
                        if (value <= 0)
                        {
                            throw new ArgumentException("The denominator must be greater than zero.");
                        }
                        _denominator = value;
                        if (_denominator > Value)
                        {
                            Value = _denominator;
                        }
                        _textureNeedsCalculation = true;
                    }
                }
            }
        }

        /// <summary>
        /// Create a new ProgressBar.
        /// </summary>
        public ProgressBar(Vector2 pos, Color fillColor, Color emptyColor, SpriteBatch sb)
            : base(null, pos, sb)
        {
            _fillColor = fillColor;
            _emptyColor = emptyColor;
        }

        private int _value;

        private int _heightScale = 1;

        /// <summary>
        /// Gets or sets the scaling of height to perform to this object.
        /// </summary>
        public int HeightScale
        {
            get
            {
                lock (syncLock)
                {
                    return _heightScale;
                }
            }
            set
            {
                lock (syncLock)
                {
                    if (_heightScale != value)
                    {
                        _heightScale = value;
                        _textureNeedsCalculation = true;
                    }
                }
            }
        }

        private int _widthScale = 1;

        /// <summary>
        /// Gets or sets the scaling of width to perform to this object.
        /// </summary>
        public int WidthScale
        {
            get
            {
                lock (syncLock)
                { return _widthScale; }
            }
            set
            {
                lock (syncLock)
                {
                    if (_widthScale != value)
                    {
                        _widthScale = value;
                        _textureNeedsCalculation = true;
                    }
                }
            }
        }


        /// <summary>
        /// Gets the percentage (not fraction) of the progress bar that is complete.
        /// </summary>
        /// <remarks>
        /// Returns a value from 0 to 100.
        /// </remarks>
        public float Percentage
        {
            get
            {
                lock (syncLock)
                {
                    return 100f * (Value.ToFloat() / Denominator.ToFloat());
                }
            }
        }

        /// <summary>
        /// A boolean indicating whether the texture needs to be recalculated.
        /// </summary>
        protected bool _textureNeedsCalculation = true;
        private Texture2D _cachedTexture = null;

        /// <summary>
        /// Gets or sets the numerator (value) of the progress bar.
        /// </summary>
        public int Value
        {
            get
            {
                lock (syncLock)
                { return _value; }
            }
            set
            {
                lock (syncLock)
                {
                    if (value != _value)
                    {
                        if (value > Denominator)
                        {
                            //Set value to denominator
                            value = Denominator;
                            //throw new ArgumentException("The value cannot be greater than the denominator.");
                        }
                        if (ProgressBarFilled != null && value == Denominator)
                        {
                            ProgressBarFilled(this, EventArgs.Empty);
                        }
                        _value = value;
                        _textureNeedsCalculation = true;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the color to show for portions of the progress bar that are full.
        /// </summary>
        public Color FillColor
        {
            get
            {
                return _fillColor;
            }

            set
            {
                if (value != _fillColor)
                {
                    _fillColor = value;
                    _textureNeedsCalculation = true;
                }
            }
        }

        private Color _fillColor;

        /// <summary>
        /// An event fired when the progress bar is filled (value is set to denominator).
        /// </summary>
        public event EventHandler ProgressBarFilled;

        /// <summary>
        /// Gets or sets a representation of the texture to use for the progress bar.
        /// </summary>
        /// <remarks>
        /// Setting this texture will result in (slow) image processing to create data.
        /// It does not assume the same color scheme, but it does assume the same scale.
        /// If the bar is one color, the program will parse it as 0% unless the color is the FillColor of this ProgressBar.
        /// If the color scheme is inverted in the texture you are setting this ProgressBar to, you should be aware of this.
        /// Parsing any non-null texture will not result in a change to <see cref="FillColor"/> and <see cref="EmptyColor"/>, but will instead set the appropriate values and cause a change in the values of this progress bar.
        /// </remarks>
        public override Texture2D Texture
        {
            get
            {
                lock (syncLock)
                {
                    if (_cachedTexture != null && !_textureNeedsCalculation)
                    {
                        return _cachedTexture;
                    }
                    Texture2D returnValue = new Texture2D(SpriteBatch.GraphicsDevice, Denominator * _widthScale, _heightScale);

                    Color[] data = new Color[returnValue.Width * returnValue.Height];

                    for (int i = 0; i < data.Length; i++)
                    {
                        if (i % returnValue.Width < Value * _widthScale)
                        {
                            data[i] = _fillColor;
                        }
                        else
                        {
                            data[i] = _emptyColor;
                        }
                    }

                    returnValue.SetData<Color>(data);
                    _cachedTexture = returnValue;
                    _textureNeedsCalculation = false;
                    return returnValue;
                }
            }
            set
            {
                lock (syncLock)
                {
                    if (value != null)
                    {
                        Color[] textureData = new Color[value.Width * value.Height];
                        if (textureData.Length == 0)
                        {
                            throw new ArgumentException("The texture must have a size greater than zero in both dimensions.");
                        }
                        value.GetData<Color>(textureData);
                        if (textureData.Distinct().ToArray().Length > 2)
                        {
                            throw new ArgumentException("The texture must have less than or equal to 2 colors.");
                        }
                        if (textureData.Distinct().ToArray().Length == 1)
                        {
                            //0% or 100%, let's check the data against our colors to see if full (we assume not inverted)
                            Denominator = value.Width / _widthScale;
                            Value = textureData[0] == _fillColor ? Denominator : 0;
                        }
                        else
                        {
                            Color colorOnLeft = textureData[0];

                            int progBarValue = 0;

                            //This is scale accounted
                            int progBarDenom = textureData.Length / value.Height / _widthScale;
                            //Begin parsing, we know color on left is value
                            for (int i = 0; i < progBarDenom; i++)
                            {
                                //We only need first row
                                if (textureData[i] == colorOnLeft)
                                {
                                    progBarValue++;
                                }
                            }

                            //Account for current scale, assign variables
                            Denominator = progBarDenom;
                            Value = progBarValue / _widthScale;
                        }
                        //throw new NotImplementedException("You cannot set the texture of a progress bar.");
                    }
                    else
                    {
                        FillColor = Color.Transparent;

                        EmptyColor = Color.Transparent;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the color to show for portions of the progress bar that are empty.
        /// </summary>
        public Color EmptyColor
        {
            get { return _emptyColor; }
            set
            {
                if (value != _emptyColor)
                {
                    _emptyColor = value;
                    _textureNeedsCalculation = true;
                }
            }
        }
        
        
        private Color _emptyColor;

    }
}
