namespace AirportsFeedReader.Tests
{
    using Common.Extensions;
    using Foundation.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Configuration;

    [TestClass]
    public class FeedReaderTests : TestsBase
    {
        private readonly string feedUrl = ConfigurationManager.AppSettings["AirportsFeed"];

        public FeedReaderTests() : base(true)
        {
        }

        [TestMethod]
        public void TestHttpReadingFromCache()
        {
            var firstRead = this.FeedReader.Read(feedUrl);
            var secondRead = this.FeedReader.Read(feedUrl);

            Assert.IsTrue(firstRead.FeedSource == FeedSource.Feed && secondRead.FeedSource == FeedSource.Cache);
        }

        [TestMethod]
        public void TestHttpReadingData()
        {
            var readData = this.FeedReader.Read(feedUrl);
            Assert.IsTrue(readData.Data.Length > 0);
        }
    }
}
