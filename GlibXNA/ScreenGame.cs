using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Glib.XNA.SpriteLib;
using Glib.XNA.InputLib;

namespace Glib.XNA
{
    /// <summary>
    /// A game that uses the <see cref="Glib.XNA.SpriteLib.ScreenManager"/> class.
    /// </summary>
    public abstract class ScreenGame : Game
    {
        /// <summary>
        /// The GraphicsDeviceManager of this game.
        /// </summary>
        protected readonly GraphicsDeviceManager Graphics;


        /// <summary>
        /// Gets or sets the root directory for loading ContentManager assets.
        /// </summary>
        /// <remarks>
        /// Defaults to "Content".
        /// </remarks>
        public string ContentRootDirectory
        {
            get { return Content.RootDirectory; }
            set { Content.RootDirectory = value; }
        }

        /// <summary>
        /// Initializes the game, adding an InputManagerComponent.
        /// </summary>
        protected override void Initialize()
        {
            Components.Add(new InputManagerComponent(this));
            base.Initialize();
        }

        /// <summary>
        /// Gets the background color of this Game.
        /// </summary>
        protected abstract Color BackgroundColor
        {
            get;
        }

        /// <summary>
        /// Initializes all of the Screens of the game.
        /// </summary>
        /// <remarks>
        /// Called immediately after the initialization of the <see cref="Glib.XNA.SpriteLib.ScreenManager"/>, in <seealso cref="LoadContent"/>.
        /// </remarks>
        protected abstract void InitializeScreens();

        /// <summary>
        /// Loads game assets, and initializes the SpriteBatch.
        /// </summary>
        protected override void LoadContent()
        {
            _sb = new SpriteBatch(GraphicsDevice);
            _allScreens = new ScreenManager(_sb, BackgroundColor);
            InitializeScreens();
            base.LoadContent();
        }

        /// <summary>
        /// Updates the visible Screens on this Game.
        /// </summary>
        /// <param name="gameTime">The current GameTime.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            AllScreens.Update(gameTime);
        }

        /// <summary>
        /// Begins drawing the frame.
        /// </summary>
        /// <returns>Whether or not to draw the frame.</returns>
        protected override bool BeginDraw()
        {
            AllScreens.BeginDraw();
            return base.BeginDraw();
        }

        /// <summary>
        /// Ends drawing of the frame.
        /// </summary>
        protected override void EndDraw()
        {
            AllScreens.EndDraw();
            base.EndDraw();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            this.AllScreens.Draw();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Initializes the Game.
        /// </summary>
        public ScreenGame()
            : base()
        {
            Graphics = new GraphicsDeviceManager(this);
            ContentRootDirectory = "Content";
            CoordinateManager = new RelativeCoordinateManager(Graphics.GraphicsDevice.Viewport);

        }

        /// <summary>
        /// Gets or sets the desired height of the Game. Requires a subsequent call to <see cref="ApplyWindowSize"/> to take affect.
        /// </summary>
        protected int Height
        {
            get
            {
                return Graphics.PreferredBackBufferHeight;
            }
            set
            {
                Graphics.PreferredBackBufferHeight = value;
            }
        }

        /// <summary>
        /// Gets or sets the desired width of the Game. Requires a subsequent call to <see cref="ApplyWindowSize"/> to take affect.
        /// </summary>
        protected int Width
        {
            get
            {
                return Graphics.PreferredBackBufferWidth;
            }
            set
            {
                Graphics.PreferredBackBufferWidth = value;
            }
        }

        /// <summary>
        /// Applies any updates to the window size, adjusting the coordinate manager accordingly.
        /// </summary>
        protected void ApplyWindowSize()
        {
            Graphics.ApplyChanges();
            CoordinateManager.SetSize(Width, Height);
        }

        /// <summary>
        /// Gets the internally used coordinate manager instance, which is mapped to the size of the current window.
        /// </summary>
        protected RelativeCoordinateManager CoordinateManager { get; private set; }

        private SpriteBatch _sb;

        /// <summary>
        /// Gets the <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> that is drawn to.
        /// </summary>
        protected SpriteBatch SpriteBatch
        {
            get { return _sb; }

        }


        private ScreenManager _allScreens;

        /// <summary>
        /// Gets the collection of all existing <see cref="Glib.XNA.SpriteLib.Screen"/>s.
        /// </summary>
        protected ScreenManager AllScreens
        {
            get
            {
                return _allScreens;
            }
        }
    }
}
