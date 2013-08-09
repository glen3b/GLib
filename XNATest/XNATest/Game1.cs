using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Glib;
using Glib.XNA;
using Glib.XNA.SpriteLib;
using Glib.XNA.InputLib;

namespace XNATest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TextSprite menuTxt;
        TextBoxSprite name;
        Sprite spr;
        Screen menu;
        Screen menuTwo;

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
            IsMouseVisible = true;
            Components.Add(new InputManagerComponent(this));
            base.Initialize();
        }

        ScreenManager menus;

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spr = new Sprite(Content.Load<Texture2D>("Jellyfish"), Vector2.Zero, spriteBatch);
            spr.UseCenterAsOrigin = false;
            spr.Scale = new Vector2(.175f);

            ProgressBar progBar = new ProgressBar(new Vector2(55), Color.White, Color.Black, spriteBatch);
            progBar.Scale = new Vector2(.25f);
            progBar.Denominator = 1024;
            progBar.WidthScale = 2;
            progBar.HeightScale = 5;
            progBar.ProgressBarFilled += new EventHandler(progBar_ProgressBarFilled);
            progBar.Value = 0;
            progBar.Updated += new EventHandler(progBar_Updated);
            

            menuTxt = new TextSprite(spriteBatch, new Vector2(0), Content.Load<SpriteFont>("SpriteFont1"), "Hello world!", Color.Black);
            menuTxt.IsHoverable = true;
            menuTxt.IsManuallySelectable = true;
            menuTxt.IsSelected = true;
            menuTxt.Pressed += new EventHandler(menuTxt_Clicked);
            name = new TextBoxSprite(new Vector2(0, 50), spriteBatch, Content.Load<SpriteFont>("SpriteFont1"));
            name.IsPassword = true;
            name.Focused = true;
            name.Width = 500;
            name.TextSubmitted += new EventHandler(name_TextSubmitted);
            menuTxtTwo = new TextSprite(spriteBatch, new Vector2(250,0), Content.Load<SpriteFont>("SpriteFont1"), "Selectable", Color.Black);
            menuTxtTwo.IsHoverable = true;
            menuTxtTwo.IsManuallySelectable = true;
            //menuTxt.Clicked += new EventHandler(menuTxt_Clicked);
            //menuTxt.HoverColor = Color.Red;

            KeyboardManager.KeyDown += new SingleKeyEventHandler(KeyboardManager_KeyDown);

            menu = new Screen(new SpriteManager(spriteBatch, progBar, name), Color.Red);
            menu.AdditionalSprites.Add(menuTxt);
            menu.AdditionalSprites.Add(menuTxtTwo);
            menu.Visible = true;

            menuTwo = new Screen(new SpriteManager(spriteBatch, spr), Color.Gray);

            menus = new ScreenManager(spriteBatch, Color.Pink, menu, menuTwo);
            // TODO: use this.Content to load your game content here
        }

        void name_TextSubmitted(object sender, EventArgs e)
        {
            
        }

        void progBar_ProgressBarFilled(object sender, EventArgs e)
        {
            menu.Visible = false;
            menuTwo.Visible = false;
        }

        ProgressBar pb;

        void progBar_Updated(object sender, EventArgs e)
        {
            pb = sender.Cast<ProgressBar>();

            pb.Value += pb.Value == pb.Denominator ? 0 : 1;
        }

        TextSprite menuTxtTwo;

        void KeyboardManager_KeyDown(object source, SingleKeyEventArgs e)
        {
            if (e.Key == Keys.Left || e.Key == Keys.Right)
            {
                menuTxt.IsSelected = !menuTxt.IsSelected;
                menuTxtTwo.IsSelected = !menuTxtTwo.IsSelected;
            }
        }

        void menuTxt_Clicked(object sender, EventArgs e)
        {
            sender.Cast<TextSprite>().Text += "!";
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

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

            menuTwo.Visible = KeyboardManager.State.IsKeyDown(Keys.LeftAlt);
            menus.Update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override bool BeginDraw()
        {
            menus.BeginDraw();
            return base.BeginDraw();
        }

        protected override void  EndDraw()
{
    menus.EndDraw();
 	 base.EndDraw();
}

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            /*
            spriteBatch.Begin();
            menuTxt.Draw();
            spr.DrawNonAuto();
            spriteBatch.End();
             */
            menus.Draw();
            base.Draw(gameTime);
        }
    }
}
