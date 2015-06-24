using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Company.ViewModels;

namespace BusinessSafe.WebSite.Areas.Company.Factories
{
    public class RiskAssessorsDefaultAddEditViewModelFactory : IRiskAssessorsDefaultAddEditViewModelFactory
    {
        private bool _showingDeleted;
        private IEnumerable<RiskAssessorDto> _riskAssessors;

        public IRiskAssessorsDefaultAddEditViewModelFactory WithShowingDeleted(bool showingDeleted)
        {
            _showingDeleted = showingDeleted;
            return this;
        }

        public IRiskAssessorsDefaultAddEditViewModelFactory WithRiskAssessors(IEnumerable<RiskAssessorDto> riskAssessorDtos)
        {
            _riskAssessors = riskAssessorDtos;
            return this;
        }

        public RiskAssessorsDefaultAddEdit GetViewModel()
        {
            var riskAssessorDefaults = _riskAssessors.Select(dto => new RiskAssessorsDefaults
            {
                Id = dto.Id.ToString(),
                EmployeeId = dto.Employee.Id,
                Forename = dto.Employee.Forename,
                Surname = dto.Employee.Surname,
                Site = dto.HasAccessToAllSites ? "All Sites": dto.Site != null ? dto.Site.Name : "",
                DoNotSendCompletedNotifications = dto.DoNotSendTaskCompletedNotifications,
                DoNotSendDueNotifications = dto.DoNotSendReviewDueNotification,
                DoNotSendOverDueNotifications = dto.DoNotSendTaskOverdueNotifications,
                IsSystemDefault = false
            }).ToList();

            return new RiskAssessorsDefaultAddEdit
                       {
                           FormId = "riskAssessors",
                           ColumnHeaderText = "Risk Assessor",
                           SectionHeading = "Risk Assessors",
                           TextInputWaterMark = "enter new assessor...",
                           RiskAssessors = riskAssessorDefaults,
                           DefaultType = "RiskAssessors",
                           ShowingDeleted = _showingDeleted
                       };
        }
    }
}