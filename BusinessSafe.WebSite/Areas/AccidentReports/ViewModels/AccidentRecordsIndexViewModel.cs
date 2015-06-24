using System.Collections.Generic;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.AccidentReports.ViewModels
{
    public class AccidentRecordsIndexViewModel
    {
        public long? SiteId { get; set; }
        public string Title { get; set; }
        public string CreatedFrom { get; set; }
        public string CreatedTo { get; set; }
        public string InjuredPersonForename { get; set; }
        public string InjuredPersonSurname { get; set; }
        public bool IsShowDeleted { get; set; }
        public AccidentRecordStatusEnum Status { get; set; }

        public List<SearchAccidentRecordResultViewModel> AccidentRecords { get; set; }

        public List<AutoCompleteViewModel> Sites { get; set; }

        public int Page { get; set; }
        public int Size { get; set; }
        public int Total { get; set; }
        public string OrderBy { get; set; }
    }

    public class SearchAccidentRecordResultViewModel
    {
        public long Id { get; set; }
        public string Reference { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string InjuredPerson { get; set; }        
        public string Severity { get; set; }
        public string Site { get; set; }
        public string ReportedBy { get; set; }
        public string DateOfAccident { get; set; }
        public string DateCreated { get; set; }
        public bool IsDeleted { get; set; }
        public AccidentRecordStatusEnum Status { get; set; }
    }
}