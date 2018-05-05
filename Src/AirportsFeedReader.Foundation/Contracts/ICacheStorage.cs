// <copyright file="ICacheStorage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Foundation.Contracts
{
    using Model;

    public interface ICacheStorage
    {
        CacheResult<string> GetData(string cacheKey = "");

        bool SaveDate(string data, string cacheKey = "");

        void Initialize();

        void ClearData(string cacheKey = "");
    }
}
