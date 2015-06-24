using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities.SafeCheck
{
    public class ChecklistQuestion : BaseEntity<Guid>
    {
        public virtual Checklist Checklist { get; set; }
        public virtual Question Question { get; set; }

        public virtual int? QuestionNumber { get; set; }
        public virtual int? CategoryNumber { get; set; }
    }
}
