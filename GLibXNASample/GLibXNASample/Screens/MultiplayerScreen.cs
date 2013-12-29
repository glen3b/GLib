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

        public MultiplayerScreen(SpriteBatch sb)
            : base(sb, Color.Chocolate * 0.8f)
        {
            Name = "MultiPlayer";

            //See MainMenu for TextSprite sample comments
            title = new TextSprite(sb, GLibXNASampleGame.Instance.Content.Load<SpriteFont>("Title"), "Networking Sample", Color.Gold);
            title.Position = new Vector2(title.GetCenterPosition(Graphics.Viewport).X, 15);
            AdditionalSprites.Add(title);
        }
    }
}
