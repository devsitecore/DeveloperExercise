// <copyright file="UnityExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Common.Extensions
{
    using global::Unity;

    public static class UnityExtensions
    {
        /// <summary>
        /// Resolves the specified container.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="container">The container.</param>
        /// <param name="name">The type name.</param>
        /// <returns>Resolved Type Object</returns>
        public static T Resolve<T>(this UnityContainer container, string name = "")
        {
            var returnObject = container.Resolve(typeof(T), name, null);

            if (!(returnObject is T))
            {
                returnObject = null;
            }

            return (T)returnObject;
        }
    }
}
