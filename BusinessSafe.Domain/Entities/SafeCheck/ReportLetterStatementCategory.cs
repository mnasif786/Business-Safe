using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities.SafeCheck
{
    public class ReportLetterStatementCategory : BaseEntity<Guid>
    {
        public virtual string Name { get; set; }
        public virtual int Sequence { get; set; }
    }
}
