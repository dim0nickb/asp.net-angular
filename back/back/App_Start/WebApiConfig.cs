﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace back
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			// Web API configuration and services
			config.EnableCors();

			// Web API routes
			config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

			config.Routes.MapHttpRoute(
				name: "ExtendedApi",
				routeTemplate: "api/{controller}/{currencyId}/{date}",
				defaults: new { currencyId = RouteParameter.Optional, date = RouteParameter.Optional }
			);
		}
    }
}