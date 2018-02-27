using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace IDT.Boss.API.GeoLocation
{
    using System.Net.Http.Formatting;

    using Newtonsoft.Json.Serialization;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            var jsonFormatter = config.Formatters.JsonFormatter;
            if (jsonFormatter != null)
            {
                jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                jsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            }

            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}
