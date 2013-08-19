using Glib.XNA.SpriteLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Xna.Framework.Graphics;
using Glib.XNA;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GlibUnitTests
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    class TestGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        internal SpriteBatch spriteBatch;

        public TestGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        internal void Init()
        {
            Initialize();
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


        protected override bool BeginDraw()
        {
            return false;
        }

        protected override void EndDraw()
        {
            return;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            return;
        }
    }

    /// <summary>
    ///This is a test class for SpriteWrapperTest and is intended
    ///to contain all SpriteWrapperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SpriteWrapperTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        TestGame mocker;

        [TestInitialize]
        public void InitClass()
        {
            /*
            GraphicsAdapter.UseReferenceDevice = true;
            System.Windows.Forms.Form f = new System.Windows.Forms.Form();

            Mocked = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.Reach, new PresentationParameters() { DeviceWindowHandle = f.Handle });


            //Mocker = new MockedGraphicsDeviceService();
            //(Mocker.CreateDevice() as GraphicsDeviceKeeper).Die();
            //Mocker.CreateDevice();
            */
            mocker = new TestGame();
            mocker.Init();
        }

        [TestCleanup]
        public void DestroyClass()
        {
            mocker.Dispose();
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        private class UnitTestSprite : ISprite
        {
            public void Draw()
            {
                IsDrawn = true;
            }

            public bool IsDrawn = false;
            public bool IsUpdated = false;

            public void Update()
            {
                IsUpdated = true;
            }
        }

        private class UnitTestUpdater : Updater
        {
            public UnitTestUpdater(SpriteWrapper sw)
                : base(sw)
            {

            }

            public int UpdatedSprites = 0;

            public IEnumerable<ISprite> AllSprites
            {
                get
                {
                    return this.GetAllSprites();
                }
            }

            public override void UpdateSprite(ISprite updating)
            {
                UpdatedSprites++;
            }
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void UpdateTest()
        {


            //SpriteBatch sb = new SpriteBatch(new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, new PresentationParameters() { DeviceWindowHandle = GraphicsAdapter.DefaultAdapter.MonitorHandle } ));
            SpriteBatch sb = mocker.spriteBatch;
            UnitTestSprite[] spritestart = new UnitTestSprite[25];
            
            //Initialize array
            for (int i = 0; i < spritestart.Length; i++)
            {
                spritestart[i] = new UnitTestSprite();
                Assert.IsFalse(spritestart[i].IsUpdated);
            }
            SpriteWrapper target = new SpriteWrapper(sb, spritestart);
            UnitTestUpdater updater = new UnitTestUpdater(target);
            Assert.AreEqual(target.Sprites, updater.AllSprites);
            target.Updater = updater;
            target.Update();
            Assert.AreEqual(updater.UpdatedSprites, spritestart.Length);
            for (int i = 0; i < spritestart.Length; i++)
            {
                Assert.IsTrue(spritestart[i].IsUpdated);
                Assert.IsFalse(spritestart[i].IsDrawn);
            }
        }

        /// <summary>
        ///A test for SpriteWrapper Constructor
        ///</summary>
        [TestMethod()]
        public void SpriteWrapperConstructorTest()
        {
            ICollection<ISprite> spritestart = new ISprite[0]; // TODO: Initialize to an appropriate value
            SpriteBatch sb = mocker.spriteBatch; // TODO: Initialize to an appropriate value
            Updater updates = null; // TODO: Initialize to an appropriate value
            SpriteWrapper target = new SpriteWrapper(spritestart, sb, updates);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for SpriteWrapper Constructor
        ///</summary>
        [TestMethod()]
        public void SpriteWrapperConstructorTest1()
        {
            SpriteBatch sb = null; // TODO: Initialize to an appropriate value
            ISprite[] spritestart = new ISprite[0]; // TODO: Initialize to an appropriate value
            SpriteWrapper target = null;
            try
            {
                target = new SpriteWrapper(sb, spritestart);
            }
            catch (ArgumentNullException) { }

            Assert.IsNull(target, "The target constructor did not throw an ArgumentNullException when it should have.");
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void UpdateTest1()
        {
            Assert.Inconclusive("TODO");
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void UpdateTest2()
        {
            Assert.Inconclusive("TODO");
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest()
        {
            Assert.Inconclusive("TODO");
        }

        /// <summary>
        ///A test for Sprites
        ///</summary>
        [TestMethod()]
        public void SpritesTest()
        {
            Assert.Inconclusive("TODO");
        }

        /// <summary>
        ///A test for Updater
        ///</summary>
        [TestMethod()]
        public void UpdaterTest()
        {
            //SpriteBatch sb = new SpriteBatch(new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, new PresentationParameters() { DeviceWindowHandle = GraphicsAdapter.DefaultAdapter.MonitorHandle } ));
            SpriteBatch sb = mocker.spriteBatch; //TODO Fail this
            UnitTestSprite[] spritestart = new UnitTestSprite[25];

            //Initialize array
            for (int i = 0; i < spritestart.Length; i++)
            {
                spritestart[i] = new UnitTestSprite();
                Assert.IsFalse(spritestart[i].IsUpdated);
            }
            SpriteWrapper target = new SpriteWrapper(sb, spritestart);
            UnitTestUpdater updater = new UnitTestUpdater(target);
            Assert.AreEqual(target.Sprites, updater.AllSprites);
            target.Update();
            Assert.AreEqual(updater.UpdatedSprites, spritestart.Length);
            for (int i = 0; i < spritestart.Length; i++)
            {
                Assert.IsTrue(spritestart[i].IsUpdated);
                Assert.IsFalse(spritestart[i].IsDrawn);
            }
        }
    }
}
