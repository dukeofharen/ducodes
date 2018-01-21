using System.ComponentModel.DataAnnotations;

namespace ConfigValidationExample.Models.Configuration
{
    public class DatabaseSettings
    {
       [Required]
       public string ConnectionString { get; set; }
    }
}
