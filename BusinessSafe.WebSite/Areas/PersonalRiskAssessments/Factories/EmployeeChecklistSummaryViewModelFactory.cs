using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Checklists;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories
{
    public class EmployeeChecklistSummaryViewModelFactory : IEmployeeChecklistSummaryViewModelFactory
    {
        private readonly IEmployeeChecklistService _employeeChecklistService;
        private Guid _employeeChecklistId;
        private const string checklistUrlPattern = "/Checklists/{0}?completedOnEmployeesBehalf=true";

        public EmployeeChecklistSummaryViewModelFactory(IEmployeeChecklistService employeeChecklistService)
        {
            _employeeChecklistService = employeeChecklistService;
        }

        public IEmployeeChecklistSummaryViewModelFactory WithEmployeeChecklistId(Guid employeeChecklistId)
        {
            _employeeChecklistId = employeeChecklistId;
            return this;
        }

        public EmployeeChecklistSummaryViewModel GetViewModel()
        {
            var employeeChecklist = _employeeChecklistService.GetWithCompletedOnEmployeesBehalfBy(_employeeChecklistId);

            return new EmployeeChecklistSummaryViewModel
                       {
                           Id = employeeChecklist.Id,
                           FriendlyReference = employeeChecklist.FriendlyReference,
                           ChecklistTitle = employeeChecklist.Checklist.Title,
                           RecipientFullName = employeeChecklist.Employee.FullName,
                           RecipientEmail = GetPotentiallyNullRecipientEmail(employeeChecklist),
                           DueDateForCompletion = GetPotentiallyNullDueDateForCompletion(employeeChecklist),
                           CompleteDate = GetPotentiallyNullCompleteDate(employeeChecklist),
                           MessageBody = employeeChecklist.LastMessage,
                           NotificationRequired = GetNotificationRequired(employeeChecklist),
                           CompletionNotificationEmailAddress = employeeChecklist.CompletionNotificationEmailAddress,
                           LastRecipientEmail = employeeChecklist.LastRecipientEmail,
                           ChecklistUrl = string.Format(checklistUrlPattern, _employeeChecklistId),
                           ShowCompletedOnEmployeesBehalfBySection = employeeChecklist.CompletedOnEmployeesBehalfBy != null,
                           CompletedOnEmployeesBehalfName = GetPotentiallyNullCompletedOnEmployeesBehalfName(employeeChecklist),
                           IsFurtherActionRequired = GetPotentiallyNullIsFurtherActionRequired(employeeChecklist),
                           AssessedBy = GetPotentiallyNullAssessedBy(employeeChecklist),
                           AssessmentDate = GetPotentiallyNullAssessmentDate(employeeChecklist)
                       };
        }

        private static bool? GetPotentiallyNullIsFurtherActionRequired(EmployeeChecklistDto employeeChecklist)
        {
            return employeeChecklist.IsFurtherActionRequired.HasValue ? (bool?)employeeChecklist.IsFurtherActionRequired.Value : null;
        }

        private static string GetPotentiallyNullAssessedBy(EmployeeChecklistDto employeeChecklist)
        {
            return employeeChecklist.AssessedByEmployee != null ? employeeChecklist.AssessedByEmployee.FullName : string.Empty;
        }

        private static string GetPotentiallyNullAssessmentDate(EmployeeChecklistDto employeeChecklist)
        {
            return employeeChecklist.AssessmentDate.HasValue ? employeeChecklist.AssessmentDate.Value.ToShortDateString() : string.Empty;
        }

        private static string GetPotentiallyNullCompletedOnEmployeesBehalfName(EmployeeChecklistDto employeeChecklist)
        {
            return employeeChecklist.CompletedOnEmployeesBehalfBy != null &&
                employeeChecklist.CompletedOnEmployeesBehalfBy.Employee != null
                ? employeeChecklist.CompletedOnEmployeesBehalfBy.Employee.FullName
                : null;
        }

        private static bool GetNotificationRequired(EmployeeChecklistDto employeeChecklist)
        {
            return employeeChecklist.SendCompletedChecklistNotificationEmail.HasValue &&
                employeeChecklist.SendCompletedChecklistNotificationEmail.Value;
        }

        private static string GetPotentiallyNullCompleteDate(EmployeeChecklistDto employeeChecklist)
        {
            return employeeChecklist.CompletedDate.HasValue
                ? employeeChecklist.CompletedDate.Value.ToShortDateString()
                : string.Empty;
        }

        private static string GetPotentiallyNullDueDateForCompletion(EmployeeChecklistDto employeeChecklist)
        {
            return employeeChecklist.DueDateForCompletion.HasValue
                ? employeeChecklist.DueDateForCompletion.Value.ToShortDateString()
                : string.Empty;
        }

        private static string GetPotentiallyNullRecipientEmail(EmployeeChecklistDto employeeChecklist)
        {
            return employeeChecklist.Employee.MainContactDetails != null
                ? employeeChecklist.Employee.MainContactDetails.Email
                : null;
        }
    }
}