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
    public class LoadingScreen : Screen
    {
        TextSprite loadingText;

        public LoadingScreen(SpriteBatch sb)
            : base(sb, Color.DarkOliveGreen)
        {
            Name = "Loading";
            loadingText = new TextSprite(sb, GLibXNASampleGame.Instance.Content.Load<SpriteFont>("Subtitle"), "Loading...", Color.Cornsilk);
            loadingText.Position = loadingText.GetCenterPosition(Graphics.Viewport);
            AdditionalSprites.Add(loadingText);
        }
    }
}
