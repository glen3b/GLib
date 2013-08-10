using System;
using Glib;
using Glib.XNA;
using Glib.XNA.InputLib;
using Glib.XNA.SpriteLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;

namespace NetworkTest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ScreenManager allScreens;

        AvailableNetworkSessionCollection availableSessions;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Components.Add(new GamerServicesComponent(this));
            Components.Add(new InputManagerComponent(this));
            IsMouseVisible = true;
            base.Initialize();
        }

        SpriteFont font;

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("SpriteFont1");
            
            Screen title = new Screen(spriteBatch, Color.CornflowerBlue);
            TextSprite createSession = new TextSprite(spriteBatch, font, "Host session");
            createSession.IsHoverable = true;
            createSession.NonHoverColor = Color.Black;
            createSession.HoverColor = Color.White;
            createSession.Pressed += new EventHandler(createSession_Pressed);
            TextSprite joinSession = new TextSprite(spriteBatch, font, "Join session");
            joinSession.Y += createSession.Font.LineSpacing+1;
            joinSession.IsHoverable = true;
            joinSession.NonHoverColor = Color.Black;
            joinSession.HoverColor = Color.White;
            joinSession.Pressed += new EventHandler(joinSession_Pressed);
            title.AdditionalSprites.Add(createSession);
            title.AdditionalSprites.Add(joinSession);
            title.Name = "titleScreen";
            title.Visible = true;

            Screen waitForPlayers = new Screen(spriteBatch, Color.CornflowerBlue);
            waitForPlayers.Name = "playerList";

            Screen listSessions = new Screen(spriteBatch, Color.CornflowerBlue);
            listSessions.Name = "listSessions";
            TextSprite refreshSessions = new TextSprite(spriteBatch, font, "Refresh");
            refreshSessions.IsHoverable = true;
            refreshSessions.NonHoverColor = Color.Black;
            refreshSessions.HoverColor = Color.LimeGreen;
            refreshSessions.Pressed += new EventHandler(joinSession_Pressed);
            listSessions.AdditionalSprites.Add(refreshSessions);


            Screen chatScreen = new Screen(spriteBatch, Color.CornflowerBlue);
            TextBoxSprite tbs = new TextBoxSprite(new Vector2(0, GraphicsDevice.Viewport.Height-20), spriteBatch, font);
            tbs.TextSubmitted += new EventHandler(tbs_TextSubmitted);
            chatScreen.Name = "chatScreen";
            chatScreen.Sprites.Add(tbs);

            allScreens = new ScreenManager(spriteBatch, Color.Red, title, waitForPlayers, listSessions, chatScreen);
            // TODO: use this.Content to load your game content here
        }

        void tbs_TextSubmitted(object sender, EventArgs e)
        {
            string sentText = sender.Cast<TextBoxSprite>().RealText;
            PacketWriter dataSender = new PacketWriter();
            dataSender.Write(sentText);
            session.LocalGamers[0].SendData(dataSender, SendDataOptions.ReliableInOrder);
        }

        void joinSession_Pressed(object sender, EventArgs e)
        {
            if (!Guide.IsVisible)
            {
                availableSessions = NetworkSession.Find(
        NetworkSessionType.SystemLink, 2,
        null);
                allScreens["listSessions"].Visible = true;
                allScreens["titleScreen"].Visible = false;
                allScreens["listSessions"].AdditionalSprites.RemoveAll(ts => !(ts is TextSprite) || ts.Cast<TextSprite>().HoverColor != Color.LimeGreen);
                float y = 50;
                float x = 100;
                for (int sessionIndex = 0; sessionIndex < availableSessions.Count; sessionIndex++)
                {
                    AvailableNetworkSession availableSession =
                        availableSessions[sessionIndex];

                    string HostGamerTag = availableSession.HostGamertag;
                    int GamersInSession = availableSession.CurrentGamerCount;
                    SessionInfoDisplay info = new SessionInfoDisplay(spriteBatch, new Vector2(x, y), font, HostGamerTag + ": " + GamersInSession + "/2 gamers", availableSession);
                    info.IsHoverable = true;
                    info.NonHoverColor = Color.Black;
                    info.HoverColor = Color.White;
                    info.Pressed += new EventHandler(info_Pressed);
                    allScreens["listSessions"].AdditionalSprites.Add(info);
                }
            }
        }

        void info_Pressed(object sender, EventArgs e)
        {
            if (!Guide.IsVisible)
            {
                AvailableNetworkSession asession = sender.Cast<SessionInfoDisplay>().Session;
                this.session = NetworkSession.Join(asession);
                session.GamerJoined += new EventHandler<GamerJoinedEventArgs>(session_GamerJoined);
                session.GameStarted += new EventHandler<GameStartedEventArgs>(session_GameStarted);
                Services.AddService(session.GetType(), session);
            }
        }

        void createSession_Pressed(object sender, EventArgs e)
        {
            if (!Guide.IsVisible)
            {

                session = NetworkSession.Create(
        NetworkSessionType.SystemLink,
        maximumLocalPlayers, maximumGamers, privateGamerSlots,
        null);
                session.AllowJoinInProgress = true;
                session.GamerJoined += new EventHandler<GamerJoinedEventArgs>(session_GamerJoined);
                session.GameStarted += new EventHandler<GameStartedEventArgs>(session_GameStarted);
                allScreens["titleScreen"].Visible = false;
                allScreens["playerList"].Visible = true;
                Services.AddService(session.GetType(), session);

                /*
                Texture2D newGamerImage = Texture2D.FromStream(GraphicsDevice, Gamer.SignedInGamers[0].GetProfile().GetGamerPicture());
                Vector2 pos = new Vector2(100, 50);
                Sprite gamerIcon = new Sprite(newGamerImage, pos, spriteBatch);
                allScreens["playerList"].Sprites.Add(gamerIcon);
                TextSprite gamerName = new TextSprite(spriteBatch, new Vector2(pos.X + gamerIcon.Width + 5, pos.Y), font, Gamer.SignedInGamers[0].DisplayName == null ? Gamer.SignedInGamers[0].Gamertag : Gamer.SignedInGamers[0].DisplayName);
                allScreens["playerList"].AdditionalSprites.Add(gamerName);
                */
            }
        }

        void session_GameStarted(object sender, GameStartedEventArgs e)
        {
            foreach (Screen s in allScreens)
            {
                s.Visible = s.Name == "chatScreen";
            }
        }

        void session_GamerJoined(object sender, GamerJoinedEventArgs e)
        {
            Screen playerList = allScreens["playerList"];
            Texture2D newGamerImage = new TextureFactory(GraphicsDevice).CreateSquare(64, Color.Red);
            try
            {
                newGamerImage = Texture2D.FromStream(GraphicsDevice, e.Gamer.GetProfile().GetGamerPicture());
            }
            catch { };
            Vector2 pos = new Vector2(100, 50);
            foreach (Sprite s in playerList.Sprites)
            {
                pos.Y += s.Height + 5;
            }
            Sprite gamerIcon = new Sprite(newGamerImage, pos, spriteBatch);
            playerList.Sprites.Add(gamerIcon);
            TextSprite gamerName = new TextSprite(spriteBatch, new Vector2(pos.X + gamerIcon.Width + 5, pos.Y), font, e.Gamer.Gamertag);
            allScreens["playerList"].AdditionalSprites.Add(gamerName);
            if (session.AllGamers.Count >= 2)
            {
                //TODO
                session.StartGame();
            }
        }

        NetworkSession session;

        int maximumGamers = 2;
        int privateGamerSlots = 1;
        int maximumLocalPlayers = 1;

        bool isAsyncingIn = false;

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        void showSignIn(IAsyncResult res)
        {
            isAsyncingIn = true;
            Guide.EndShowMessageBox(res);
        }

        bool hasSignedIn = false;

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Gamer.SignedInGamers.Count == 0 && !Guide.IsVisible && !hasSignedIn)
            {
                Guide.ShowSignIn(2, false);
                hasSignedIn = true;
            }
            else if (!isAsyncingIn && Gamer.SignedInGamers.Count == 0 && !Guide.IsVisible)
            {
                AsyncCallback showDialog = new AsyncCallback(showSignIn);
                Guide.BeginShowMessageBox("Must be signed in", "This game requires a signed in gamer to play.", new string[]{"OK"}, 0, MessageBoxIcon.Alert, showDialog, Guid.NewGuid());
            }

            if (isAsyncingIn && Gamer.SignedInGamers.Count > 0)
            {
                isAsyncingIn = false;
                
            }
            else if (isAsyncingIn && !Guide.IsVisible)
            {
                Guide.ShowSignIn(2, false);
            }

            allScreens.Update(gameTime);

            // TODO: Add your update logic here

            if (this.session != null)
            {
                session.Update();
            }

            base.Update(gameTime);
        }

        protected override bool BeginDraw()
        {
            allScreens.BeginDraw();
            return base.BeginDraw();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            allScreens.Draw();

            base.Draw(gameTime);
        }

        protected override void EndDraw()
        {
            allScreens.EndDraw();
            base.EndDraw();
        }
    }
}
