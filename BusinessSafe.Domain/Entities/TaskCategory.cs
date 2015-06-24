using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class TaskCategory : Entity<long>
    {
        public virtual string Category { get; protected set; }

        public static TaskCategory Create(long id, string category)
        {
            var responsibilityTaskCategory = new TaskCategory {Id = id, Category = category};

            return responsibilityTaskCategory;
        }
    }
}
