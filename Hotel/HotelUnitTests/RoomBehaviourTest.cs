using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hotel.Persons;
using Hotel.Rooms;
using Hotel;

namespace HotelUnitTests
{
    /// <summary>
    /// Summary description for RoomBehaviour
    /// </summary>
    [TestClass]
    public class RoomBehaviourTest
    {
        public RoomBehaviourTest()
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
        public void CafeBehaviourTest()
        {
            Cafe cafe = new Cafe(1, new Microsoft.Xna.Framework.Point(0, 0), new Microsoft.Xna.Framework.Point(1, 1), 4);
            GuestRoom room = new GuestRoom(2, new Microsoft.Xna.Framework.Point(1, 1), new Microsoft.Xna.Framework.Point(1, 1), 4);
            Guest guest = new Guest(cafe);
            guest.Room = room;

            cafe.RoomBehaviour.OnArrival(cafe, guest);

            Assert.IsTrue(cafe.PeopleCount == 1);
        }

        [TestMethod]
        public void CinemaBehaviourTest()
        {
            Cinema cinema = new Cinema(1, new Microsoft.Xna.Framework.Point(0, 0), new Microsoft.Xna.Framework.Point(1, 1));
            GuestRoom room = new GuestRoom(2, new Microsoft.Xna.Framework.Point(1, 1), new Microsoft.Xna.Framework.Point(1, 1), 4);
            Guest guest = new Guest(cinema);
            guest.Room = room;

            cinema.Update(10);
            cinema.Update(10);

            cinema.RoomBehaviour.OnArrival(cinema, guest);
            Assert.IsTrue(cinema.PeopleCount == 1);
        }

        [TestMethod]
        public void ElevatorshaftPass()
        {
            ElevatorShaft elevatorShaft = new ElevatorShaft(1, new Microsoft.Xna.Framework.Point(0, 0));
            ElevatorShaft elevatorShaft2 = new ElevatorShaft(1, new Microsoft.Xna.Framework.Point(0, 1));
            elevatorShaft.Neighbors.Add(Direction.North, elevatorShaft);
            elevatorShaft2.Neighbors.Add(Direction.South, elevatorShaft2);

            GuestRoom room = new GuestRoom(2, new Microsoft.Xna.Framework.Point(1, 1), new Microsoft.Xna.Framework.Point(1, 1), 4);
            Guest guest = new Guest(elevatorShaft);
            guest.Room = room;

            guest.Path.Clear();
            guest.Path.Add(elevatorShaft);
            guest.Path.Add(elevatorShaft2);

            elevatorShaft.RoomBehaviour.OnPassRoom(elevatorShaft, guest);
            Assert.IsTrue(guest.CalledElevator);
        }
    }
}
