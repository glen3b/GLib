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
            SpriteBatch sb = null; //TODO Fail this
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
            ICollection<ISprite> spritestart = null; // TODO: Initialize to an appropriate value
            SpriteBatch sb = null; // TODO: Initialize to an appropriate value
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
            Assert.Inconclusive("TODO");
        }
    }
}
