using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hotel.Persons;
using Hotel;

namespace HotelUnitTests
{
    /// <summary>
    /// Summary description for PersonTest
    /// </summary>
    [TestClass]
    public class PersonTest
    {
        public PersonTest()
        {
            if (ServiceLocator.Get<ConfigLoader>() == null)
                ServiceLocator.Add<ConfigLoader>(new ConfigLoader(""));
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GuestConstructor()
        {
            GuestRoom room = new GuestRoom(1, new Microsoft.Xna.Framework.Point(0, 0), new Microsoft.Xna.Framework.Point(1, 1), 1);

            Guest guest = new Guest(room);
            Assert.IsNotNull(guest);
        }

        [TestMethod]
        public void CleanerConstructor()
        {
            GuestRoom room = new GuestRoom(1, new Microsoft.Xna.Framework.Point(0, 0), new Microsoft.Xna.Framework.Point(1, 1), 1);

            Cleaner cleaner = new Cleaner(room);
            Assert.IsNotNull(cleaner);
        }


        [TestMethod]
        public void ReceptionistConstructor()
        {
            GuestRoom room = new GuestRoom(1, new Microsoft.Xna.Framework.Point(0, 0), new Microsoft.Xna.Framework.Point(1, 1), 1);

            Receptionist receptionist = new Receptionist(room, new List<Room>() { room });
            Assert.IsNotNull(receptionist);
        }
    }
}
