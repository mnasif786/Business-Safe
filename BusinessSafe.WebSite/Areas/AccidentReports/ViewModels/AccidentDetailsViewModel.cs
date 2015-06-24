using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.AccidentReports.ViewModels
{
    public class AccidentDetailsViewModel
    {
        public static long OFF_SITE = -1L;
        public static long OTHER_ACCIDENT_TYPE = 16L;
        public static long OTHER_ACCIDENT_CAUSE = 14L;

        public IEnumerable<AutoCompleteViewModel> Sites { get; set; }
        public IEnumerable<AutoCompleteViewModel> Employees { get; set; }
        public IEnumerable<AutoCompleteViewModel> AccidentTypes { get; set; }
        public IEnumerable<AutoCompleteViewModel> AccidentCauses { get; set; }
        public DocumentsViewModel DocumentsViewModel { get; set; }
        public long CompanyId { get; set; }
        public long AccidentRecordId { get; set; }        
        public string DateOfAccident { get; set; }
        public string TimeOfAccident { get; set; }
        public string Site { get; set; }
        public long? SiteId { get; set; }
        public string Location { get; set; }
        public string Severity { get; set; }
        public string AccidentType { get; set; }
        public long? AccidentTypeId { get; set; }
        public bool IsOtherAccidentType { get; set; }
        public string OtherAccidentType { get; set; }
        public string AccidentCause { get; set; }
        public long? AccidentCauseId { get; set; }
        public bool IsOtherAccidentCause { get; set; }
        public string OtherAccidentCause { get; set; }
        public bool FirstAidAdministered { get; set; }
        public string FirstAiderEmployee { get; set; }
        public Guid? FirstAiderEmployeeId { get; set; }
        [MaxLength(250, ErrorMessage = "Details of first treatment can not be greater than 500 characters.")]
        public string DetailsOfFirstAid { get; set; }
        public string DetailsOfAccident { get; set; }
        public string OffSiteName { get; set; }
        public string NonEmployeeFirstAiderName { get; set; }
        public bool ShowNonEmployeeFirstAidInputs { get; set; }
    }
}