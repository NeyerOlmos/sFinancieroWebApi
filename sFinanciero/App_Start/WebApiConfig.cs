using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

using System.Web.Http.Cors;

namespace sFinanciero
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            



            // Configuración y servicios de Web API
            //EnableCorsAttribute cors = new EnableCorsAttribute("http://sfinanciero.herokuapp.com , http://localhost:4200", "*", "*");
            //config.EnableCors(cors);
            config.EnableCors();

            // Configure Web API para usar solo la autenticación de token de portador.
            config.SuppressDefaultHostAuthentication();

            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Rutas de Web API
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            config.Formatters.Remove(config.Formatters.XmlFormatter);
            //config.Formatters.JsonFormatter.SerializerSettings.Formatting =Newtonsoft.Json.Formatting.Indented;
          //  config.Formatters.JsonFormatter.SerializerSettings.MaxDepth = 1;
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
         //  config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
        }
    }
}
