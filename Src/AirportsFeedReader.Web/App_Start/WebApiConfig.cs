
namespace AirportsFeedReader.Web
{
    using Common.Unity;
    using Common.Extensions;
    using Foundation.Contracts;
    using System.Web.Http;
    using System.Web.Mvc;
    using Foundation.Model;

    public class FromFeedHeaderAttribute : ActionFilterAttribute
    {
        private ICacheHandler CacheHandler;
        private readonly string HeaderName = "from-feed";

        public FromFeedHeaderAttribute()
        {
            var cacheHandler = DependencyInjection.Instance().Container.Resolve<ICacheHandler>();
            this.Init(cacheHandler);
        }

        public FromFeedHeaderAttribute(ICacheHandler cacheHandler)
        {
            this.Init(cacheHandler);
        }

        protected virtual void Init(ICacheHandler cacheHandler)
        {
            this.CacheHandler = cacheHandler;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cacheData = this.CacheHandler.GetData();

            var feedSource = FeedSource.Cache;

            if (string.IsNullOrEmpty(cacheData))
            {
                feedSource = FeedSource.Feed;
            }

            context.HttpContext.Response.Headers.Add(this.HeaderName, feedSource.ToString());
        }
    }

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
