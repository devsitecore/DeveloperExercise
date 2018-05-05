using AirportsFeedReader.Common.Unity;
using AirportsFeedReader.Foundation.Contracts;
using Unity;

namespace AirportsFeedReader.Tests
{
    using Common.Extensions;

    public class TestsBase
    {
        protected IUnityContainer Container { get; private set; }
        protected IAirportRepository AirportRepository { get; private set; }
        protected ICountryRepository CountryRepository { get; private set; }
        protected ICacheHandler CacheHandler { get; private set; }
        protected ICacheStorage CacheStorage { get; private set; }
        protected IFeedReader FeedReader { get; private set; }

        public TestsBase(bool clearCache = false)
        {
            this.Container = DependencyInjection.Instance().Initialize();
            this.AirportRepository = this.Container.Resolve<IAirportRepository>();
            this.CountryRepository = this.Container.Resolve<ICountryRepository>();
            
            this.CacheHandler = this.Container.Resolve<ICacheHandler>();
            this.CacheStorage = this.Container.Resolve<ICacheStorage>();
            this.FeedReader = this.Container.Resolve<IFeedReader>();
            
            if (clearCache)
            {
                this.CacheStorage.ClearData();
            }
        }
    }
}
