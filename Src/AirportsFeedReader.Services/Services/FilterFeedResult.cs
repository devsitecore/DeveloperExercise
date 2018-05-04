using AirportsFeedReader.Foundation.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using AirportsFeedReader.Foundation.Model;
using System.IO;
using AirportsFeedReader.Foundation.Extensions;

namespace AirportsFeedReader.Services.Services
{
    public class FilterFeedResult : IFilterFeedResult
    {
        private readonly string Continent = "EU";
        private readonly string DataType = "airport";

        public string Filter(string feedResult)
        {
            var airports = feedResult.ToAirports();

            if (airports != null)
            {
                airports = airports.Where(location => location.Continent.Equals(Continent) && location.Type.Equals(DataType) && !string.IsNullOrEmpty(location.Name))
                    .Select(airport => airport).ToArray();

                feedResult = airports.ToJson();
            }

            return feedResult;
        }
    }
}
