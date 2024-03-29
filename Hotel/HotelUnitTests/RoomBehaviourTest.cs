﻿using Hotel;
using Hotel.Persons;
using Hotel.Rooms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelUnitTests
{
    class RoomBehaviourTest
    { 
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
        public void CafeArrivalBehaviourTest()
        {
            Cafe cafe = new Cafe(1, new Microsoft.Xna.Framework.Point(0, 0), new Microsoft.Xna.Framework.Point(1, 1), 4);
            GuestRoom room = new GuestRoom(2, new Microsoft.Xna.Framework.Point(1, 1), new Microsoft.Xna.Framework.Point(1, 1), 4);
            Guest guest = new Guest(cafe);
            guest.Room = room;

            cafe.RoomBehaviour.OnArrival(cafe, guest);

            Assert.IsTrue(cafe.PeopleCount == 1);
        }

        [TestMethod]
        public void CafeDepartureBehaviourTest()
        {
            Cafe cafe = new Cafe(1, new Microsoft.Xna.Framework.Point(0, 0), new Microsoft.Xna.Framework.Point(1, 1), 4);
            GuestRoom room = new GuestRoom(2, new Microsoft.Xna.Framework.Point(1, 1), new Microsoft.Xna.Framework.Point(1, 1), 4);
            Guest guest = new Guest(cafe);
            guest.Room = room;

            cafe.RoomBehaviour.OnArrival(cafe, guest);

            cafe.RoomBehaviour.OnDeparture(cafe, guest);
            Assert.IsTrue(cafe.PeopleCount == 0);
        }

        [TestMethod]
        public void CafeFindNextCafe()
        {
            Cafe cafe = new Cafe(1, new Microsoft.Xna.Framework.Point(0, 0), new Microsoft.Xna.Framework.Point(1, 1), 0);
            Cafe cafe2 = new Cafe(1, new Microsoft.Xna.Framework.Point(0, 0), new Microsoft.Xna.Framework.Point(1, 1), 1);
            GuestRoom room = new GuestRoom(2, new Microsoft.Xna.Framework.Point(1, 1), new Microsoft.Xna.Framework.Point(1, 1), 4);

            room.Neighbors.Add(Direction.East, cafe);
            cafe.Neighbors.Add(Direction.West, room);

            room.Neighbors.Add(Direction.West, cafe2);
            cafe2.Neighbors.Add(Direction.East, room);

            Guest guest = new Guest(cafe);
            guest.Room = room;

            cafe.RoomBehaviour.OnArrival(cafe, guest);

            Assert.ReferenceEquals(guest.TargetRoom, cafe2);
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
