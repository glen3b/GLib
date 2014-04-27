using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// A sprite that displays a video.
    /// </summary>
    /// <remarks>
    /// This class performs calculations that assume that the game operates at a constant framerate.
    /// </remarks>
    public class VideoSprite : Sprite
    {
        private static TextureFactory _textureCreator;

        private VideoPlayer _video;

        /// <summary>
        /// Gets the <see cref="VideoPlayer"/> representing the current video.
        /// </summary>
        public VideoPlayer Video
        {
            get { return _video; }
        }

        /// <summary>
        /// The size of the video.
        /// </summary>
        protected Vector2 VideoSize;

        /// <summary>
        /// Creates a <see cref="VideoSprite"/> and begins playing the video.
        /// </summary>
        /// <param name="videoToPlay">The <see cref="Microsoft.Xna.Framework.Media.Video"/> to play.</param>
        /// <param name="position">The position of the <see cref="VideoSprite"/>.</param>
        /// <param name="spriteBatch">The <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> to render the <see cref="Microsoft.Xna.Framework.Media.Video"/> to play.</param>
        public VideoSprite(Video videoToPlay, Vector2 position, SpriteBatch spriteBatch)
            : base((_textureCreator == null ? new TextureFactory(spriteBatch.GraphicsDevice) : _textureCreator).CreateSquare(1, new Color(Color.DarkGray.R, Color.DarkGray.G, Color.DarkGray.B, 100)), position, spriteBatch)
        {
            if (_textureCreator == null)
            {
                _textureCreator = new TextureFactory(spriteBatch.GraphicsDevice);
            }

            if (videoToPlay == null)
            {
                throw new ArgumentNullException("videoToPlay");
            }

            Texture = _textureCreator.CreateRectangle(videoToPlay.Width, videoToPlay.Height, new Color(Color.DarkGray.R, Color.DarkGray.G, Color.DarkGray.B, 100));

            _video = new VideoPlayer();
            _video.Play(videoToPlay);
            VideoSize = new Vector2(videoToPlay.Width, videoToPlay.Height);
        }

        /// <summary>
        /// Updates the video.
        /// </summary>
        public override void Update()
        {
            base.Update();
            VideoSize = Video.Video == null ? Vector2.Zero : new Vector2(Video.Video.Width, Video.Video.Height);

            if (Video.Video != null)
            {
                Texture = Video.GetTexture();
            }
        }

        ///// <summary>
        ///// The framerate of the <see cref="Game"/>, in FPS.
        ///// </summary>
        //protected double Framerate;

        ///// <summary>
        ///// Updates the video.
        ///// </summary>
        ///// <param name="gt">The current <see cref="GameTime"/>.</param>
        //public void Update(GameTime gt)
        //{
        //    ShouldRetrieveTexture = true;

        //    // // // // // // // // // // // // // // //
        //    //       Unneccesary logic follows        //
        //    // // // // // // // // // // // // // // //

        //    //if (gt.IsRunningSlowly)
        //    //{
        //    //    // Cannot accurately calculate framerate.
        //    //    ShouldRetrieveTexture = true;
        //    //}
        //    //else if (Video.Video != null)
        //    //{
        //    //    // Attempt to calculate framerate to the nearest second.
        //    //    Framerate = Math.Round(1.0 / gt.ElapsedGameTime.TotalSeconds);

        //    //    // Frames that have passed so far within the game.
        //    //    double frames = (gt.TotalGameTime.TotalSeconds / Framerate).Round();

        //    //    if (Framerate < Video.Video.FramesPerSecond.Round())
        //    //    {
        //    //        // The video has a greater framerate than the game. Deal with it!
        //    //        ShouldRetrieveTexture = true;
        //    //    }
        //    //    else if (Framerate > Video.Video.FramesPerSecond.Round())
        //    //    {
        //    //        // The game has a greater framerate than the video.
        //    //        ShouldRetrieveTexture = Math.Round((frames % (Framerate / Video.Video.FramesPerSecond)) * 10) / 10 == 0;
        //    //    }
        //    //    else
        //    //    {
        //    //        // The framerates are equal. Everyone is happy.
        //    //        ShouldRetrieveTexture = true;
        //    //    }
        //    //}
        //    Update();
        //}
    }
}
