using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.Entities.SafeCheck;

namespace BusinessSafe.Domain.ParameterClasses.SafeCheck
{
    public class CreateUpdateChecklistParameters
    {
        public CreateUpdateChecklistParameters()
        {
            ImmediateRiskNotifications = new List<AddImmediateRiskNotificationParameters>();
        }
        
        public Guid Id { get; set; }
        public int? ClientId { get; set; }
        public int? SiteId { get; set; }
        public string CoveringLetterContent { get; set; }
        public string Status { get; set; }
        public string VisitDate { get; set; }
        public string VisitBy { get; set; }
        public string VisitType { get; set; }
        public string MainPersonSeenName { get; set; }
        public Employee MainPersonSeen { get; set; }
        public string AreasVisited { get; set; }
        public string AreasNotVisited { get; set; }
        public string EmailAddress { get; set; }
        public ImpressionType ImpressionType { get; set; }
        public IList<AddImmediateRiskNotificationParameters> ImmediateRiskNotifications { get; set; }
        public bool Submit { get; set; }
        public string SiteAddress1 { get; set; }
        public string SiteAddressPostcode { get; set; }
        public UserForAuditing User { get; set; }
        public Site Site { get; set; }
        public ChecklistTemplate ChecklistTemplate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CompletedOn { get; set; }
        public string CompletedBy { get; set; }
        public DateTime? SubmittedOn { get; set; }
        public string SubmittedBy { get; set; }
        public string PostedBy { get; set; }
        public string QAComments { get; set; }
        public bool UpdatesRequired { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool EmailReportToPerson { get; set; }
        public bool EmailReportToOthers { get; set; }
        public bool PostReport { get; set; }
        public string OtherEmailAddresses { get; set; }
        public SummaryReportHeaderType ReportHeader { get; set; }
        public bool IncludeActionPlan { get; set; }
        public bool IncludeComplianceReview { get; set; }
        public string Jurisdiction { get; set; }
        public bool ExecutiveSummaryUpdateRequired { get; set; }
        public bool ExecutiveSummaryQACommentsResolved { get; set; }
        public string ExecutiveSummaryQACommentsSignedOffBy { get; set; }
        public DateTime? ExecutiveSummaryQACommentsSignedOffDate { get; set; }
        public string ClientLogoFilename { get; set; }
        public string ClientSiteGeneralNotes { get; set; }
        public bool SpecialReport { get; set; }
    }
}
