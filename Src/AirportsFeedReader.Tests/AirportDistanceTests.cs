using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AirportsFeedReader.Common.Unity;
using AirportsFeedReader.Foundation.Contracts;
using AirportsFeedReader.Common.Extensions;
using Unity;
using AirportsFeedReader.Foundation.Model;

namespace AirportsFeedReader.Tests
{
    [TestClass]
    public class AirportDistanceTests : TestsBase
    {
        public AirportDistanceTests() : base(true)
        {
        }

        [TestMethod]
        public void DistanceBetweenSameAirport()
        {
            var source = new Airport() { Longitude = 41.933334, Latitude = 25.55 };
            var distance = source.Distance(source);

            Assert.AreEqual(distance, 0);
        }

        [TestMethod]
        public void DistanceBetweenHkvAndJam()
        {
            var source = new Airport() { Longitude = 41.933334, Latitude = 25.55 };
            var destination = new Airport() { Longitude = 42.516666, Latitude = 26.483334 };
            var distance = source.Distance(destination);

            Assert.AreNotEqual(distance, 0);
        }

        [TestMethod]
        public void DistanceBetweenSameAirportUsingRepo()
        {
            var data = this.AirportRepository.CalculateDistance("HKV", "HKV");

            Assert.AreEqual(data.Distance, 0);
        }

        [TestMethod]
        public void DistanceBetweenAirportsUsingRepo()
        {
            var data = this.AirportRepository.CalculateDistance("HKV", "JAM");

            Assert.AreNotEqual(data.Distance, 0);
        }

        [TestMethod]
        public void TestDistanceWithNullParam()
        {
            var data = this.AirportRepository.CalculateDistance("HKV", "InvalidIATA");

            Assert.IsTrue(data.Distance == 0);
        }

        [TestMethod]
        public void DistanceBetweenAirportsWithUnitUsingRepo()
        {
            var data = this.AirportRepository.CalculateDistance("HKV", "JAM");

            Assert.IsTrue(data.Distance > 0 && !string.IsNullOrEmpty(data.Unit));
        }
    }
}
