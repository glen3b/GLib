using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// A class representing a Sprite from a sprite sheet. Supports sprite sheet animation.
    /// </summary>
    public class SpriteSheet : Sprite, ITimerSprite
    {

        private bool _restartAnimation = true;

        /// <summary>
        /// Whether or not to restart the animation when completed.
        /// </summary>
        public bool RestartAnimation
        {
            get { return _restartAnimation; }
            set { _restartAnimation = value; }
        }

        private bool _isComplete;

        /// <summary>
        /// Whether or not the animation is complete.
        /// </summary>
        /// <remarks>
        /// Is always false if RestartAnimation is true.
        /// </remarks>
        public bool IsComplete
        {
            get { return _isComplete; }
            set { _isComplete = value; }
        }
        


        /// <summary>
        /// The current zero-based row of Sprites in this SpriteSheet.
        /// </summary>
        public int CurrentRow
        {
            get
            {
                return CurrentSprite.Y;
            }
            set
            {
                CurrentSprite.Y = value;
            }
        }

        /// <summary>
        /// A scale-sensitive width of 1 sprite. Use SpriteSize.Width not to account for scale.
        /// </summary>
        public override float Width
        {
            get
            {
                return SpriteSize.Width * Scale.X;
            }
        }

        /// <summary>
        /// A scale-sensitive height of 1 sprite. Use SpriteSize.Height not to account for scale.
        /// </summary>
        public override float Height
        {
            get
            {
                return SpriteSize.Height * Scale.Y;
            }
        }

        /// <summary>
        /// The current zero-based column of Sprites in this SpriteSheet.
        /// </summary>
        public int CurrentColumn
        {
            get
            {
                return CurrentSprite.X;
            }
            set
            {
                CurrentSprite.X = value;
            }
        }

        /// <summary>
        /// The scale-sensitive center of the sprite.
        /// </summary>
        public override Vector2 Center
        {
            get
            {
                return new Vector2(X + (Width / 2), Y + (Height / 2));
                //return new Vector2(Rectangle.Center.X, Rectangle.Center.Y);
            }
        }

        /// <summary>
        /// The currently selected Sprite, where X is the zero-based column and Y is the zero-based row.
        /// </summary>
        public Point CurrentSprite = new Point(0,0);

        /// <summary>
        /// Get the region drawn relative to the upper left corner of this SpriteSheet based on the given row and column.
        /// </summary>
        /// <param name="row">The zero-based row of sprites to draw from.</param>
        /// <param name="column">The zero-based column of sprites to draw from.</param>
        /// <returns>The region to draw with the gived row and column.</returns>
        public Rectangle GetDrawRegion(int row, int column)
        {
            return new Rectangle(SpriteSize.Width * column, SpriteSize.Height * row, SpriteSize.Width, SpriteSize.Height);
        }

        /// <summary>
        /// The number of columns of Sprites in this SpriteSheet.
        /// </summary>
        public int Columns
        {
            get
            {
                return Convert.ToInt32(Texture.Width / SpriteSize.Width);
            }
        }

        /// <summary>
        /// The size of one Sprite.
        /// </summary>
        public readonly Rectangle SpriteSize;

        /// <summary>
        /// The number of rows in this SpriteSheet.
        /// </summary>
        public int Rows
        {
            get
            {
                return Convert.ToInt32(Texture.Height / SpriteSize.Height);
            }
        }

        /// <summary>
        /// The default delay between animations in milliseconds.
        /// Set to 0 to update every call to Update().
        /// </summary>
        public const int DefaultAnimationDelay = 0;

        /// <summary>
        /// The delay between changes of the sprite (animation) in this SpriteSheet, if any.
        /// </summary>
        /// <remarks>
        /// Set to no time to change every update.
        /// Set to null to not animate.
        /// </remarks>
        public TimeSpan? AnimationDelay = null;

        /// <summary>
        /// A boolean representing whether or not this SpriteSheet is automatically animated.
        /// </summary>
        public bool IsAnimated
        {
            get
            {
                return AnimationDelay.HasValue;
            }
            set
            {
                if (value && !AnimationDelay.HasValue)
                {
                    AnimationDelay = TimeSpan.FromMilliseconds(DefaultAnimationDelay);
                }
                else if(!value)
                {
                    AnimationDelay = null;
                }
            }
        }

        /// <summary>
        /// Construct a new SpriteSheet.
        /// </summary>
        /// <param name="sheet">The Texture2D of the actual sprite sheet.</param>
        /// <param name="size">The size of one sprite (X and Y are ignored in this paramater).</param>
        /// <param name="position">The initial position of this SpriteSheet.</param>
        /// <param name="sb">The SpriteBatch used for drawing.</param>
        public SpriteSheet(Texture2D sheet, Rectangle size, Vector2 position, SpriteBatch sb) : base(sheet, position, sb)
        {
            SpriteSize = size;
        }

        /// <summary>
        /// Construct a new SpriteSheet.
        /// </summary>
        /// <param name="sheet">The Texture2D of the actual sprite sheet.</param>
        /// <param name="size">The size of one sprite (X and Y are ignored in this paramater).</param>
        /// <param name="position">The initial position of this SpriteSheet.</param>
        /// <param name="sb">The SpriteBatch used for drawing.</param>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        public SpriteSheet(Texture2D sheet, Rectangle size, Vector2 position, SpriteBatch sb, int rows, int columns) : base(sheet, position, sb)
        {
            SpriteSize = size;
        }

        /// <summary>
        /// Whether or not to only draw the DrawRegion of the SpriteSheet.
        /// </summary>
        /// <remarks>
        /// Always true.
        /// </remarks>
        public new bool OnlyDrawRegion
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Construct a new SpriteSheet.
        /// </summary>
        /// <param name="manager">The ContentManager used to load the Texture2D of the ssprite sheet.</param>
        /// <param name="assetName">The asset name of the Texture2D containing the sprite sheet.</param>
        /// <param name="size">The size of one sprite (X and Y are ignored in this paramater).</param>
        /// <param name="position">The initial position of this SpriteSheet.</param>
        /// <param name="sb">The SpriteBatch used for drawing.</param>
        public SpriteSheet(Microsoft.Xna.Framework.Content.ContentManager manager, string assetName, Rectangle size, Vector2 position, SpriteBatch sb)
            : this(manager.Load<Texture2D>(assetName), size, position, sb)
        {

        }

        private TimeSpan elapsedTime = new TimeSpan();

        /// <summary>
        /// The current drawing region of the SpriteSheet.
        /// </summary>
        public new Rectangle DrawRegion
        {
            get
            {
                return GetDrawRegion(CurrentRow, CurrentColumn);
            }
        }

        /// <summary>
        /// Call the Updated event of this SpriteSheet, and animate the SpriteSheet if applicable.
        /// </summary>
        /// <param name="gt">The GameTime object passed to a Game's Update(GameTime) method.</param>
        public void Update(GameTime gt)
        {
            elapsedTime += gt.ElapsedGameTime;
            Update();
        }

        /// <summary>
        /// An event fired when the animation is completed, and either ends or is about to restart.
        /// </summary>
        public event EventHandler AnimationCompleted;

        /// <summary>
        /// Gets the effective origin of the Sprite.
        /// </summary>
        public override Vector2 Origin
        {
            get
            {
                return (UseCenterAsOrigin ? new Vector2(SpriteSize.Width / 2f, SpriteSize.Height / 2f) : Vector2.Zero);
            }
        }

        private bool _fireCompletionEvent = true;

        /// <summary>
        /// Call the Updated event of this SpriteSheet, WITHOUT ANIMATION TICKS.
        /// Will only animate if AnimationDelay is set to 0 milliseconds.
        /// </summary>
        public override void Update()
        {
            base.Update();
            if (IsAnimated)
            {
                if (elapsedTime.TotalMilliseconds >= AnimationDelay.Value.TotalMilliseconds)
                {
                    elapsedTime = TimeSpan.Zero;
                    CurrentColumn++;
                    if (CurrentColumn >= Columns)
                    {
                        CurrentColumn = 0;
                        CurrentRow++;
                        if (CurrentRow >= Rows && _restartAnimation)
                        {
                            if (AnimationCompleted != null && _fireCompletionEvent)
                            {
                                AnimationCompleted(this, EventArgs.Empty);

                            }
                            CurrentRow = 0;
                        }
                        else if (CurrentRow >= Rows)
                        {

                            IsComplete = true;
                            if (AnimationCompleted != null && _fireCompletionEvent)
                            {
                                AnimationCompleted(this, EventArgs.Empty);
                                _fireCompletionEvent = false;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Draw this SpriteSheet's current region to the screen, without automatic SpriteBatch handling.
        /// </summary>
        public override void DrawNonAuto()
        {
            SpriteBatch.Draw(Texture, Position, DrawRegion, Color, Rotation.Radians, Origin, Scale, SpriteEffects.None, 0f);
        }
    }
}
