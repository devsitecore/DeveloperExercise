using AirportsFeedReader.Foundation.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirportsFeedReader.Tests
{
    [TestClass]
    public class CountryRepositoryTests : TestsBase
    {
        public CountryRepositoryTests() : base(true)
        {
        }

        [TestMethod]
        public void TestCountryList()
        {
            var list = this.CountryRepository.GetCountries();
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod]
        public void TestCountryDataWithCache()
        {
            var list = this.CountryRepository.GetCountries();
            var list2 = this.CountryRepository.GetCountries();
            Assert.IsTrue(list.Count > 0 && list.Count == list2.Count);
        }

        [TestMethod]
        public void TestCountryData()
        {
            var coutnry = new Country() { Name = "A", CountryCode = "00", Region = "R" };

            Assert.IsTrue(coutnry.Name == "A" && coutnry.CountryCode == "00" && coutnry.Region == "R");
        }
    }
}
