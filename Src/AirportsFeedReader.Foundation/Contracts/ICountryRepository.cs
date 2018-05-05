// <copyright file="ICountryRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Foundation.Contracts
{
    using System.Collections.Generic;
    using Model;

    public interface ICountryRepository
    {
        IList<Country> GetCountries();

        void Init(IFeedReader feedReader = null);
    }
}
