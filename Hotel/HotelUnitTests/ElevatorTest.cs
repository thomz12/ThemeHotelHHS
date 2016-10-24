using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hotel;

namespace HotelUnitTests
{
    /// <summary>
    /// Summary description for ElevatorTest
    /// </summary>
    [TestClass]
    public class ElevatorTest
    {
        public ElevatorTest()
        {
            if(ServiceLocator.Get<ConfigLoader>() == null)
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

        [TestInitialize]
        public void InitTest()
        {
        }

        [TestMethod]
        public void ElevatorConstructor()
        {
            Elevator elevator = new Elevator();
            Assert.IsNotNull(elevator);
        }

        [TestMethod]
        public void CallElevatorFrom0To1()
        {
            Elevator elevator = new Elevator();
            elevator.CallElevator(0, 1);
            elevator.Update(10);
            elevator.Update(10);
            elevator.Update(10);
            Assert.IsTrue(elevator.CurrentFloor == 1);
       }

        [TestMethod]
        public void CallElevatorFrom1To0()
        {
            Elevator elevator = new Elevator();
            elevator.CallElevator(1, 0);
            elevator.Update(10);
            elevator.Update(10);
            elevator.Update(10);
            elevator.Update(10);
            elevator.Update(10);
            elevator.Update(10);
            Assert.IsTrue(elevator.CurrentFloor == 0);
        }
    }
}
