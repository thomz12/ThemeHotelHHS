using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hotel.RoomsFactory;
using Hotel;
using System.Collections.Generic;
using Hotel.Rooms;
using Microsoft.Xna.Framework;

namespace HotelUnitTests
{
    [TestClass]
    public class RoomFactoryTest
    {
        [TestMethod]
        public void FactoryConstructor()
        {
            RoomFactory rf = new RoomFactory();
            Assert.IsNotNull(rf);
        }

        [TestMethod]
        public void GuestRoomBuilding()
        {
            HotelBuilder hb = new HotelBuilder();
            hb.RoomFactory.RegisterComponent("Room", new GuestRoomFactoryComponent());

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("ID", "1");
            data.Add("AreaType", "Room");
            data.Add("Position", "1,1");
            data.Add("Dimension", "1,1");
            data.Add("Classification", "1 Star");

            Assert.IsTrue(hb.RoomFactory.BuildRoom(data) is GuestRoom);
        }

        [TestMethod]
        public void CinemaBuilding()
        {
            HotelBuilder hb = new HotelBuilder();
            hb.RoomFactory.RegisterComponent("Cinema", new CinemaFactoryComponent());

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("ID", "1");
            data.Add("AreaType", "Cinema");
            data.Add("Position", "2,2");
            data.Add("Dimension", "2,2");

            Assert.IsTrue(hb.RoomFactory.BuildRoom(data) is Cinema);
        }

        [TestMethod]
        public void RestaurantBuilding()
        {
            HotelBuilder hb = new HotelBuilder();
            hb.RoomFactory.RegisterComponent("Restaurant", new CafeFactoryComponent());

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("ID", "1");
            data.Add("AreaType", "Restaurant");
            data.Add("Position", "1,1");
            data.Add("Dimension", "1,2");
            data.Add("Capacity", "4");

            Assert.IsTrue(hb.RoomFactory.BuildRoom(data) is Cafe);
        }

        [TestMethod]
        public void EmptyBuilding()
        {
            HotelBuilder hb = new HotelBuilder();
            hb.RoomFactory.RegisterComponent("Empty", new EmptyRoomFactoryComponent());

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("ID", "1");
            data.Add("AreaType", "Empty");
            data.Add("Position", "1,1");
            data.Add("Dimension", "1,1");

            Assert.IsTrue(hb.RoomFactory.BuildRoom(data) is EmptyRoom);
        }

        [TestMethod]
        public void FitnessBuilding()
        {
            HotelBuilder hb = new HotelBuilder();
            hb.RoomFactory.RegisterComponent("Fitness", new FitnessFactoryComponent());

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("ID", "1");
            data.Add("AreaType", "Fitness");
            data.Add("Position", "1,1");
            data.Add("Dimension", "2,2");

            Assert.IsTrue(hb.RoomFactory.BuildRoom(data) is Fitness);
        }

        [TestMethod]
        public void PoolBuilding()
        {
            HotelBuilder hb = new HotelBuilder();
            hb.RoomFactory.RegisterComponent("Pool", new PoolFactoryComponent());

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("ID", "1");
            data.Add("AreaType", "Pool");
            data.Add("Position", "1,1");
            data.Add("Dimension", "2,2");

            Assert.IsTrue(hb.RoomFactory.BuildRoom(data) is Pool);
        }

        [TestMethod]
        public void BuildRoomOverload()
        {
            HotelBuilder hb = new HotelBuilder();
            hb.RoomFactory.RegisterComponent("Empty", new EmptyRoomFactoryComponent());
            Assert.IsTrue(hb.RoomFactory.BuildRoom(-1, "Empty", new Point(0, 0), new Point(1, 1)) is EmptyRoom);
        }

        [TestMethod]
        public void PoolRoomOverload()
        {
            HotelBuilder hb = new HotelBuilder();
            hb.RoomFactory.RegisterComponent("Pool", new PoolFactoryComponent());
            Assert.IsTrue(hb.RoomFactory.BuildRoom(-1, "Pool", new Point(0, 0), new Point(1, 1)) is Pool);
        }

        [TestMethod]
        public void RunFailedBuild()
        {
            HotelBuilder hb = new HotelBuilder();
            Assert.IsTrue(hb.RoomFactory.BuildRoom(-1, "kaas", new Point(0, 0), new Point(1, 1)) == null);
        }
    }
}
