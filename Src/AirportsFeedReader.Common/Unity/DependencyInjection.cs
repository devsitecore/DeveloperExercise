// <copyright file="DependencyInjection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Common.Unity
{
    using global::Unity;
    using Microsoft.Practices.Unity.Configuration;

    public class DependencyInjection
    {
        private static DependencyInjection dependencyInjectionInstance;

        /// <summary>
        /// Prevents a default instance of the <see cref="DependencyInjection"/> class from being created.
        /// </summary>
        private DependencyInjection()
        {
        }

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public virtual UnityContainer Container { get; protected set; }

        /// <summary>
        /// Instances this instance.
        /// </summary>
        /// <returns>Container</returns>
        public static DependencyInjection Instance() => dependencyInjectionInstance != null ? dependencyInjectionInstance : dependencyInjectionInstance = new DependencyInjection();

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>UnityContainer</returns>
        public UnityContainer Initialize()
        {
            var container = new UnityContainer();
            container.LoadConfiguration();
            return this.Container = container;
        }
    }
}
