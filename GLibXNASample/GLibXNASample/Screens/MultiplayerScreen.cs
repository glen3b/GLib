using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glib.XNA;
using Glib.XNA.SpriteLib;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GLibXNASample.Screens
{
    /// <summary>
    /// This is a class extending <see cref="Screen"/>.
    /// </summary>
    public class MultiplayerScreen : Screen
    {
        TextSprite title;
        TextSprite hostSession;

        public MultiplayerScreen(SpriteBatch sb)
            : base(sb, Color.Chocolate * 0.9f)
        {
            ClearColor.A = 255;

            Name = "MultiPlayer";

            //See MainMenu for TextSprite sample comments
            title = new TextSprite(sb, GLibXNASampleGame.Instance.Content.Load<SpriteFont>("Title"), "Networking Sample", Color.Gold);
            title.Position = new Vector2(title.GetCenterPosition(Graphics.Viewport).X, 15);
            AdditionalSprites.Add(title);

            hostSession = new TextSprite(sb, new Vector2(0, title.Y + title.Height + 5), GLibXNASampleGame.Instance.Content.Load<SpriteFont>("MenuItem"), "Host Session", Color.MediumSpringGreen);
            hostSession.X = hostSession.GetCenterPosition(Graphics.Viewport).X;
            hostSession.IsHoverable = true;
            hostSession.HoverColor = Color.SpringGreen;
            hostSession.Pressed += new EventHandler(hostSession_Pressed);
            AdditionalSprites.Add(hostSession);
        }

        /// <summary>
        /// An event handler invoked when the hostSession TextSprite is pressed (using the mouse or otherwise).
        /// </summary>
        void hostSession_Pressed(object sender, EventArgs e)
        {

        }

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
                    GLibXNASampleGame.Instance.SetMouseVisible(true);
                }
            }
        }
    }
}
