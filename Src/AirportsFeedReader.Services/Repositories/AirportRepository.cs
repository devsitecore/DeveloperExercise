// <copyright file="AirportRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Services.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
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

        public virtual DistanceResult CalculateDistance(string source, string destination)
        {
            var airports = this.GetAirports();
            var result = new DistanceResult();

            var sourceAirport = airports.Where(airport => airport.Iata == source).Select(airport => airport).FirstOrDefault<Airport>();
            var destinationAirport = airports.Where(airport => airport.Iata == destination).Select(airport => airport).FirstOrDefault<Airport>();

            if (sourceAirport != null && destinationAirport != null)
            {
                var distance = sourceAirport.Distance(destinationAirport);

                // Convert to Kilometers
                distance = distance / 1000;

                result.Distance = Math.Round(distance, 2);
            }

            return result;
        }

        public virtual IList<Airport> GetAirports()
        {
            var feedResult = this.FeedReader.Read(this.feedUrl);
            var airports = feedResult.Data.ToModelList<IList<Airport>>();

            return airports;
        }
    }
}