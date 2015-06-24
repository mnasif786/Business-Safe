using System;
using System.Collections.Generic;
using System.ComponentModel;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Request
{
    public class SearchAccidentRecordsRequest
    {
        public long CompanyId { get; set; }
        public string Title { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public long? SiteId { get; set; }
        public bool ShowDeleted { get; set; }
        public bool ShowArchived { get; set; }
        public IList<long> AllowedSiteIds { get; set; }
        public string InjuredPersonForename { get; set; }
        public string InjuredPersonSurname { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public AccidentRecordstOrderByColumn OrderBy { get; set; }
        public bool Ascending { get; set; }
    }
}