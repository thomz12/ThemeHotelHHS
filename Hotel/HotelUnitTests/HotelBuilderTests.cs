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
        public HotelBuilderTests()
        {

        }

        // Sets the test context which provides information about the functionality for the current test run.
        private TestContext testContextInstance;
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

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            
        }

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
            ServiceLocator.Add<ConfigLoader>(new ConfigLoader(@"C:\Users\Daan\Source\Repos\ThemeHotelHHS\Hotel Simulator Debug\Config.cfg"));
            HotelBuilder hb = new HotelBuilder();
            hb.RoomFactory.RegisterComponent("Room", new GuestRoomFactoryComponent());
            hb.RoomFactory.RegisterComponent("Cinema", new CinemaFactoryComponent());
            hb.RoomFactory.RegisterComponent("Restaurant", new CafeFactoryComponent());
            hb.RoomFactory.RegisterComponent("Empty", new EmptyRoomFactoryComponent());
            hb.RoomFactory.RegisterComponent("Fitness", new FitnessFactoryComponent());
            hb.RoomFactory.RegisterComponent("Pool", new PoolFactoryComponent());
            List<Room> hotel = hb.BuildHotel();
            Assert.IsNotNull(hotel);
        }
    }
}
