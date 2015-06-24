using System.Collections.Generic;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class Section : Entity<long>
    {
        public virtual Checklist Checklist { get; set; }
        public virtual string Title { get; set; }
        public virtual string ShortTitle { get; set; }
        public virtual int ListOrder { get; set; }
        public virtual IList<Question> Questions { get; set; }
    }
}