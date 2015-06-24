using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class ActionPlanDto
    {
        public ActionPlanDto()
        {
            Actions = new List<ActionDto>();
            //ImmediateRiskNotifications = new List<ActionDto>();
        }

        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string Title { get; set; }
        public SiteDto Site { get; set; }
        public DateTime? DateOfVisit { get; set; }
        public string VisitBy { get; set; }
        public DateTime SubmittedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public AuditedUserDto CreatedBy { get; set; }
        public string AreasVisited { get; set; }
        public string AreasNotVisited { get; set; }
        public long? ExecutiveSummaryDocumentLibraryId { get; set; }
        public DerivedTaskStatusForDisplay Status { get; set; }
        public bool NoLongerRequired { get; set; }

        public IEnumerable<ActionDto> Actions { get; set; }
        //public IEnumerable<ActionDto> ImmediateRiskNotifications { get; set; }
    }
}