using System;
using System.Collections.Generic;
using System.ComponentModel;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Request
{
    public class SearchTasksRequest
    {
        public long CompanyId { get; set; }
        public IEnumerable<Guid> EmployeeIds { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public DateTime? CompletedFrom { get; set; }
        public DateTime? CompletedTo { get; set; }
        public long TaskCategoryId { get; set; }
        public int TaskStatusId { get; set; }
        public bool ShowDeleted { get; set; }
        public bool ShowCompleted { get; set; }
        public IList<long> AllowedSiteIds { get; set; }
        public long? SiteGroupId { get; set; }
        public long? SiteId { get; set; }
        public string Title { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public TaskOrderByColumn OrderBy { get; set; }
        public bool Ascending { get; set; }
    }
}
