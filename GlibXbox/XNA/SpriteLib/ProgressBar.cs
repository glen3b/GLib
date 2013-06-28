using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// Represents a progress bar.
    /// </summary>
    public class ProgressBar : Sprite
    {
        private int _denominator = 10;

        /// <summary>
        /// Gets or sets the highest possible value (denominator) of the progress bar.
        /// </summary>
        public int Denominator
        {
            get { return _denominator; }
            set {
                if (value <= 0)
                {
                    throw new ArgumentException("The denominator must be greater than zero.");
                }
                _denominator = value;
                if (_denominator > Value)
                {
                    Value = _denominator;
                }
            }
        }

        /// <summary>
        /// Create a new ProgressBar.
        /// </summary>
        public ProgressBar(Vector2 pos, Color fillColor, Color emptyColor, SpriteBatch sb) : base(null, pos, sb)
        {
            FillColor = fillColor;
            EmptyColor = emptyColor;
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
                return _heightScale;
            }
            set
            {
                _heightScale = value;
            }
        }

        private int _widthScale = 1;

        /// <summary>
        /// Gets or sets the scaling of width to perform to this object.
        /// </summary>
        public int WidthScale
        {
            get { return _widthScale; }
            set { _widthScale = value; }
        }
        

        /// <summary>
        /// Gets the percentage (not fraction) of the progress bar that is complete.
        /// </summary>
        public float Percentage
        {
            get
            {
                return 100f * (Value.ToFloat() / Denominator.ToFloat());
            }
        }

        /// <summary>
        /// Gets or sets the numerator (value) of the progress bar.
        /// </summary>
        public int Value
        {
            get { return _value; }
            set {
                if (value > Denominator)
                {
                    throw new ArgumentException("The value cannot be greater than the denominator.");
                }
                _value = value;
            }
        }

        /// <summary>
        /// The color to show for portions of the progress bar that are full.
        /// </summary>
        public Color FillColor;

        /// <summary>
        /// Gets a representation of the texture to use for the progress bar.
        /// </summary>
        public override Texture2D Texture
        {
            get
            {
                Texture2D returnValue = new Texture2D(SpriteBatch.GraphicsDevice, Denominator * _widthScale, _heightScale);

                Color[] data = new Color[returnValue.Width*returnValue.Height];

                for (int i = 0; i < data.Length; i++)
                {
                    if (i % returnValue.Width < Value*_widthScale)
                    {
                        data[i] = FillColor;
                    }
                    else
                    {
                        data[i] = EmptyColor;
                    }
                }

                returnValue.SetData<Color>(data);

                return returnValue;
            }
            set
            {
                if(value != null){
                throw new NotImplementedException("You cannot set the texture of a progress bar.");
                }
            }
        }

        /// <summary>
        /// The color to show for portions of the progress bar that are empty.
        /// </summary>
        public Color EmptyColor;
        
    }
}
