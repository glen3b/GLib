using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glib.XNA;
using Glib.XNA.NetworkLib;
using Glib.XNA.SpriteLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Net;

namespace GLibXNASample.Screens
{
    public class GameLobbyScreen : Screen
    {
        TextSprite title;
        TextSprite gamerList;


        public GameLobbyScreen(SpriteBatch sb)
            : base(sb, Color.Gainsboro * 0.75f)
        {
            ClearColor.A = 255;
            Name = "NetworkLobby";

            title = new TextSprite(sb, GLibXNASampleGame.Instance.Content.Load<SpriteFont>("Title"), "Lobby", Color.ForestGreen);
            title.X = title.GetCenterPosition(Graphics.Viewport).X;
            title.Y = 15;
            AdditionalSprites.Add(title);

            gamerList = new TextSprite(sb, GLibXNASampleGame.Instance.Content.Load<SpriteFont>("MenuItem"), "", Color.IndianRed);
            gamerList.Position = gamerList.GetCenterPosition(Graphics.Viewport);
            AdditionalSprites.Add(gamerList);
        }

        public override bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                if (value != Visible)
                {
                    base.Visible = value;
                    if (value)
                    {
                        //Clear the list of gamers
                        gamerList.Text = String.Empty;
                        gamerList.Position = gamerList.GetCenterPosition(Graphics.Viewport);

                        //Subscribe to events relating to Gamers joining and leaving the NetworkSession
                        GLibXNASampleGame.Instance.SessionManagement.Session.GamerJoined += new EventHandler<GamerJoinedEventArgs>(Session_GamerJoined);
                        GLibXNASampleGame.Instance.SessionManagement.Session.GamerLeft += new EventHandler<GamerLeftEventArgs>(Session_GamerLeft);

                        //foreach (NetworkGamer gamer in GLibXNASampleGame.Instance.SessionManagement.Session.AllGamers)
                        //{
                        //    gamerList.Text += gamer.Gamertag + Environment.NewLine;
                        //}
                    }
                    else
                    {
                        if (GLibXNASampleGame.Instance.SessionManagement.Session != null)
                        {
                            GLibXNASampleGame.Instance.SessionManagement.LeaveSession();
                        }
                    }
                }
            }
        }

        void Session_GamerLeft(object sender, GamerLeftEventArgs e)
        {
            gamerList.Text.Replace(e.Gamer.Gamertag + Environment.NewLine, "");
            gamerList.Position = gamerList.GetCenterPosition(Graphics.Viewport);
        }

        void Session_GamerJoined(object sender, GamerJoinedEventArgs e)
        {
            gamerList.Text += e.Gamer.Gamertag + Environment.NewLine;
            gamerList.Position = gamerList.GetCenterPosition(Graphics.Viewport);
        }
    }
}
