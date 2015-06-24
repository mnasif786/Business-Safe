using System.Collections.Generic;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public abstract class Document : Entity<long>
    {
        public virtual long ClientId { get; protected set; }
        public virtual long DocumentLibraryId { get; set; }
        public virtual string Filename { get; set; }
        public virtual string Extension { get; set; }
        public virtual long FilesizeByte { get; set; }
        public virtual string Description { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual string Title { get; set; }
        public virtual IList<DocumentKeyword> DocumentKeywords { get; set; }
        
        public abstract string DocumentReference { get; }
        public abstract string SiteReference { get;  }
    }
}
