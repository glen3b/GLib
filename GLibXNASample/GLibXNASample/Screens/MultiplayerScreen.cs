using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glib.XNA;
using Glib.XNA.SpriteLib;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.GamerServices;

namespace GLibXNASample.Screens
{
    /// <summary>
    /// This is a class extending <see cref="Screen"/>.
    /// </summary>
    public class MultiplayerScreen : Screen
    {
        TextSprite title;
        TextSprite hostSession;
        TextSprite joinSession;

        public MultiplayerScreen(SpriteBatch sb)
            : base(sb, Color.Chocolate * 0.9f)
        {
            ClearColor.A = 255;

            Name = "MultiPlayer";

            GLibXNASampleGame.Instance.SessionManagement.SessionsFound += new EventHandler<Glib.XNA.NetworkLib.NetworkSessionsFoundEventArgs>(SessionManagement_SessionsFound);
            GLibXNASampleGame.Instance.SessionManagement.SessionJoined += new EventHandler<Glib.XNA.NetworkLib.NetworkSessionJoinedEventArgs>(SessionManagement_SessionJoined);


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

            joinSession = new TextSprite(sb, new Vector2(0, hostSession.Y + hostSession.Height + 5), GLibXNASampleGame.Instance.Content.Load<SpriteFont>("MenuItem"), "Join Session", Color.MediumSpringGreen);
            joinSession.X = joinSession.GetCenterPosition(Graphics.Viewport).X;
            joinSession.IsHoverable = true;
            joinSession.HoverColor = Color.SpringGreen;
            joinSession.Pressed += new EventHandler(joinSession_Pressed);
            AdditionalSprites.Add(joinSession);
        }

        void SessionManagement_SessionJoined(object sender, Glib.XNA.NetworkLib.NetworkSessionJoinedEventArgs e)
        {
            if (e.Error != null || e.Joined == null)
            {
                //TODO: Errors reported to user
                GLibXNASampleGame.Instance.SetScreen("MultiPlayer");
                return;
            }
        }

        void joinSession_Pressed(object sender, EventArgs e)
        {
            GLibXNASampleGame.Instance.SetScreen("Loading");
            GLibXNASampleGame.Instance.SessionManagement.FindSessions(NetworkSessionType.SystemLink, 4);
        }

        void SessionManagement_SessionsFound(object sender, Glib.XNA.NetworkLib.NetworkSessionsFoundEventArgs e)
        {
            //TODO: Make this better:
            //Session list screen [User chooses session]
            //Errors reported to user

            if (!e.SessionsFound || e.Error != null)
            {
                GLibXNASampleGame.Instance.SetScreen("MultiPlayer");
                return;
            }
            e.SessionToJoin = e.AvailableSessions[0];
        }

        /// <summary>
        /// An event handler invoked when the hostSession TextSprite is pressed (using the mouse or otherwise).
        /// </summary>
        void hostSession_Pressed(object sender, EventArgs e)
        {
            GLibXNASampleGame.Instance.SetScreen("Loading");

            //The SessionManagerComponent does not provide methods for the creation of NetworkSessions
            //It must me done through the NetworkSession class directly
            NetworkSession.BeginCreate(NetworkSessionType.SystemLink, 1, 4, onSessionCreation, null);
        }

        private void onSessionCreation(IAsyncResult res)
        {
            NetworkSession createdSession = null;
            try
            {
                createdSession = NetworkSession.EndCreate(res);
            }
            catch
            {
                createdSession = null;
            }

            if (createdSession == null)
            {
                if (!Guide.IsVisible)
                {
                    Guide.BeginShowMessageBox(PlayerIndex.One, "Error Creating Multiplayer Game", "An error occurred during the creation of the multiplayer session.", new string[] { "OK" }, 0, MessageBoxIcon.Error, null, null);
                }
                GLibXNASampleGame.Instance.SetScreen("MainMenu");
                return;
            }

            GLibXNASampleGame.Instance.SessionManagement.JoinSession(createdSession);
            GLibXNASampleGame.Instance.NetworkTransmitter.Session = createdSession;
            GLibXNASampleGame.Instance.SetScreen("NetworkLobby");
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
                    if (Gamer.SignedInGamers.Count <= 0)
                    {
                        if(!Guide.IsVisible){
                            Guide.ShowSignIn(1, false);
                        }
                        GLibXNASampleGame.Instance.SetScreen("MainMenu");
                        return;
                    }

                    GLibXNASampleGame.Instance.SetMouseVisible(true);
                }
            }
        }
    }
}
