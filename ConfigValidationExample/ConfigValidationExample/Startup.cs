using System;
using System.IO;
using ConfigValidationExample.Models.Configuration;
using ConfigValidationExample.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ConfigValidationExample
{
   public class Startup
   {
      public IConfiguration Configuration { get; set; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         // Register the configuration here.
         var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true);
         Configuration = builder.Build();

         services.Configure<Settings>(Configuration);

         // Register the classes needed for the configuration validation
         services.AddTransient<IModelValidator, ModelValidator>();
         services.AddTransient<IValidatedSettings<Settings>, ValidatedSettings<Settings>>();

         services.AddMvc();
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
            app.UseBrowserLink();
         }
         else
         {
            app.UseExceptionHandler("/Home/Error");
         }

         app.UseStaticFiles();

         app.UseMvc(routes =>
         {
            routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");
         });

         var monitor = app.ApplicationServices.GetService<IOptionsMonitor<Settings>>();
         var validatedSettings = app.ApplicationServices.GetService<IValidatedSettings<Settings>>();
         monitor.OnChange(settings =>
         {
            // Make sure that every time when the settings file changes, it will be validated.
            validatedSettings.GetValidatedSettings(true);
         });
      }
   }
}
