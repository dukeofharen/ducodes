using System;
using System.Linq;
using System.Text;
using ConfigValidationExample.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ConfigValidationExample.Services
{
   internal class ValidatedSettings<TSettings> : IValidatedSettings<TSettings> where TSettings : class, new()
   {
      private static TSettings _settings;
      private readonly IOptionsMonitor<TSettings> _options;
      private readonly IModelValidator _modelValidator;

      public ValidatedSettings(
         IOptionsMonitor<TSettings> options,
         IModelValidator modelValidator)
      {
         _options = options;
         _modelValidator = modelValidator;
      }

      public TSettings GetValidatedSettings(bool forceValidate = false)
      {
         if (_settings == null || forceValidate)
         {
            var settings = _options.CurrentValue;
            var validationResults = _modelValidator.ValidateModel(settings).ToArray();
            if (validationResults.Any())
            {
               var builder = new StringBuilder();
               builder.AppendLine("The configuration file contains the following errors:");
               foreach (var result in validationResults)
               {
                  builder.AppendLine(result.ErrorMessage);
                  if (result is CompositeValidationResult compositeResult)
                  {
                     foreach (var compositeError in compositeResult.Results)
                     {
                        builder.AppendLine($"- {compositeError.ErrorMessage}");
                     }
                  }
               }

               throw new Exception(builder.ToString());
            }

            _settings = settings;
         }

         return _settings;
      }
   }
}
