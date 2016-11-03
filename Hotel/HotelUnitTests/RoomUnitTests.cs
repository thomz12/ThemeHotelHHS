using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hotel;
using Hotel.Rooms;
using Hotel.Persons;

namespace HotelUnitTests
{
    /// <summary>
    /// Summary description for RoomUnitTests
    /// </summary>
    [TestClass]
    public class RoomUnitTests
    {
        public RoomUnitTests()
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
        public void RemoveGuestFromLobbyQueue()
        {
            Lobby lobby = new Lobby(1, new Microsoft.Xna.Framework.Point(0, 0), new Microsoft.Xna.Framework.Point(1, 1));
            GuestRoom room = new GuestRoom(2, new Microsoft.Xna.Framework.Point(1, 0), new Microsoft.Xna.Framework.Point(1, 1), 1);

            lobby.Neighbors.Add(Direction.East, room);
            room.Neighbors.Add(Direction.West, lobby);

            Receptionist receptionist = new Receptionist(lobby, new List<Room>() { lobby, room });

            Guest guest = new Guest(lobby);
            guest.Classification = 1;

            lobby.CheckIn(guest);

            lobby.RemoveFromQueues(guest);

            for (int i = 0; i < 10; i++)
            {
                receptionist.Update(1);
            }

            Assert.IsTrue(guest.Room == null);
        }

        [TestMethod]
        public void AddGuestToCheckinLobby()
        {
            Lobby lobby = new Lobby(1, new Microsoft.Xna.Framework.Point(0, 0), new Microsoft.Xna.Framework.Point(1, 1));
            GuestRoom room = new GuestRoom(2, new Microsoft.Xna.Framework.Point(1, 0), new Microsoft.Xna.Framework.Point(1, 1), 1);

            lobby.Neighbors.Add(Direction.East, room);
            room.Neighbors.Add(Direction.West, lobby);

            Receptionist receptionist = new Receptionist(lobby, new List<Room>() { lobby, room });

            Guest guest = new Guest(lobby);
            guest.Classification = 1;

            lobby.CheckIn(guest);

            for (int i = 0; i < 10; i++)
            {
                receptionist.Update(1);
            }

            Assert.IsTrue(guest.Room != null);
        }

        [TestMethod]
        public void AddGuestToCheckOut()
        {
            Lobby lobby = new Lobby(1, new Microsoft.Xna.Framework.Point(0, 0), new Microsoft.Xna.Framework.Point(1, 1));
            GuestRoom room = new GuestRoom(2, new Microsoft.Xna.Framework.Point(1, 0), new Microsoft.Xna.Framework.Point(1, 1), 1);

            lobby.Neighbors.Add(Direction.East, room);
            room.Neighbors.Add(Direction.West, lobby);

            Receptionist receptionist = new Receptionist(lobby, new List<Room>() { lobby, room });

            Guest guest = new Guest(lobby);
            guest.Classification = 1;

            lobby.CheckIn(guest);

            for (int i = 0; i < 10; i++)
            {
                receptionist.Update(1);
            }

            guest.FindAndTargetRoom(x => x is Lobby && (x as Lobby).Receptionist != null);
            guest.StayState = StayState.CheckOut;

            for (int i = 0; i < 10; i++)
            {
                guest.Update(1);
                receptionist.Update(1);
            }

            Assert.IsTrue(guest.Room == null);
        }

        [TestMethod]
        public void EndMovieCinemaTest()
        {
            ServiceLocator.Get<ConfigLoader>().GetConfig().FilmDuration = 10;
            Cinema cinema = new Cinema(0, new Microsoft.Xna.Framework.Point(0, 0), new Microsoft.Xna.Framework.Point(1, 1));
            cinema.StartMovie();

            for (int i = 0; i < 15; i++)
                cinema.Update(1);

            // Cinema should be open when movie has ended.
            Assert.IsTrue(cinema.Open);
        }

        [TestMethod]
        public void StartCinemaTest()
        {
            Cinema cinema = new Cinema(0, new Microsoft.Xna.Framework.Point(0, 0), new Microsoft.Xna.Framework.Point(1, 1));
            cinema.StartMovie();

            // Cinema should be closed when movie is started.
            Assert.IsTrue(!cinema.Open);
        }

