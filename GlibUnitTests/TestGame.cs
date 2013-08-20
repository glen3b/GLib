using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GlibUnitTests
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    class TestGame : Microsoft.Xna.Framework.Game
    {
        internal GraphicsDeviceManager graphics;
        internal SpriteBatch spriteBatch;

        private static SpriteBatch initedSb = null;

        public static SpriteBatch GetSpriteBatch()
        {
            initedSb = null;
            TestGame gameToTest = new TestGame();
            gameToTest.Drawn += new EventHandler(gameToTest_Drawn);
            gameToTest.Run();
            while (initedSb == null)
            {

            }
            return initedSb;
        }

        private static void gameToTest_Drawn(object sender, EventArgs e)
        {
            initedSb = ((TestGame)sender).spriteBatch;
            ((Game)sender).Exit();
        }

        private TestGame()
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
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public event EventHandler Drawn;

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }

        protected override void EndDraw()
        {
            if (Drawn != null)
            {
                Drawn(this, EventArgs.Empty);
            }
            base.EndDraw();
        }
    }

}
