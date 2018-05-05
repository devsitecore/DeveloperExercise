// <copyright file="IFeedReader.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Foundation.Contracts
{
    using Model;

    public interface IFeedReader
    {
        FeedReaderResult Read(string feedUrl, string cacheKey = "");
    }
}
