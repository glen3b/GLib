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
using Glib.XNA.InputLib;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;

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

            KeyboardManager.KeyDown += new SingleKeyEventHandler(KeyboardManager_KeyDown);
        }

        void KeyboardManager_KeyDown(object source, SingleKeyEventArgs e)
        {
            if (Visible && e.Key == Keys.R)
            {
                foreach (LocalNetworkGamer gamer in GLibXNASampleGame.Instance.SessionManagement.Session.LocalGamers)
                {
                    gamer.IsReady = !gamer.IsReady;
                }
            }
            else if (Visible && (e.Key == Keys.Space || e.Key == Keys.Enter) && GLibXNASampleGame.Instance.SessionManagement.Session.AllGamers.Count >= 2 && GLibXNASampleGame.Instance.SessionManagement.Session.IsHost && GLibXNASampleGame.Instance.SessionManagement.Session.SessionState == NetworkSessionState.Lobby && GLibXNASampleGame.Instance.SessionManagement.Session.IsEveryoneReady)
            {
                GLibXNASampleGame.Instance.SessionManagement.Session.StartGame();
            }
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

                        //Subscribe to the game start event
                        GLibXNASampleGame.Instance.SessionManagement.Session.GameStarted += new EventHandler<GameStartedEventArgs>(Session_GameStarted);

                        //Populate the gamer list
                        foreach (Gamer g in GLibXNASampleGame.Instance.SessionManagement.Session.AllGamers)
                        {
                            gamerList.Text += g.Gamertag + Environment.NewLine;
                        }
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

        void Session_GameStarted(object sender, GameStartedEventArgs e)
        {
            if (!Guide.IsVisible)
            {
                Guide.BeginShowMessageBox("Multiplayer!", "You joined a network session that just started! Our sample ends here. Go wherever you want with your code now!", new string[] { "Great!" }, 0, MessageBoxIcon.None, toMPScreen, null);
            }
        }

        void toMPScreen(IAsyncResult r)
        {
            GLibXNASampleGame.Instance.SetScreen("MainMenu");
        }

        void Session_GamerLeft(object sender, GamerLeftEventArgs e)
        {
            gamerList.Text.Replace(e.Gamer.Gamertag + Environment.NewLine, "");
            gamerList.Position = gamerList.GetCenterPosition(Graphics.Viewport);
        }

        void Session_GamerJoined(object sender, GamerJoinedEventArgs e)
        {
            if (!gamerList.Text.Contains(e.Gamer.Gamertag + Environment.NewLine))
            {
                gamerList.Text += e.Gamer.Gamertag + Environment.NewLine;
                gamerList.Position = gamerList.GetCenterPosition(Graphics.Viewport);
            }
        }
    }
}
