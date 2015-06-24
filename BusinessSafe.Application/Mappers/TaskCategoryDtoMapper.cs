using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class TaskCategoryDtoMapper
    {
        public static TaskCategoryDto Map(TaskCategory entity)
        {
            return new TaskCategoryDto()
                   {
                       Id = entity.Id,
                       Category = entity.Category
                   };
        }
    }
}