namespace AirportsFeedReader.Web
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Unity;

    public class ControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// The container
        /// </summary>
        private readonly IUnityContainer Container;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerFactory"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public ControllerFactory(IUnityContainer container)
        {
            this.Container = container;
        }

        /// <summary>
        /// Retrieves the controller instance for the specified request context and controller type.
        /// </summary>
        /// <param name="requestContext">The context of the HTTP request, which includes the HTTP context and route data.</param>
        /// <param name="controllerType">The type of the controller.</param>
        /// <returns>
        /// The controller instance.
        /// </returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return this.Container.Resolve(controllerType) as IController;
        }
    }
}
