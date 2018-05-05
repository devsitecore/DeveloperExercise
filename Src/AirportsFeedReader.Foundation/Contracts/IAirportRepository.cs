﻿// <copyright file="IAirportRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Foundation.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Model;

    public interface IAirportRepository
    {
        Task<IList<Airport>> GetAirports();
    }
}
