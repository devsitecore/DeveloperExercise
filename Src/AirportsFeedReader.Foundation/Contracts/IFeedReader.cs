// <copyright file="IFeedReader.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Foundation.Contracts
{
    using System.Threading.Tasks;
    using Model;

    public interface IFeedReader
    {
        Task<FeedReaderResult> Read(string feedUrl);
    }
}
