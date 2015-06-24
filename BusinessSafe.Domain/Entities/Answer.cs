using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class Answer : Entity<long>
    {
        public virtual Question Question { get; set; }
        public virtual string AdditionalInfo { get; set; }

        public virtual Answer Self
        {
            get { return this; }
        }
    }
}
