using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class Question : Entity<long>
    {
        public virtual Section Section { get; set; }
        public virtual QuestionType QuestionType { get; set; }
        public virtual int ListOrder { get; set; }
        public virtual bool IsRequired { get; set; }
        public virtual string Text { get; set; }
        public virtual string Information { get; set; }
    }
}
