using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string> ListOfErrors { get; set; } = new List<string>();    
        public ValidationException(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                ListOfErrors.Add(error.ErrorMessage);
            }
        }
    }
}
