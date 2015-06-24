using System;
using System.Collections.Generic;
using System.ComponentModel;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Request.Responsibilities
{
    public class SearchResponsibilitiesRequest
    {      
        public Guid CurrentUserId { get; set; }
        public IList<long> AllowedSiteIds { get; set; }
        public long CompanyId { get; set; }
        public long? ResponsibilityCategoryId { get; set; }
        public long? SiteId { get; set; }
        public long? SiteGroupId { get; set; }
        public string Title { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public bool ShowDeleted { get; set; }
        public bool ShowCompleted { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public ResponsibilitiesRequestOrderByColumn OrderBy { get; set; }
        public bool Ascending { get; set; }
    }
}