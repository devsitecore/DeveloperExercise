// <copyright file="AirportRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Services.Repositories
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Threading.Tasks;
    using Foundation.Contracts;
    using Foundation.Extensions;
    using Foundation.Model;

    public class AirportRepository : IAirportRepository
    {
        private readonly string feedUrl = ConfigurationManager.AppSettings["AirportsFeed"];

        public AirportRepository(IFeedReader feedReader)
        {
            this.FeedReader = feedReader;
        }

        private IFeedReader FeedReader { get; set; }

        public async Task<IList<Airport>> GetAirports()
        {
            var feedResult = await this.FeedReader.Read(this.feedUrl);
            var airports = feedResult.Data.ToModelList<IList<Airport>>();

            return airports;
        }
    }
}