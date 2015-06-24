using System.Collections.Generic;
using FluentValidation.Results;

namespace BusinessSafe.Application.Response
{
    public class UpdateEmployeeResponse
    {
        public bool Success { get; set; }
        public IList<ValidationFailure> Errors { get; set; }
    }
}
