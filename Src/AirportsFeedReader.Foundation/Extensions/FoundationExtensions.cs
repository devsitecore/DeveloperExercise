// <copyright file="FoundationExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Foundation.Extensions
{
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using Model;

    public static class FoundationExtensions
    {
        public static string ToJson(this Airport[] airports)
        {
            var serializer = new DataContractJsonSerializer(typeof(Airport[]));

            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, airports);
                var jsonData = Encoding.UTF8.GetString(ms.ToArray());

                return jsonData;
            }
        }

        public static Airport[] ToAirports(this string data)
        {
            var serializer = new DataContractJsonSerializer(typeof(Airport[]));

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
            {
                var airports = (Airport[])serializer.ReadObject(stream);
                return airports;
            }
        }
    }
}
