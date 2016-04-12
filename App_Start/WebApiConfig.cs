using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ParameterBindingExample.ParameterBindings;

namespace ParameterBindingExample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.ParameterBindingRules.Add(
                descriptor => 
                    RequestUriParameterBinding.AppliesTo(descriptor) 
                    ? new RequestUriParameterBinding(descriptor) 
                    : null);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
