// <copyright file="FilterFeedResult.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Services.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Foundation.Contracts;
    using Foundation.Extensions;
    using Foundation.Model;

    public class FilterFeedResult : IFilterFeedResult
    {
        private readonly string europeContinent = "EU";
        private readonly string dataType = "airport";

        public string Filter(string feedResult)
        {
            var airports = feedResult.ToModelList<IList<Airport>>();

            if (airports != null)
            {
                airports = airports.Where(airport => airport.Continent.Equals(this.europeContinent) && airport.Type.Equals(this.dataType) && !string.IsNullOrEmpty(airport.Name))
                    .Select(airport => airport).ToArray();

                feedResult = airports.ToJson();
            }

            return feedResult;
        }
    }
}
