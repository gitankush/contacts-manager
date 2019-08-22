using Evolent.ContactsMgmt.BL.Repository;
using Evolent.ContactsMgmt.Common.Contracts;
using Evolent.ContactsMgmt.Common.Helpers;
using Evolent.ContactsMgmt.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Evolent.ContactsMgmt.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            ServiceLocator.Register<IRepository<ContactDTO>>(new ContactRepository());

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
