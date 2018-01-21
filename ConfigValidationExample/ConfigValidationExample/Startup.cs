using ConfigValidationExample.Models.Configuration;
using ConfigValidationExample.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            .AddJsonFile("appsettings.json");

         Configuration = builder.Build();

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
      }
   }
}
