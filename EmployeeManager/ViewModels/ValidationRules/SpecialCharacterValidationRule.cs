using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace EmployeeManager.ViewModels.ValidationRules
{
    internal class SpecialCharacterValidationRule:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string strValue = Convert.ToString(value);

            if (string.IsNullOrEmpty(strValue))
                return new ValidationResult(false, $"Value cannot be null or blank");

            var regEx = new Regex("[^0-9a-zA-Z ,]");
            if (regEx.IsMatch(strValue))
                return new ValidationResult(false, $"Value contains invalid characters");

            return new ValidationResult(true, null);
        }
    }
}
