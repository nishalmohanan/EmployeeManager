using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Client
{
    internal  class EmployeeValidator
    {
        private Employee _employee;

        public EmployeeValidator(Employee employee)
        {
            _employee = employee;
        }
        public bool IsValid()
        {
            return Validate(_employee).isValid;
        }
        private (bool isValid, IList<string> errors) Validate(Employee employee)
        {
            ValidationContext employeeValidationContext = new ValidationContext(employee);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            if (Validator.TryValidateObject(employee, employeeValidationContext, validationResults))
            {
                return (true, new List<string>());
            }
            else
            {
                return (false, new List<string>());
            }
        }
        
    }
}
