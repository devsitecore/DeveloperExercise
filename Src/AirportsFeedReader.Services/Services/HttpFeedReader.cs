using AirportsFeedReader.Foundation.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportsFeedReader.Foundation.Model;
using System.Net.Http;

namespace AirportsFeedReader.Services.Services
{
    public class HttpFeedReader : IFeedReader
    {
        private ICacheHandler CacheHandler { get; set; }
        private ICacheStorage CacheStorage { get; set; }
        private IFilterFeedResult FilterFeedResult { get; set; }
        
        public HttpFeedReader(ICacheHandler cacheHandler, ICacheStorage cacheStorage, IFilterFeedResult filterFeedResult)
        {
            this.CacheStorage = cacheStorage;
            this.CacheHandler = cacheHandler;
            this.FilterFeedResult = filterFeedResult;
        }

        public async Task<FeedReaderResult> Read(string feedUrl)
        {
            var feedReaderResult = new FeedReaderResult();
            var data = this.CacheHandler.GetData();
            var feedSource = FeedSource.Cache;

            if (string.IsNullOrEmpty(data))
            {
                feedSource = FeedSource.Feed;

                var httpClient = new HttpClient();
                var feedData = await httpClient.GetAsync(feedUrl);
                data = await feedData.Content.ReadAsStringAsync();

                data = this.FilterFeedResult.Filter(data);

                this.CacheStorage.SaveDate(data);
            }

            feedReaderResult.FeedSource = feedSource;
            feedReaderResult.Data = data;

            return feedReaderResult;
        }
    }
}
