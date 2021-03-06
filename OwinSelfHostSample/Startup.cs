﻿using Owin;
using System.Web.Http;

namespace OwinSelfhostSample
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional,
                                pareaname= RouteParameter.Optional,
                                pareaid = RouteParameter.Optional,
                                platitude = RouteParameter.Optional,
                                plongitude = RouteParameter.Optional,
                                postrefuse = RouteParameter.Optional,
                                putrefuse = RouteParameter.Optional


                }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi2",
                routeTemplate: "api/{controller}/{id}/{pareaid}",
                defaults: new
                {
                    id = RouteParameter.Optional,
                    pareaname = RouteParameter.Optional,
                    pareaid = RouteParameter.Optional,
                    platitude = RouteParameter.Optional,
                    plongitude = RouteParameter.Optional,
                    postrefuse = RouteParameter.Optional,
                    putrefuse = RouteParameter.Optional


                }
            );


            appBuilder.UseWebApi(config);
        }
    }
}
