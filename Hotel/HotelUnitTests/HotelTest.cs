using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hotel;
using Hotel.Rooms;
using Microsoft.Xna.Framework;

namespace HotelUnitTests
{
    /// <summary>
    /// Summary description for HotelTest
    /// </summary>
    [TestClass]
    public class HotelTest
    {
        List<Room> rooms = new List<Room>();

        public HotelTest()
        {
            if (ServiceLocator.Get<ConfigLoader>() == null)
                ServiceLocator.Add<ConfigLoader>(new ConfigLoader(""));

            rooms = new List<Room>();
            rooms.Add(new EmptyRoom(-1, new Point(-1, 0), new Point(1, 1)));
            (rooms[0] as EmptyRoom).Entrance = true;
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
                    rooms.Add(new GuestRoom(j * i + 9, new Point(i, j), new Point(1, 1), 1));

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
        public void HotelConstructor()
        {
            Hotel.Hotel hotel = new Hotel.Hotel();
            Assert.IsNotNull(hotel);
        }

        [TestMethod]
        public void AddStaffTest()
        {
            Hotel.Hotel hotel = new Hotel.Hotel();
            hotel.Rooms = rooms;
            hotel.CreateStaff();

            // Created a receptionist.
            Assert.IsTrue(hotel.Staff.Count == 1);
        }

        [TestMethod]
        public void AddGuestsTest()
        {
            Hotel.Hotel hotel = new Hotel.Hotel();
            hotel.Rooms = rooms;
            hotel.AddGuest("John", 1);

            Assert.IsTrue(hotel.Guests.Count == 1);
        }
    }
}
