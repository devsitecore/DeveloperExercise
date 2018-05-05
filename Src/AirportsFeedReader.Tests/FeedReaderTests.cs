using AirportsFeedReader.Common.Unity;
using AirportsFeedReader.Foundation.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

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
