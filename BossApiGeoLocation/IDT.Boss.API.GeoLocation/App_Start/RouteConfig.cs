// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="">
//
// </copyright>
// <summary>
//   The route config.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IDT.Boss.API.GeoLocation
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// The route config.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// The register routes.
        /// </summary>
        /// <param name="routes">
        /// The routes.
        /// </param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Help Area", string.Empty, new { controller = "Help", action = "Index" }).DataTokens =
                new RouteValueDictionary(new { area = "HelpPage" });
        }
    }
}