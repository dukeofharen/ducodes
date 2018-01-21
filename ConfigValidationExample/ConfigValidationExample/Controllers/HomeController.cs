using Microsoft.AspNetCore.Mvc;
using ConfigValidationExample.Models.Configuration;
using ConfigValidationExample.Services;
using Newtonsoft.Json;

namespace ConfigValidationExample.Controllers
{
   public class HomeController : Controller
   {
      private readonly IValidatedSettings<Settings> _validatedSettings;

      public HomeController(IValidatedSettings<Settings> validatedSettings)
      {
         _validatedSettings = validatedSettings;
      }

      public IActionResult Index()
      {
         var settings = _validatedSettings.GetValidatedSettings(true);
         ViewData["Settings"] = JsonConvert.SerializeObject(settings);
         return View();
      }
   }
}
