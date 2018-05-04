// <copyright file="ICacheHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Foundation.Contracts
{
    using Model;

    public interface ICacheHandler
    {
        FeedSource FeedSource { get; set; }

        string GetData();
    }
}
