using Glib.XNA;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace GlibUnitTests
{
    
    
    /// <summary>
    ///This is a test class for TextureFactoryTest and is intended
    ///to contain all TextureFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TextureFactoryTest
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

        private SpriteBatch MockDrawable;

        [TestInitialize()]
        public void Init()
        {
            MockDrawable = TestGame.GetSpriteBatch();
        }

        [TestCleanup()]
        public void Teardown()
        {
            MockDrawable.Dispose();
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


        /// <summary>
        ///A test for TextureFactory Constructor
        ///</summary>
        [TestMethod()]
        public void TextureFactoryConstructorTest()
        {
            GraphicsDevice device = MockDrawable.GraphicsDevice;
            TextureFactory target = new TextureFactory(device);
            Assert.IsNotNull(target.WhitePixel);
        }

        /// <summary>
        ///A test for CreateRectangle
        ///</summary>
        [TestMethod()]
        public void CreateRectangleTest()
        {
            GraphicsDevice device = MockDrawable.GraphicsDevice; // TODO: Initialize to an appropriate value
            TextureFactory target = new TextureFactory(device); // TODO: Initialize to an appropriate value
            int width = 43; // TODO: Initialize to an appropriate value
            int height = 91; // TODO: Initialize to an appropriate value
            Texture2D actual = target.CreateRectangle(width, height);
            Color[] resultingData = new Color[width * height];
            actual.GetData<Color>(resultingData);
            Color[] expected = Enumerable.Repeat(Color.White, width*height).ToArray();
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], resultingData[i]);
            }
        }

        /// <summary>
        ///A test for CreateRectangle
        ///</summary>
        [TestMethod()]
        public void CreateRectangleTest1()
        {
            GraphicsDevice device = MockDrawable.GraphicsDevice;
            TextureFactory target = new TextureFactory(device);
            int width = 543;
            int height = 1239;
            Color color = new Color(134, 234, 65.4f, 100); // TODO: Initialize to an appropriate value
            Texture2D actual;
            actual = target.CreateRectangle(width, height, color);
            Color[] resultingData = new Color[width * height];
            actual.GetData<Color>(resultingData);
            Color[] expectedData = Enumerable.Repeat<Color>(color, width * height).ToArray();
            for (int i = 0; i < expectedData.Length; i++)
            {
                Assert.AreEqual(expectedData[i], resultingData[i]);
            }
        }

        /// <summary>
        ///A test for CreateSquare
        ///</summary>
        [TestMethod()]
        public void CreateSquareTest()
        {
            GraphicsDevice device = MockDrawable.GraphicsDevice; // TODO: Initialize to an appropriate value
            TextureFactory target = new TextureFactory(device); // TODO: Initialize to an appropriate value
            int size = 4; // TODO: Initialize to an appropriate value
            Color[] actualData = new Color[size * size];
            Texture2D actual;
            actual = target.CreateSquare(size);
            actual.GetData(actualData);
            Color[] expected = Enumerable.Repeat(Color.White, size * size).ToArray();
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actualData[i]);
            }
        }

        /// <summary>
        ///A test for CreateSquare
        ///</summary>
        [TestMethod()]
        public void CreateSquareTest1()
        {
            Assert.Inconclusive("TODO: Write this test.");
            GraphicsDevice device = null; // TODO: Initialize to an appropriate value
            TextureFactory target = new TextureFactory(device); // TODO: Initialize to an appropriate value
            int size = 0; // TODO: Initialize to an appropriate value
            Color color = new Color(); // TODO: Initialize to an appropriate value
            Texture2D expected = null; // TODO: Initialize to an appropriate value
            Texture2D actual;
            actual = target.CreateSquare(size, color);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateTexture
        ///</summary>
        [TestMethod()]
        public void CreateTextureTest()
        {
            GraphicsDevice device = MockDrawable.GraphicsDevice; // TODO: Initialize to an appropriate value
            Assert.Inconclusive("Write this this test method.");
            TextureFactory target = new TextureFactory(device); // TODO: Initialize to an appropriate value
            int width = 0; // TODO: Initialize to an appropriate value
            int height = 0; // TODO: Initialize to an appropriate value
            Func<Point, Color> colorDetermine = null; // TODO: Initialize to an appropriate value
            Texture2D expected = null; // TODO: Initialize to an appropriate value
            Texture2D actual;
            actual = target.CreateTexture(width, height, colorDetermine);
            Assert.AreEqual(expected, actual);
            
        }

        /// <summary>
        ///A test for WhitePixel
        ///</summary>
        [TestMethod()]
        public void WhitePixelTest()
        {
            GraphicsDevice device = MockDrawable.GraphicsDevice; // TODO: Initialize to an appropriate value
            TextureFactory target = new TextureFactory(device); // TODO: Initialize to an appropriate value
            Texture2D actual;
            actual = target.WhitePixel;
            Color[] data = new Color[] { Color.Black };
            actual.GetData(data);
            Assert.AreEqual(Color.White, data[0]);
        }
    }
}
