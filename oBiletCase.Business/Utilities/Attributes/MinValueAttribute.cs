using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oBiletCase.Business.Utilities.Attributes
{
    public class MinValueAttribute : ValidationAttribute
    {
        private readonly int _minValue;

        public MinValueAttribute(int maxValue)
        {
            _minValue = maxValue;
        }

        public override bool IsValid(object value)
        {
            return (int)value >= _minValue;
        }
    }
}
