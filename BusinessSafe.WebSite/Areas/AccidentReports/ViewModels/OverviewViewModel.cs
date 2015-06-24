using System.Collections.Generic;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.AccidentReports.ViewModels
{
    public class AccidentRecordNotificationMemberEmail
    {
        public string Email { get; set; }
        public string Name { get; set; }        
    }

    public class OverviewViewModel 
    {
        public long AccidentRecordId { get; set; }
        public long CompanyId { get; set; }
        public string DescriptionHowAccidentHappened { get; set; }
        public DocumentsViewModel Documents { get; set; }
        public bool NextStepsVisible { get; set; }
        public bool IsReportable { get; set; }

        public List<AccidentRecordNotificationMemberEmail> AccidentRecordNotificationMemberEmails { get; set; }

        public bool DoNotSendEmailNotification { get; set; }

        public bool EmailNotificationSent { get; set; }
        public OverviewViewModel()
        {
            Documents = new DocumentsViewModel();
            Documents.ExistingDocumentsViewModel = new ExistingDocumentsViewModel();
            AccidentRecordNotificationMemberEmails = new List<AccidentRecordNotificationMemberEmail>();
        }        
    }
}
