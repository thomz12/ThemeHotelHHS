using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hotel.Persons;
using Hotel;
using Hotel.Rooms;
using Microsoft.Xna.Framework;

namespace HotelUnitTests
{
    /// <summary>
    /// Summary description for PersonTest
    /// </summary>
    [TestClass]
    public class PersonTest
    {

        public List<Room> rooms;
        public PersonTest()
        {
            if (ServiceLocator.Get<ConfigLoader>() == null)
                ServiceLocator.Add<ConfigLoader>(new ConfigLoader(""));

            if (rooms != null)
                return;

            rooms = new List<Room>();
            rooms.Add(new ElevatorShaft(0, new Point(0, 0)));
            rooms.Add(new ElevatorShaft(1, new Point(0, 1)));
            rooms.Add(new ElevatorShaft(2, new Point(0, 2)));
            rooms.Add(new Lobby(3, new Point(1, 0), new Point(1, 1)));
            rooms.Add(new Lobby(4, new Point(2, 0), new Point(1, 1)));
            rooms.Add(new Lobby(5, new Point(3, 0), new Point(1, 1)));
            rooms.Add(new Lobby(6, new Point(4, 0), new Point(1, 1)));
            rooms.Add(new Staircase(7, new Point(5, 0)));
            rooms.Add(new Staircase(8, new Point(5, 1)));
            rooms.Add(new Staircase(9, new Point(5, 2)));
            for (int i = 1; i < 5; i++)
                for (int j = 1; j < 3; j++)
                    rooms.Add(new GuestRoom(j * (i  + 1) + 9, new Point(i, j), new Point(1, 1), 1));

            HotelBuilder builder = new HotelBuilder();
            foreach (Room r in rooms)
                builder.PlaceRoom(r);
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
        public void PersonMove()
        {
            Guest guest = new Guest(rooms[0]);
            // TODO: IDK
            //guest.Evacuating = true;
            guest.Sprite.DrawDestination = new Rectangle((int)guest.Position.X, (int)guest.Position.Y, 25, 50);
            guest.FindAndTargetRoom(x => x == rooms[16]);

            for (int i = 0; i < 100; i++)
                guest.Update(1);

            Assert.IsTrue(guest.CurrentRoom == rooms[16]);
        }


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
