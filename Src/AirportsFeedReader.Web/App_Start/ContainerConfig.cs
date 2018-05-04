namespace AirportsFeedReader.Web
{
    using Common.Unity;
    using System.Web.Mvc;
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
