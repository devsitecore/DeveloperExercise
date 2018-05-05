﻿// <copyright file="HttpFeedReader.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Services.Services
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Foundation.Contracts;
    using Foundation.Model;

    public class HttpFeedReader : IFeedReader
    {
        public HttpFeedReader(ICacheHandler cacheHandler, ICacheStorage cacheStorage, IFilterFeedResult filterFeedResult)
        {
            this.CacheStorage = cacheStorage;
            this.CacheHandler = cacheHandler;
            this.FilterFeedResult = filterFeedResult;
        }

        private ICacheHandler CacheHandler { get; set; }

        private ICacheStorage CacheStorage { get; set; }

        private IFilterFeedResult FilterFeedResult { get; set; }

        public async Task<FeedReaderResult> Read(string feedUrl, string cacheKey = "")
        {
            var feedReaderResult = new FeedReaderResult();
            var data = this.CacheHandler.GetData(cacheKey);
            var feedSource = FeedSource.Cache;

            if (string.IsNullOrEmpty(data))
            {
                feedSource = FeedSource.Feed;

                var httpClient = new HttpClient();
                var feedData = await httpClient.GetAsync(feedUrl);
                data = await feedData.Content.ReadAsStringAsync();

                data = this.FilterFeedResult.Filter(data);

                this.CacheStorage.SaveDate(data, cacheKey);
            }

            feedReaderResult.FeedSource = feedSource;
            feedReaderResult.Data = data;

            return feedReaderResult;
        }
    }
}
