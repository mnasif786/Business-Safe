using System.Collections.Generic;
using System.ServiceModel;
using BusinessSafe.Domain.Common;
using BusinessSafe.Infrastructure.Attributes;

namespace BusinessSafe.Application.Common
{
    [CoverageExclude]
    public class ValidationFaultException : FaultException<ValidationFault>
    {
        public ValidationFaultException(IEnumerable<ValidationMessage> messages) : base(new ValidationFault(messages), "A validation error occured.")
        { }
    }
}
