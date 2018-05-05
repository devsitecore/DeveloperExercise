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
    public class Airport
    {
        [DataMember(Name= "iata")]
        public string Iata { get; set; }

        [DataMember(Name="lon")]
        public double Longitude { get; set; }

        [DataMember(Name = "iso")]
        public string Iso { get; set; }

        [DataMember(Name = "status")]
        public int Status { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "continent")]
        public string Continent { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "lat")]
        public double Latitude { get; set; }

        [DataMember(Name = "size")]
        public string Size { get; set; }

        public double Distance(Airport destinatin)
        {
            double distance = 0;

            var sourceCoord = new GeoCoordinate(this.Latitude, this.Longitude);
            var destinationCoord = new GeoCoordinate(destinatin.Latitude, destinatin.Longitude);

            distance = sourceCoord.GetDistanceTo(destinationCoord);

            return distance;
        }
    }
}
