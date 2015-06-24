using BusinessSafe.Infrastructure.Attributes;

namespace BusinessSafe.Application.DataTransferObjects
{
    [CoverageExclude]
    public class ResponsibilityCategoryDto
    {
        public long? Id { get; set; }
        public string Category { get; set; }
    }
}