using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Infrastructure.Attributes;

namespace BusinessSafe.Application.DataTransferObjects
{
    [CoverageExclude]
    public class TaskCategoryDto
    {
        public long? Id { get; set; }
        public string Category { get; set; }

        public static TaskCategoryDto CreateFrom(TaskCategory taskCategory)
        {
            return  new TaskCategoryDto()
                        {
                            Id = taskCategory.Id,
                            Category = taskCategory.Category
                        };
        }

        public static IEnumerable<TaskCategoryDto> CreateFrom(IEnumerable<TaskCategory> taskCategories)
        {
            return taskCategories.Select(CreateFrom);
        }
    }
}