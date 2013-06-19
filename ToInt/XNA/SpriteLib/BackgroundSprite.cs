using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// Represents a background Sprite which tiles a background.
    /// </summary>
    public class BackgroundSprite : Sprite, ISpriteBatchManagerSprite
    {
        private bool _showStitches = false;

        /// <summary>
        /// Gets or sets a boolean indicating whether or not to show stitches.
        /// </summary>
        public bool ShowStitches
        {
            get { return _showStitches; }
            set { _showStitches = value; }
        }
        

        private List<KeyValuePair<Vector2, SpriteEffects>> _bgList;

        /// <summary>
        /// Create a new BackgroundSprite.
        /// </summary>
        /// <param name="texture">The sprite texture.</param>
        /// <param name="sb">The SpriteBatch.</param>
        /// <param name="height">The height of the background matrix.</param>
        /// <param name="width">The width of the background matrix.</param>
        public BackgroundSprite(Texture2D texture, SpriteBatch sb, int height, int width)
            : base(texture, Vector2.Zero, sb)
        {
            _bgList = new List<KeyValuePair<Vector2, SpriteEffects>>();
            //Create background matrix
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    //The x-coordinate moves by the width of the texture, flipping the image horizontally to prevent visible stitches
                    //The y-coordinate moves with each row, flipping the image vertically
                    Vector2 bgImgPos = new Vector2(Texture.Width * col, Texture.Height * row);

                    //DEBUG purposes
                    if (_showStitches)
                    {
                        bgImgPos += new Vector2(col, row);
                    }

                    //Flip every other image horizontally
                    SpriteEffects effects = col % 2 == 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

                    //Flip every other row vertically, too
                    effects |= row % 2 == 0 ? SpriteEffects.None : SpriteEffects.FlipVertically;

                    //Add to list
                    _bgList.Add(new KeyValuePair<Vector2, SpriteEffects>(bgImgPos, effects));
                }
            }
        }

        /// <summary>
        /// Draw this background sprite w/o opening/closing the SpriteBatch.
        /// </summary>
        public override void DrawNonAuto()
        {
            foreach (KeyValuePair<Vector2, SpriteEffects> bgItem in _bgList)
            {
                SpriteBatch.Draw(Texture, bgItem.Key, null, Color, 0f, Vector2.Zero, Scale, bgItem.Value, 0f);
                //base.Draw(spriteBatch, bgItem.Key, bgItem.Value);
            }
        }

        /// <summary>
        /// Sets the viewport to the middle of the BackgroundSprite Texture.
        /// </summary>
        public void CenterViewport()
        {
            if (Texture.Width > (UsedViewport.HasValue ? UsedViewport.Value : SpriteBatch.GraphicsDevice.Viewport).Width)
            {
                X = -(Texture.Width - (UsedViewport.HasValue ? UsedViewport.Value : SpriteBatch.GraphicsDevice.Viewport).Width) / 2;
            }

            if (Texture.Height > (UsedViewport.HasValue ? UsedViewport.Value : SpriteBatch.GraphicsDevice.Viewport).Height)
            {
                Y = -(Texture.Height - (UsedViewport.HasValue ? UsedViewport.Value : SpriteBatch.GraphicsDevice.Viewport).Height) / 2;
            }
        }
        }
    }
