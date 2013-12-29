using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glib.XNA;
using Glib.XNA.SpriteLib;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Glib.XNA.InputLib;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GLibXNASample.Screens
{
    /// <summary>
    /// This is a class extending <see cref="Screen"/>, it is the video player demo.
    /// </summary>
    public class VideoPlayerScreen : Screen
    {
        TextSprite title;
        TextSprite escapeReturnDesc;
        VideoSprite video;
        TextSprite creditTextSprite;

        /// <summary>
        /// Creates and initializes the video player sample screen.
        /// </summary>
        /// <param name="spriteBatch">The <see cref="SpriteBatch"/> to render to.</param>
        public VideoPlayerScreen(SpriteBatch spriteBatch)
            : base(spriteBatch, Color.DarkGray)
        {
            Name = "VideoPlayer";

            //Reduces the ARGB values, then resets alpha to 255
            ClearColor *= 1f/6f;
            ClearColor.A = 255;

            //See MainMenu for TextSprite sample comments
            title = new TextSprite(spriteBatch, GLibXNASampleGame.Instance.Content.Load<SpriteFont>("Title"), "VideoPlayer Sample", Color.PaleGoldenrod);
            title.Position = new Vector2(title.GetCenterPosition(spriteBatch.GraphicsDevice.Viewport).X, 15);
            AdditionalSprites.Add(title);

            escapeReturnDesc = new TextSprite(spriteBatch, GLibXNASampleGame.Instance.Content.Load<SpriteFont>("Details"), "Press escape\nto return to\nthe main menu", Color.PaleGoldenrod);
            escapeReturnDesc.Position = new Vector2(3);
            AdditionalSprites.Add(escapeReturnDesc);

            //This event is fired when a key is pressed
            KeyboardManager.KeyDown += new SingleKeyEventHandler(KeyboardManager_KeyDown);

            creditTextSprite = new TextSprite(spriteBatch, GLibXNASampleGame.Instance.Content.Load<SpriteFont>("Details"), "Video obtained from nps.gov/cany/planyourvisit/rivervideos.htm", Color.Goldenrod);
            creditTextSprite.Color.A = 32;
            creditTextSprite.Position = new Vector2(creditTextSprite.GetCenterPosition(spriteBatch.GraphicsDevice.Viewport).X, spriteBatch.GraphicsDevice.Viewport.Height - creditTextSprite.Height - 5);
            AdditionalSprites.Add(creditTextSprite);

            //VideoSprite: Like a sprite, but displays a video
            video = new VideoSprite(GLibXNASampleGame.Instance.Content.Load<Video>("VideoSample"), Vector2.Zero, spriteBatch);
            video.Video.Stop();
            video.Position = video.GetCenterPosition(spriteBatch.GraphicsDevice.Viewport);
            Sprites.Add(video);
        }

        void KeyboardManager_KeyDown(object source, SingleKeyEventArgs e)
        {
            if (e.Key == Keys.Escape)
            {
                GLibXNASampleGame.Instance.SetScreen("MainMenu");
            }
            else if (e.Key == Keys.Space)
            {
                if (video.Video.State == MediaState.Playing)
                {
                    video.Video.Pause();
                }
                else
                {
                    video.Video.Resume();
                }
            }
        }

        //Overriding Visible property of Screen class
        public override bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
                if (value)
                {
                    //Resets screen
                    escapeReturnDesc.Color = Color.PaleGoldenrod;
                    video.Video.Play(GLibXNASampleGame.Instance.Content.Load<Video>("VideoSample"));
                }
                else
                {
                    video.Video.Stop();
                }
            }
        }

        //See MainMenu for details of update function
        public override void Update(GameTime game)
        {
            base.Update(game);
            escapeReturnDesc.Color *= 0.9825f;
        }
    }
}
