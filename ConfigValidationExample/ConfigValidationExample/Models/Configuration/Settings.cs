using System.ComponentModel.DataAnnotations;
using ConfigValidationExample.Attributes;

namespace ConfigValidationExample.Models.Configuration
{
   public class Settings
   {
      [Required]
      public string WebsiteName { get; set; }

      [Required]
      [EmailAddress]
      public string AdminEmail { get; set; }

      [Required]
      [Url]
      public string RootUrl { get; set; }

      [ValidateObject]
      public DatabaseSettings DatabaseSettings { get; set; }

      [ValidateObject]
      public HyperLink[] Links { get; set; }
   }
}
