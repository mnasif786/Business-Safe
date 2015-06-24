using BusinessSafe.Domain.Entities;
using System.Collections.Generic;
namespace BusinessSafe.WebSite.Areas.AccidentReports.ViewModels
{
    public class NextStepsViewModel
    {
        public List<NextStepsSectionEnum> SectionsToShow { get; set; }
        public long AccidentRecordId { get; set; }
        public long CompanyId { get; set; }
        public long AccidentInvestigationFormClientDocumentationId { get; set; }
        public long GbAccidentReportGuidanceNoteClientDocumentationId { get; set; }
        public long NiAccidentReportGuidanceNoteClientDocumentationId { get; set; }
        public long RoiAccidentReportGuidanceNoteClientDocumentationId { get; set; }
        public long GbOnlineAccidentReportFormClientDocumentationId { get; set; }
        public long NiOnlineAccidentReportFormClientDocumentationId { get; set; }
        public long RoiOnlineAccidentReportFormClientDocumentationId { get; set; }
        public long IomOnlineAccidentReportFormClientDocumentationId { get; set; }
        public bool NextStepsVisible { get; set; }
    }
}