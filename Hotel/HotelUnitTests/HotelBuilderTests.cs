using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hotel;
using Hotel.RoomsFactory;
using System.Collections.Generic;

namespace HotelUnitTests
{
    [TestClass]
    public class HotelBuilderTests
    {
        public static HotelBuilder HotelBuilder;

        public HotelBuilderTests()
        {
            if (ServiceLocator.Get<ConfigLoader>() == null)
                ServiceLocator.Add<ConfigLoader>(new ConfigLoader(""));
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
            HotelBuilder = new HotelBuilder();
        }

        [TestMethod]
        public void HotelBuilderConstructor()
        {
            HotelBuilder hb = new HotelBuilder();
            Assert.IsNotNull(hb);
        }

        [TestMethod]
        public void HotelBuilderFactoryConstructor()
        {
            RoomFactory rf = new RoomFactory();
            Assert.IsNotNull(rf);
        }

        [TestMethod]
        public void FactoryRegisterComponent()
        {
            IRoomFactoryComponent rfc = new GuestRoomFactoryComponent();
            HotelBuilder.RoomFactory.RegisterComponent("Room", rfc);
            /*
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("")

            HotelBuilder.RoomFactory.BuildRoom();

            Assert.IsTrue();
            */
        }
    }
}
