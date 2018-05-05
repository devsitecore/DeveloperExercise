// <copyright file="CountryRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Services.Repositories
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Threading.Tasks;
    using Common.Extensions;
    using Common.Unity;
    using Foundation.Contracts;
    using Foundation.Extensions;
    using Foundation.Model;

    public class CountryRepository : ICountryRepository
    {
        private readonly string countriesFileFeedPath = ConfigurationManager.AppSettings["CountriesFileFeedPath"];

        public CountryRepository()
        {
            this.Init();
        }

        private IFeedReader FileFeedReader { get; set; }

        public IList<Country> GetCountries()
        {
            var feedResult = this.FileFeedReader.Read(this.countriesFileFeedPath, "CountriesCacheData");
            var countries = feedResult.Data.ToModelList<IList<Country>>();

            return countries;
        }

        public virtual void Init(IFeedReader feedReader = null)
        {
            this.FileFeedReader = feedReader != null ? feedReader : DependencyInjection.Instance().Container.Resolve<IFeedReader>("LocalFileFeedReader");
        }
    }
}