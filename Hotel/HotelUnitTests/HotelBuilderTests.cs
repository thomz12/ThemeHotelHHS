using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hotel;
using Hotel.RoomsFactory;
using System.Collections.Generic;
using Hotel.Rooms;
using Microsoft.Xna.Framework;

namespace HotelUnitTests
{
    [TestClass]
    public class HotelBuilderTests
    {
        [TestMethod]
        public void HotelBuilderConstructor()
        {
            HotelBuilder hb = new HotelBuilder();
            Assert.IsNotNull(hb);
        }

        [TestMethod]
        public void BuildHotel()
        {
            ServiceLocator.Remove<ConfigLoader>();

            // This test requires a config file to work.
            // Make sure there is a config file present!
            ServiceLocator.Add<ConfigLoader>(new ConfigLoader(@"C:\Users\Thom\Source\Repos\ThemeHotelHHS\Hotel Simulator Debug\Config.cfg"));
            HotelBuilder hb = new HotelBuilder();

            // Register components to the roomfactory
            hb.RoomFactory.RegisterComponent("Room", new GuestRoomFactoryComponent());
            hb.RoomFactory.RegisterComponent("Cinema", new CinemaFactoryComponent());
            hb.RoomFactory.RegisterComponent("Restaurant", new CafeFactoryComponent());
            hb.RoomFactory.RegisterComponent("Empty", new EmptyRoomFactoryComponent());
            hb.RoomFactory.RegisterComponent("Lobby", new LobbyFactoryComponent());
            hb.RoomFactory.RegisterComponent("ElevatorShaft", new ElevatorShaftFactoryComponent());
            hb.RoomFactory.RegisterComponent("Staircase", new StaircaseFactoryComponent());
            hb.RoomFactory.RegisterComponent("Fitness", new FitnessFactoryComponent());
            hb.RoomFactory.RegisterComponent("Pool", new PoolFactoryComponent());

            List<Room> hotel = hb.BuildHotel();
            Assert.IsNotNull(hotel);
        }
    }
}
