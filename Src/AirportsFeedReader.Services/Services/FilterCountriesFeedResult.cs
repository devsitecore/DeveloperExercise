// <copyright file="FilterCountriesFeedResult.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Services.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Extensions;
    using Common.Extensions;
    using Foundation.Contracts;
    using Foundation.Model;

    public class FilterCountriesFeedResult : IFilterFeedResult
    {
        private readonly string europeRegion = "Europe";

        public string Filter(string feedResult)
        {
            var modelArray = feedResult.ToModelList<IList<Country>>();

            if (modelArray != null)
            {
                modelArray = modelArray.Where(country => country.Region.Equals(this.europeRegion))
                    .Select(country => country).ToArray();

                feedResult = modelArray.ToJson();
            }

            return feedResult;
        }
    }
}
