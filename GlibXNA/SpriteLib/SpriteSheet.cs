using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// Represents an animated sprite sheet.
    /// For a non animated sprite sheet, please use a <see cref="Sprite"/> and set the appropriate properties.
    /// </summary>
    public class SpriteSheet : Sprite, ITimerSprite
    {
        /// <summary>
        /// Gets or sets the height of the current frame.
        /// </summary>
        public override float Height
        {
            get
            {
                return CurrentFrame == null ? 0 : (DrawRegion.HasValue ? CurrentFrame.DrawRegion.Value.Height * CurrentFrame.Scale.Y : CurrentFrame.Texture.Height * CurrentFrame.Scale.Y);
            }
            set
            {
                CurrentFrame.Scale = new Vector2(CurrentFrame.Scale.X, (value * (CurrentFrame.DrawRegion.HasValue ? CurrentFrame.Texture.Height.ToFloat() / CurrentFrame.DrawRegion.Value.Height.ToFloat() : 1)) / Texture.Height);
            }
        }

        /// <summary>
        /// Gets or sets the width of the current frame.
        /// </summary>
        public override float Width
        {
            get
            {
                return CurrentFrame == null ? 0 : (DrawRegion.HasValue ? CurrentFrame.DrawRegion.Value.Width * CurrentFrame.Scale.X : CurrentFrame.Texture.Width * CurrentFrame.Scale.X);
            }
            set
            {
                CurrentFrame.Scale = new Vector2((value * (CurrentFrame.DrawRegion.HasValue ? CurrentFrame.Texture.Width.ToFloat() / CurrentFrame.DrawRegion.Value.Width.ToFloat() : 1)) / Texture.Width, CurrentFrame.Scale.Y);
            }
        }

        /// <summary>
        /// Creates a <see cref="SpriteSheet"/>.
        /// </summary>
        /// <param name="position">The position of the <see cref="SpriteSheet"/>.</param>
        /// <param name="batch">The <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> to render to.</param>
        /// <param name="frames">The collection of frames that makes up this <see cref="SpriteSheet"/>.</param>
        public SpriteSheet(Vector2 position, SpriteBatch batch, IEnumerable<Frame> frames)
            : base(null, position, batch)
        {
            if (frames == null)
            {
                throw new ArgumentNullException("frames");
            }

            _frames = new FrameCollection();

            foreach (Frame frame in frames)
            {
                if (frame == null)
                {
                    throw new ArgumentException("The frames array cannot contain null elements.");
                }

                Frames.Add(frame);
            }
        }

        /// <summary>
        /// A delegate method intended to center a sprite upon an event firing.
        /// </summary>
        /// <param name="sender">The sprite that fired the event.</param>
        /// <param name="args">The event arguments.</param>
        public static void CenterSprite(object sender, EventArgs args)
        {
            if (!(sender is Sprite))
            {
                throw new InvalidOperationException("Cannot center something that is not an instance of sprite.");
            }
            ((ISizedScreenObject)sender).Position = ((ISizedScreenObject)sender).GetCenterPosition(((Sprite)sender).SpriteBatch.GraphicsDevice.Viewport, ((Sprite)sender).Origin);
        }

        private FrameCollection _frames;

        /// <summary>
        /// Gets the collection of frames to display.
        /// </summary>
        public FrameCollection Frames
        {
            get { return _frames; }
        }

        /// <summary>
        /// Gets the current frame.
        /// </summary>
        public Frame CurrentFrame
        {
            get
            {
                if (_frames == null)
                {
                    return null;
                }

                if (Frames.Count <= 0)
                {
                    throw new InvalidOperationException("There are no frames in this SpriteSheet.");
                }
                return Frames[CurrentFrameIndex];
            }
        }

        private int _currentFrameIndex;

        /// <summary>
        /// Gets or sets the current frame's index.
        /// </summary>
        public int CurrentFrameIndex
        {
            get
            {
                if (_frames == null)
                {
                    return -1;
                }

                if (_currentFrameIndex >= _frames.Count)
                {
                    _currentFrameIndex = _frames.Count - 1;
                }

                if (_currentFrameIndex <= 0)
                {
                    _currentFrameIndex = 0;
                }

                return _currentFrameIndex;
            }
            set
            {
                if (_frames == null)
                {
                    return;
                }


                if (value != _currentFrameIndex)
                {
                    if (value < 0 || value >= _frames.Count)
                    {
                        throw new ArgumentOutOfRangeException("CurrentFrameIndex");
                    }
                    _currentFrameIndex = value;
                    if (FrameChanged != null)
                    {
                        FrameChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the draw region of the current frame.
        /// </summary>
        public override Rectangle? DrawRegion
        {
            get
            {
                return CurrentFrame.DrawRegion;
            }
            set
            {
                if (_frames == null)
                {
                    return;
                }

                CurrentFrame.DrawRegion = value;
            }
        }

        /// <summary>
        /// Gets or sets the texture of the current frame.
        /// </summary>
        public override Texture2D Texture
        {
            get
            {
                return CurrentFrame.Texture;
            }
            set
            {
                if (_frames == null)
                {
                    return;
                }

                CurrentFrame.Texture = value;
            }
        }

        /// <summary>
        /// Gets or sets the scale of the current frame of the <see cref="SpriteSheet"/>.
        /// </summary>
        public override Vector2 Scale
        {
            get
            {
                return CurrentFrame.Scale;
            }
            set
            {
                if (_frames == null)
                {
                    return;
                }

                CurrentFrame.Scale = value;
            }
        }

        /// <summary>
        /// Gets or sets the origin of the current frame of the <see cref="SpriteSheet"/>.
        /// </summary>
        public override Vector2 Origin
        {
            get
            {
                return CurrentFrame.Origin;
            }
            set
            {
                if (_frames == null)
                {
                    return;
                }

                CurrentFrame.Origin = value;
            }
        }

        /// <summary>
        /// The amount of time that has passed whilst on this frame.
        /// </summary>
        protected TimeSpan _elapsedFrameTime;

        private TimeSpan _frameTime;

        /// <summary>
        /// Gets or sets the amount of time to spend on each frame.
        /// </summary>
        public TimeSpan FrameTime
        {
            get { return _frameTime; }
            set
            {
                if (value < TimeSpan.Zero)
                {
                    throw new ArgumentOutOfRangeException("FrameTime");
                }
                _frameTime = value;
            }
        }

        /// <summary>
        /// An event fired when the animation has finished one cycle.
        /// If this event is cancelled the animation will pause.
        /// </summary>
        public event EventHandler<CancelEventArgs> AnimationCompleted;

        /// <summary>
        /// An event fired after the current frame changes.
        /// </summary>
        public event EventHandler FrameChanged;

        private bool _isPaused;

        /// <summary>
        /// Gets or sets a boolean indicating if this animation is paused.
        /// </summary>
        public bool IsPaused
        {
            get { return _isPaused; }
            set { _isPaused = value; }
        }


        /// <summary>
        /// Updates this sprite sheet.
        /// </summary>
        public override void Update()
        {
            if (!Visible)
            {
                return;
            }

            if (!IsPaused && _elapsedFrameTime >= FrameTime)
            {
                //Switch frame
                _elapsedFrameTime = TimeSpan.Zero;

                if (CurrentFrameIndex + 1 >= Frames.Count)
                {
                    if (AnimationCompleted != null)
                    {
                        CancelEventArgs args = new CancelEventArgs();
                        AnimationCompleted(this, args);
                        if (args.Cancel)
                        {
                            IsPaused = true;
                        }
                    }

                    //Check again because event could cause a pause mid-frame
                    if (!IsPaused)
                    {
                        CurrentFrameIndex = 0;
                    }

                }
                else
                {
                    CurrentFrameIndex++;
                }
            }

            base.Update();


        }

        /// <summary>
        /// Updates the <see cref="SpriteSheet"/>, ticking the elapsed time on this frame.
        /// </summary>
        /// <param name="gameTime">A snapshot of game timing values.</param>
        public void Update(GameTime gameTime)
        {
            if (!Visible)
            {
                return;
            }

            if (!IsPaused)
            {
                _elapsedFrameTime += gameTime.ElapsedGameTime;
            }

            Update();
        }
    }
}
