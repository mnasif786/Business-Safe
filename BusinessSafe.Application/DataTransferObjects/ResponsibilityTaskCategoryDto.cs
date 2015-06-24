using BusinessSafe.Infrastructure.Attributes;

namespace BusinessSafe.Application.DataTransferObjects
{
    [CoverageExclude]
    public class ResponsibilityTaskCategoryDto
    {
        public string Category { get; private set; }
    }
}