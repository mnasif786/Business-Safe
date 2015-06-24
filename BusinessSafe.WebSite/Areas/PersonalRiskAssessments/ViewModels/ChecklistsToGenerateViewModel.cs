using System.Collections.Generic;
using BusinessSafe.Application.Request;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels
{
    public class ChecklistsToGenerateViewModel
    {
        public bool? HasMultipleChecklistRecipients { get; set; }
        public IList<EmployeeWithNewEmailRequest> RequestEmployees { get; set; }
        public IList<long> ChecklistIds { get; set; }
        public string Message { get; set; }
    }
}