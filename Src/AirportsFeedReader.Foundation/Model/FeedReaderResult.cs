// <copyright file="FeedReaderResult.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Foundation.Model
{
    public class FeedReaderResult
    {
        public virtual string Data { get; set; }

        public virtual FeedSource FeedSource { get; set; }
    }
}
