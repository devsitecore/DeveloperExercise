// <copyright file="FileFeedReader.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Services.Services
{
    using System.IO;
    using System.Threading.Tasks;
    using Common.Extensions;
    using Common.Unity;
    using Foundation.Contracts;
    using Foundation.Model;

    public class FileFeedReader : IFeedReader
    {
        public FileFeedReader(ICacheStorage cacheStorage)
        {
            this.CacheStorage = cacheStorage;
            this.FilterFeedResult = DependencyInjection.Instance().Container.Resolve<IFilterFeedResult>("FilterCountriesFeedResult");
        }

        private ICacheStorage CacheStorage { get; set; }

        private IFilterFeedResult FilterFeedResult { get; set; }

        public async Task<FeedReaderResult> Read(string feedUrl, string cacheKey = "")
        {
            var feedReaderResult = new FeedReaderResult();
            var cacheResult = this.CacheStorage.GetData(cacheKey);
            var data = cacheResult.Data;
            var feedSource = FeedSource.Cache;

            if (string.IsNullOrEmpty(data))
            {
                feedSource = FeedSource.Feed;
                feedUrl = feedUrl.ConvertToApplicationRootPath();

                if (File.Exists(feedUrl))
                {
                    data = await Task.Run(() => File.ReadAllText(feedUrl));
                    data = this.FilterFeedResult.Filter(data);
                }

                this.CacheStorage.SaveDate(data, cacheKey);
            }

            feedReaderResult.FeedSource = feedSource;
            feedReaderResult.Data = data;

            return feedReaderResult;
        }
    }
}
