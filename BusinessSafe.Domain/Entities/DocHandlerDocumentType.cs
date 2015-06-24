using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class DocHandlerDocumentType : Entity<long>
    {
        public virtual DocHandlerDocumentTypeGroup DocHandlerDocumentTypeGroup { get; set; }
    }
}
