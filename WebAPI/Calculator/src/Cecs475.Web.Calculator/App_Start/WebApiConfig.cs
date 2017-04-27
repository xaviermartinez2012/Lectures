using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Cecs475.Web.Calculator {
	public static class WebApiConfig {
		public static void Register(HttpConfiguration config) {
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();

			// We remove the auto-generated "MapHttpRoute" method calls which set up convention-based routing.

			GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings
				.Add(new RequestHeaderMapping("Accept",
						"text/html",
						StringComparison.InvariantCultureIgnoreCase,
						true,
						"application/json"));
		}
	}
}
