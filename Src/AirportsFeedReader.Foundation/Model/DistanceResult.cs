// <copyright file="Airport.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Foundation.Model
{
    using System;
    using System.Device.Location;
    using System.Runtime.Serialization;

    [DataContract]
    [Serializable]
    public class DistanceResult
    {
        public DistanceResult()
        {
            this.Unit = "Kilometer";
        }

        [DataMember]
        public double Distance { get; set; }

        [DataMember]
        public string Unit { get; set; }
    }
}
