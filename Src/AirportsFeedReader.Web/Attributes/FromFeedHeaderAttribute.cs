// <copyright file="FromFeedHeaderAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Web
{
    using System.Web.Mvc;
    using Common.Extensions;
    using Common.Unity;
    using Foundation.Contracts;
    using Foundation.Model;

    public class FromFeedHeaderAttribute : ActionFilterAttribute
    {
        private readonly string headerName = "from-feed";

        public FromFeedHeaderAttribute()
        {
            var cacheHandler = DependencyInjection.Instance().Container.Resolve<ICacheHandler>();
            this.Init(cacheHandler);
        }

        public FromFeedHeaderAttribute(ICacheHandler cacheHandler)
        {
            this.Init(cacheHandler);
        }

        private ICacheHandler CacheHandler { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cacheData = this.CacheHandler.GetData();

            var feedSource = FeedSource.Cache;

            if (string.IsNullOrEmpty(cacheData))
            {
                feedSource = FeedSource.Feed;
            }

            context.HttpContext.Response.Headers.Add(this.headerName, feedSource.ToString());
        }

        protected void Init(ICacheHandler cacheHandler)
        {
            this.CacheHandler = cacheHandler;
        }
    }
}