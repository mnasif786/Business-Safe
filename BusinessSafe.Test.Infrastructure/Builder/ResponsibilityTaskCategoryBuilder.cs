using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Test.Infrastructure.Builder
{
    public class ResponsibilityTaskCategoryBuilder
    {
        private static long _id;
        private static string _category;

        public static ResponsibilityTaskCategoryBuilder Create()
        {
            var responsibilityTaskCategoryBuilder = new ResponsibilityTaskCategoryBuilder();
            _id = 1;
            _category = "Category";
            return responsibilityTaskCategoryBuilder;
        }

        public TaskCategory Build()
        {
            return TaskCategory.Create(_id, _category);

        }

        public ResponsibilityTaskCategoryBuilder WithId(long responsibilityTaskCategoryId)
        {
            _id = responsibilityTaskCategoryId;
            return this;
        }
    }
}