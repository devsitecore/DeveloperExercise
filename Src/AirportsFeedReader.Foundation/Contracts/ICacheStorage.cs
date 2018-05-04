// <copyright file="ICacheStorage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Foundation.Contracts
{
    using Model;

    public interface ICacheStorage
    {
        CacheResult<string> GetData();

        bool SaveDate(string data);

        void Initialize();

        void ClearData();
    }
}
