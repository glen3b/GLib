using Glib.XNA.SpriteLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Xna.Framework;

namespace GlibUnitTests
{
    
    
    /// <summary>
    ///This is a test class for SpriteRotationTest and is intended
    ///to contain all SpriteRotationTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SpriteRotationTest
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


        /// <summary>
        ///A test for SpriteRotation Constructor
        ///</summary>
        [TestMethod()]
        public void SpriteRotationConstructorTest0()
        {
            float degrees = 360F; // TODO: Initialize to an appropriate value
            SpriteRotation target = new SpriteRotation(degrees);
            Assert.AreEqual(MathHelper.TwoPi, target.Radians);
            Assert.AreEqual(324F, target.Gradians);
        }

        /// <summary>
        ///A test for SpriteRotation Constructor
        ///</summary>
        [TestMethod()]
        public void SpriteRotationConstructorTest1()
        {
            float value = 0F; // TODO: Initialize to an appropriate value
            AngleType measurementType = new AngleType(); // TODO: Initialize to an appropriate value
            SpriteRotation target = new SpriteRotation(value, measurementType);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for FromDegrees
        ///</summary>
        [TestMethod()]
        public void FromDegreesTest()
        {
            float degrees = 0F; // TODO: Initialize to an appropriate value
            SpriteRotation expected = new SpriteRotation(); // TODO: Initialize to an appropriate value
            SpriteRotation actual;
            actual = SpriteRotation.FromDegrees(degrees);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FromRadians
        ///</summary>
        [TestMethod()]
        public void FromRadiansTest()
        {
            float radians = 0F; // TODO: Initialize to an appropriate value
            SpriteRotation expected = new SpriteRotation(); // TODO: Initialize to an appropriate value
            SpriteRotation actual;
            actual = SpriteRotation.FromRadians(radians);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for op_Addition
        ///</summary>
        [TestMethod()]
        public void op_AdditionTest()
        {
            SpriteRotation x = new SpriteRotation(); // TODO: Initialize to an appropriate value
            SpriteRotation y = new SpriteRotation(); // TODO: Initialize to an appropriate value
            SpriteRotation expected = new SpriteRotation(); // TODO: Initialize to an appropriate value
            SpriteRotation actual;
            actual = (x + y);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for op_Addition
        ///</summary>
        [TestMethod()]
        public void op_AdditionTest1()
        {
            SpriteRotation x = new SpriteRotation(); // TODO: Initialize to an appropriate value
            float y = 0F; // TODO: Initialize to an appropriate value
            SpriteRotation expected = new SpriteRotation(); // TODO: Initialize to an appropriate value
            SpriteRotation actual;
            actual = (x + y);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for op_GreaterThan
        ///</summary>
        [TestMethod()]
        public void op_GreaterThanTest()
        {
            SpriteRotation x = new SpriteRotation(); // TODO: Initialize to an appropriate value
            SpriteRotation y = new SpriteRotation(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = (x > y);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for op_GreaterThanOrEqual
        ///</summary>
        [TestMethod()]
        public void op_GreaterThanOrEqualTest()
        {
            SpriteRotation x = new SpriteRotation(); // TODO: Initialize to an appropriate value
            SpriteRotation y = new SpriteRotation(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = (x >= y);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for op_LessThan
        ///</summary>
        [TestMethod()]
        public void op_LessThanTest()
        {
            SpriteRotation x = new SpriteRotation(); // TODO: Initialize to an appropriate value
            SpriteRotation y = new SpriteRotation(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = (x < y);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for op_LessThanOrEqual
        ///</summary>
        [TestMethod()]
        public void op_LessThanOrEqualTest()
        {
            SpriteRotation x = new SpriteRotation(); // TODO: Initialize to an appropriate value
            SpriteRotation y = new SpriteRotation(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = (x <= y);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for op_Subtraction
        ///</summary>
        [TestMethod()]
        public void op_SubtractionTest()
        {
            SpriteRotation x = new SpriteRotation(); // TODO: Initialize to an appropriate value
            float y = 0F; // TODO: Initialize to an appropriate value
            SpriteRotation expected = new SpriteRotation(); // TODO: Initialize to an appropriate value
            SpriteRotation actual;
            actual = (x - y);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for op_Subtraction
        ///</summary>
        [TestMethod()]
        public void op_SubtractionTest1()
        {
            SpriteRotation x = new SpriteRotation(); // TODO: Initialize to an appropriate value
            SpriteRotation y = new SpriteRotation(); // TODO: Initialize to an appropriate value
            SpriteRotation expected = new SpriteRotation(); // TODO: Initialize to an appropriate value
            SpriteRotation actual;
            actual = (x - y);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Degrees
        ///</summary>
        [TestMethod()]
        public void DegreesTest()
        {
            SpriteRotation target = new SpriteRotation(); // TODO: Initialize to an appropriate value
            float expected = 0F; // TODO: Initialize to an appropriate value
            float actual;
            target.Degrees = expected;
            actual = target.Degrees;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Gradians
        ///</summary>
        [TestMethod()]
        public void GradiansTest()
        {
            SpriteRotation target = new SpriteRotation(); // TODO: Initialize to an appropriate value
            float expected = 0F; // TODO: Initialize to an appropriate value
            float actual;
            target.Gradians = expected;
            actual = target.Gradians;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Radians
        ///</summary>
        [TestMethod()]
        public void RadiansTest()
        {
            SpriteRotation target = new SpriteRotation(); // TODO: Initialize to an appropriate value
            float expected = 0F; // TODO: Initialize to an appropriate value
            float actual;
            target.Radians = expected;
            actual = target.Radians;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Vector
        ///</summary>
        [TestMethod()]
        public void VectorTest()
        {
            SpriteRotation target = new SpriteRotation(); // TODO: Initialize to an appropriate value
            Vector2 expected = new Vector2(); // TODO: Initialize to an appropriate value
            Vector2 actual;
            target.Vector = expected;
            actual = target.Vector;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Zero
        ///</summary>
        [TestMethod()]
        public void ZeroTest()
        {
            SpriteRotation actual;
            actual = SpriteRotation.Zero;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
