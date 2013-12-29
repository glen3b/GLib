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

namespace GLibXNASample.Screens
{
    /// <summary>
    /// This is a class extending <see cref="Screen"/>, it is the video player demo.
    /// </summary>
    public class VideoPlayer : Screen
    {
        TextSprite title;
        TextSprite escapeReturnDesc;

        /// <summary>
        /// Creates and initializes the video player sample screen.
        /// </summary>
        /// <param name="spriteBatch">The <see cref="SpriteBatch"/> to render to.</param>
        public VideoPlayer(SpriteBatch spriteBatch)
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
        }

        void KeyboardManager_KeyDown(object source, SingleKeyEventArgs e)
        {
            if (e.Key == Keys.Escape)
            {
                GLibXNASampleGame.Instance.SetScreen("MainMenu");
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
