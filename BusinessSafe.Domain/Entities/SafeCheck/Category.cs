using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities.SafeCheck
{
    public class Category : BaseEntity<Guid>
    {
        public virtual string Title { get; set; }
        public virtual string ReportTitle { get; set; }
        public virtual bool Mandatory { get; set; }
        public virtual IList<Question> Questions { get; set; }
        public virtual int OrderNumber { get; set; }
        public virtual string TabTitle { get; set; }

        public Category()
        {
            Questions = new List<Question>();
        }

        public static Category Create(Guid id, string title)
        {
            return new Category() {Id = id, Title = title};
        }
    }
}
