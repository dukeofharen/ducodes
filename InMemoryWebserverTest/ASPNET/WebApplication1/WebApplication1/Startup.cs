using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Owin;
using Unity;
using Unity.WebApi;

namespace WebApplication1
{
   public class Startup
   {
      /// <summary>
      /// Configures WebAPI to use OWIN.
      /// </summary>
      /// <param name="appBuilder">The application builder.</param>
      public void Configuration(IAppBuilder appBuilder)
      {
         Configure(appBuilder);
      }

      /// <summary>
      /// Configures the web application.
      /// </summary>
      /// <param name="appBuilder">The application builder.</param>
      /// <param name="container">The container.</param>
      /// <param name="settings">The settings.</param>
      internal static void Configure(IAppBuilder appBuilder, IUnityContainer container = null)
      {
         var config = new HttpConfiguration();
         config.MapHttpAttributeRoutes();

         container = container ?? DependencyContainer.GetContainer();
         config.DependencyResolver = new UnityDependencyResolver(container);

         // Update Newtonsoft.Json settings so NULL properties in returned JSON messages are excluded.
         config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

         appBuilder.UseWebApi(config);
      }
   }
}