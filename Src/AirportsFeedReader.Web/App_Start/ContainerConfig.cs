// <copyright file="ContainerConfig.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Web
{
    using System.Web.Mvc;
    using Common.Unity;
    using Unity;

    public static class ContainerConfig
    {
        public static void Initialize()
        {
            var container = DependencyInjection.Instance().Initialize();
            SetControllerFactory(container);
        }

        /// <summary>
        /// Sets the controller factory.
        /// </summary>
        /// <param name="container">The container.</param>
        private static void SetControllerFactory(IUnityContainer container)
        {
            var controllerFactory = new ControllerFactory(container);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}
