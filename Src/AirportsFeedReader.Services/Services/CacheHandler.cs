// <copyright file="CacheHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Services.Services
{
    using System;
    using System.Configuration;
    using Foundation.Contracts;
    using Foundation.Model;

    public class CacheHandler : ICacheHandler
    {
        private readonly int defaultCacheTimeOut = 5;
        private readonly string cacheTimeoutMinutesKey = "CacheTimeoutMinutes";

        public CacheHandler(ICacheStorage cacheStorage)
        {
            this.CacheStorage = cacheStorage;
        }

        public FeedSource FeedSource { get; set; }

        protected virtual int CacheTimeOut
        {
            get
            {
                var timeout = this.defaultCacheTimeOut;

                var setting = ConfigurationManager.AppSettings[this.cacheTimeoutMinutesKey];

                if (!string.IsNullOrEmpty(setting))
                {
                    int.TryParse(setting, out timeout);
                }

                return timeout;
            }
        }

        private ICacheStorage CacheStorage { get; set; }

        public string GetData(string cacheKey = "")
        {
            var data = string.Empty;
            var result = this.CacheStorage.GetData(cacheKey);

            if (!this.IsCacheExpired(result.CacheDate))
            {
                data = result.Data;
                this.FeedSource = FeedSource.Cache;
            }
            else
            {
                this.CacheStorage.ClearData(cacheKey);
                this.FeedSource = FeedSource.Feed;
            }

            return data;
        }

        protected virtual bool IsCacheExpired(DateTime cacheDate)
        {
            var expired = false;

            if (cacheDate.AddMinutes(this.CacheTimeOut) < DateTime.UtcNow)
            {
                expired = true;
            }

            return expired;
        }
    }
}
