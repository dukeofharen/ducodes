using System.ComponentModel.DataAnnotations;

namespace ConfigValidationExample.Models.Configuration
{
   public class HyperLink
   {
      [Required]
      public string Text { get; set; }

      [Url]
      [Required]
      public string Link { get; set; }
   }
}
