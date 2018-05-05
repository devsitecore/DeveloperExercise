// <copyright file="Country.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Foundation.Model
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    [Serializable]
    public class Country
    {
        [DataMember(Name= "name")]
        public string Name { get; set; }

        [DataMember(Name="iso2")]
        public string CountryCode { get; set; }

        [DataMember(Name = "region")]
        public string Region { get; set; }
    }
}
