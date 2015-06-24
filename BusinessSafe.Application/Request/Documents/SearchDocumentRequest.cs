using System.Collections.Generic;

namespace BusinessSafe.Application.Request.Documents
{
    public class SearchDocumentRequest
    {
        public long CompanyId { get; set; }
        public string TitleLike { get; set; }
        public long DocumentTypeId { get; set; }
        public long SiteId { get; set; }
        public long SiteGroupId { get; set; }
        public IList<long> AllowedSiteIds { get; set; }
    }
}