using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConfigValidationExample.Services
{
    public interface IModelValidator
    {
       IEnumerable<ValidationResult> ValidateModel(object model);
    }
}
