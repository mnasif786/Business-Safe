using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Results;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class InvalidAddUpdateEmployeeContactDetailsParameters: Exception
    {
        public readonly IList<ValidationFailure> Errors;
        public InvalidAddUpdateEmployeeContactDetailsParameters(IList<ValidationFailure> errors )
            : base("Invalid values specfied when adding/updating employee contact details. See list of errors for details of validation failures")
        {
            Errors = errors;
        }
    }

}
