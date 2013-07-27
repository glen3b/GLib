using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glib.XNA.SpriteLib;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Net;

namespace NetworkTest
{
    public class SessionInfoDisplay : TextSprite
    {
        public AvailableNetworkSession Session;

        public SessionInfoDisplay(SpriteBatch s, Vector2 v, SpriteFont f, string t, AvailableNetworkSession sess)
            : base(s, v, f, t)
        {
            Session = sess;
        }
    }
}
