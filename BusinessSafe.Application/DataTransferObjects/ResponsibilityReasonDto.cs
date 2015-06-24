using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Infrastructure.Attributes;

namespace BusinessSafe.Application.DataTransferObjects
{
    [CoverageExclude]
    public class ResponsibilityReasonDto
    {
        public long? Id { get; set; }
        public string Reason { get; set; }
    }
}