using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hotel;

namespace HotelUnitTests
{
    [TestClass]
    public class EventHandlerTests
    {
        [TestMethod]
        public void EventHandlerConstructor()
        {
            Hotel.Hotel hotel = new Hotel.Hotel();
            HotelEventHandler handler = new HotelEventHandler(hotel);
            Assert.IsNotNull(handler);
        }
    }
}
