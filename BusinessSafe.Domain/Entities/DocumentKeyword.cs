using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class DocumentKeyword : Entity<long>
    {
        public virtual Document Document { get; set; }
        public virtual Keyword Keyword { get; set; }
    }
}
