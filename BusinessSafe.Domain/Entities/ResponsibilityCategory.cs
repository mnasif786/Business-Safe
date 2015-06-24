using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class ResponsibilityCategory : Entity<long>
    {
        public virtual string Category { get; protected set; }
        public virtual int Sequence { get; protected set; }

        public static ResponsibilityCategory Create(long id, string category)
        {
            var responsibilityCategory = new ResponsibilityCategory { Id = id, Category = category };

            return responsibilityCategory;
        }
    }
}
