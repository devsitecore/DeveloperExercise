using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirportsFeedReader.Tests
{
    [TestClass]
    public class AirportRepositoryTests : TestsBase
    {
        public AirportRepositoryTests() : base(true)
        {

        }

        [TestMethod]
        public void TestAirportList()
        {
            var list = this.AirportRepository.GetAirports();
            var list2 = this.AirportRepository.GetAirports();
            Assert.IsTrue(list.Count > 0 && list2.Count > 0);
        }
    }
}
