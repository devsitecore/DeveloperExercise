// <copyright file="CacheResult.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Foundation.Model
{
    using System;

    public class CacheResult<T>
    {
        public DateTime CacheDate { get; set; }

        public T Data { get; set; }
    }
}
