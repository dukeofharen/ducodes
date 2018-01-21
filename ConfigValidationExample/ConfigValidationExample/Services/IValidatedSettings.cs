namespace ConfigValidationExample.Services
{
   public interface IValidatedSettings<TSettings> where TSettings : class
   {
      TSettings GetValidatedSettings(bool forceValidate = false);
   }
}
