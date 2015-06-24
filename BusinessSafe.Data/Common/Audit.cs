using System;

namespace BusinessSafe.Data.Common
{
    public class Audit
    {
        public virtual long Id { get; set; }
        public virtual string Type { get; set; }
        public virtual string FieldName { get; set; }
        public virtual string OldValue { get; set; }
        public virtual string NewValue { get; set; }
        public virtual string UserName { get; set; }
        public virtual string EntityId { get; set; }
        public virtual DateTime UpdateDate { get; set; }
        public virtual string EntityName { get; set; }
    }
}