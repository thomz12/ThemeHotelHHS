using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hotel;
using Hotel.Rooms;
using Microsoft.Xna.Framework;
using Hotel.Persons;

namespace HotelUnitTests
{
    [TestClass]
    public class InformationWindowTests
    {
        [TestMethod]
        public void InformationWindowConstructor()
        {
            InformationWindow window = new InformationWindow();
            Assert.IsNotNull(window);
        }

        [TestMethod]
        public void ShowInfo()
        {
            GuestRoom aGuestRoom = new GuestRoom(0, new Point(1, 1), new Point(1, 1), 1);
            InformationWindow window = new InformationWindow();
            window.ShowInformation(aGuestRoom);
            Assert.IsTrue(window.IsShowingInfo);
        }

        [TestMethod]
        public void HideInfo()
        {
            GuestRoom aGuestRoom = new GuestRoom(0, new Point(1, 1), new Point(1, 1), 1);
            InformationWindow window = new InformationWindow();
            window.ShowInformation(aGuestRoom);
            window.HideInformation();
            Assert.IsFalse(window.IsShowingInfo);
        }

        [TestMethod]
        public void Update()
        {
            GuestRoom aGuestRoom = new GuestRoom(0, new Point(1, 1), new Point(1, 1), 1);
            InformationWindow window = new InformationWindow();
            window.ShowInformation(aGuestRoom);
            window.Update(1);
            Assert.IsTrue(window.IsShowingInfo);
        }
    }
}
