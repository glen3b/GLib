using Glib.XNA.SpriteLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GlibUnitTests
{
    
    
    /// <summary>
    ///This is a test class for VelocityTest and is intended
    ///to contain all VelocityTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VelocityTest
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
        ///A test for Velocity Constructor
        ///</summary>
        [TestMethod()]
        public void VelocityConstructorTestSomeNull()
        {
            float YSpeed = 5F;
            Velocity target = new Velocity();
            target.YVelocity = YSpeed;
            Assert.IsNull(target.XVelocity);
            Assert.Equals(target.YVelocity, YSpeed);
        }

        /// <summary>
        ///A test for Velocity Constructor
        ///</summary>
        [TestMethod()]
        public void VelocityConstructorTestNoNull()
        {
            float XSpeed = 2423.53489F;
            float YSpeed = -35.48F;
            Velocity target = new Velocity(XSpeed, YSpeed);
            Assert.Equals(target.XVelocity, XSpeed);
            Assert.Equals(target.YVelocity, YSpeed);
        }

        /// <summary>
        ///A test for Velocity Constructor
        ///</summary>
        [TestMethod()]
        public void VelocityConstructorTestAllNull()
        {
            Velocity target = new Velocity();
            Assert.IsNull(target.XVelocity);
            Assert.IsNull(target.YVelocity);
        }
    }
}
