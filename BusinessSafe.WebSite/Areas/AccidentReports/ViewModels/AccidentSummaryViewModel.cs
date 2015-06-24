using System;
using System.ComponentModel.DataAnnotations;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.WebSite.Areas.AccidentReports.ViewModels
{
    public class AccidentSummaryViewModel
    {
        public long CompanyId { get; set; }
        public long AccidentRecordId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Reference is required")]
        public string Reference { get; set; }
        public long JurisdictionId { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool NextStepsVisible { get; set; }
        public AccidentRecordStatusEnum Status { get; set; }

        public static object Create(long id, long companyId, string title, string reference, DateTime createdOn, bool isDeleted, bool isClosed)
        {
            var result = new AccidentSummaryViewModel
                             {
                                 AccidentRecordId = id,
                                 CompanyId = companyId,
                                 Title = title,
                                 Reference = reference,
                                 CreatedOn = createdOn,
                                 IsDeleted = isDeleted,
                                 Status = isClosed ? AccidentRecordStatusEnum.Closed : AccidentRecordStatusEnum.Open
                             };
            return result;
        }
    }
}