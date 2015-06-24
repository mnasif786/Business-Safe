using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace BusinessSafe.Application.Response
{
    public class AddEmployeeResponse
    {
        public Guid EmployeeId { get; set; }
        public bool Success { get; set; }
        public List<string> Messages { get; set; }
        public IList<ValidationFailure> Errors { get; set; }
    }
}