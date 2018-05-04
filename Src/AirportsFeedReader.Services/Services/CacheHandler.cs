namespace AirportsFeedReader.Services.Services
{
    using Foundation.Contracts;
    using System;
    using System.Configuration;
    using Foundation.Model;

    class CacheHandler : ICacheHandler
    {
        private ICacheStorage CacheStorage { get; set; }
        private readonly int DefaultCacheTimeOut = 5;

        protected virtual int CacheTimeOut
        {
            get
            {
                var timeout = DefaultCacheTimeOut;

                var setting = ConfigurationManager.AppSettings["CacheTimeoutMinutes"];

                if (!string.IsNullOrEmpty(setting))
                {
                    int.TryParse(setting, out timeout);
                }

                return timeout;
            }
        }

        public FeedSource FeedSource { get; set; }

        public CacheHandler(ICacheStorage cacheStorage)
        {
            this.CacheStorage = cacheStorage;
        }

        protected virtual bool IsCacheExpired(DateTime cacheDate)
        {
            var expired = false;

            if (cacheDate.AddMinutes(CacheTimeOut) < DateTime.UtcNow)
            {
                expired = true;
            }

            return expired;
        }


        public string GetData()
        {
            var data = string.Empty;
            var result = this.CacheStorage.GetData();

            if (!this.IsCacheExpired(result.CacheDate))
            {
                data = result.Data;
                this.FeedSource = FeedSource.Cache;
            }
            else
            {
                this.CacheStorage.ClearData();
                this.FeedSource = FeedSource.Feed;
            }

            return data;
        }
    }
}
