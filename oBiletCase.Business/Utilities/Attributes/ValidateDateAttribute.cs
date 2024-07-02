using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oBiletCase.Business.Utilities.Attributes
{
    public class ValidateDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid
            (object obj, ValidationContext validationContext)
        {
            DateTime date = Convert.ToDateTime(obj);

            return (date >= DateTime.Now)
                ? ValidationResult.Success
                : new ValidationResult("Date cannot be earlier than today!");
        }
    }
}