        [TestMethod]
        public void RoomNeighbourNeighbor()
        {
            Room guestroom00 = new GuestRoom(1, new Microsoft.Xna.Framework.Point(0, 0), new Microsoft.Xna.Framework.Point(1, 1), 1);
            Room guestroom10 = new GuestRoom(2, new Microsoft.Xna.Framework.Point(1, 0), new Microsoft.Xna.Framework.Point(1, 1), 1);

            Direction dir = guestroom00.IsNeighbor(guestroom10);

            Assert.IsTrue(dir == Direction.East);
        }

        [TestMethod]
        public void RoomNeighbourNotNeighbor()
        {
            Room guestroom00 = new GuestRoom(1, new Microsoft.Xna.Framework.Point(0, 0), new Microsoft.Xna.Framework.Point(1, 1), 1);
            Room guestroom01 = new GuestRoom(2, new Microsoft.Xna.Framework.Point(0, 1), new Microsoft.Xna.Framework.Point(1, 1), 1);

            Direction dir = guestroom00.IsNeighbor(guestroom01);

            Assert.IsTrue(dir == Direction.None);
        }

        [TestMethod]
        public void GuestRoomConstructor()
        {
            GuestRoom room = new GuestRoom(1, new Microsoft.Xna.Framework.Point(0, 0), new Microsoft.Xna.Framework.Point(1, 1), 2);
            Assert.IsNotNull(room);
        }

        [TestMethod]
        public void GuestRoomDirty()
        {
            GuestRoom room = new GuestRoom(1, new Microsoft.Xna.Framework.Point(0, 0), new Microsoft.Xna.Framework.Point(1, 1), 2);
            room.State = RoomState.Dirty;
            Assert.IsTrue(room.State == RoomState.Dirty);
        }

        [TestMethod]
        public void CafeConstructor()
        {
            Cafe room = new Cafe(1, new Microsoft.Xna.Framework.Point(0, 0), new Microsoft.Xna.Framework.Point(1, 1), 6);
            Assert.IsNotNull(room);
        }

        [TestMethod]
        public void UpdateSpriteTestGuestRoom()
        {
            GuestRoom room = new GuestRoom(1, new Microsoft.Xna.Framework.Point(1, 1), new Microsoft.Xna.Framework.Point(1, 1), 1);
            room.Update(1);
            Assert.IsNull(room.Sprite.Texture);
        }

        [TestMethod]
        public void CinemaConstructor()
        {
            Cinema room = new Cinema(1, new Microsoft.Xna.Framework.Point(0, 0), new Microsoft.Xna.Framework.Point(1, 1));
            Assert.IsNotNull(room);
        }

        [TestMethod]
        public void FitnessConstructor()
        {
            Fitness room = new Fitness(1, new Microsoft.Xna.Framework.Point(0, 0), new Microsoft.Xna.Framework.Point(1, 1));
            Assert.IsNotNull(room);
        }

        [TestMethod]
        public void LobbyConstructor()
        {
            Lobby room = new Lobby(1, new Microsoft.Xna.Framework.Point(0, 0), new Microsoft.Xna.Framework.Point(1, 1));
            Assert.IsNotNull(room);
        }

        [TestMethod]
        public void StaircaseConstructor()
        {
            Staircase room = new Staircase(1, new Microsoft.Xna.Framework.Point(0, 0));
            Assert.IsNotNull(room);
        }

        [TestMethod]
        public void PoolConstructor()
        {
            Pool room = new Pool(1, new Microsoft.Xna.Framework.Point(0, 0), new Microsoft.Xna.Framework.Point(1, 1));
            Assert.IsNotNull(room);
        }

        [TestMethod]
        public void ElevatorShaftConstructor()
        {
            ElevatorShaft room = new ElevatorShaft(1, new Microsoft.Xna.Framework.Point(0, 0));
            Assert.IsNotNull(room);
        }

        [TestMethod]
        public void EmptyRoomConstructor()
        {
            EmptyRoom room = new EmptyRoom(1, new Microsoft.Xna.Framework.Point(0, 0), new Microsoft.Xna.Framework.Point(1, 1));
            Assert.IsNotNull(room);
        }
    }
}
