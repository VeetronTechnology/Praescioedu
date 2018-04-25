using Praescio.API.CustomFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Praescio.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new UnhandledExceptionFilter());  
            // Web API configuration and services

            // Web API routes

            // Web API routes
            config.MapHttpAttributeRoutes();
            //var cors = new EnableCorsAttribute("*", "Authorization, X-Requested-With, Content-Type, Accept, Origin", "POST, PUT, GET, DELETE, OPTIONS");
            //config.EnableCors(cors);


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
