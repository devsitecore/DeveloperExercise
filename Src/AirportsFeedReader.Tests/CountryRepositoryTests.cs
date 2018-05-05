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
            var list2 = this.CountryRepository.GetCountries();
            Assert.IsTrue(list.Count > 0 && list2.Count > 0);
        }
    }
}
