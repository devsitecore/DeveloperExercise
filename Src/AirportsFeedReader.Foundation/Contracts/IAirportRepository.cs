// <copyright file="IAirportRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Foundation.Contracts
{
    using System.Collections.Generic;
    using Model;

    public interface IAirportRepository
    {
        IList<Airport> GetAirports();

        DistanceResult CalculateDistance(string source, string destination);
    }
}
