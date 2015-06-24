using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.ViewModels;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels
{
    public class EmployeeChecklistGeneratorViewModel: IValidatableObject
    {
        public Guid? EmployeeId { get; set; }
        public long RiskAssessmentId { get; set; }
        public string IsForMultipleEmployees { get; set; }
        public bool SingleEmployeesSectionVisible { get; set; }
        public bool MultipleEmployeesSectionVisible { get; set; }
        public List<AutoCompleteViewModel> Employees { get; set; }
        public List<AutoCompleteViewModel> Sites { get; set; }
        public long? SiteId { get; set; }
        public string Site { get; set; }
        public List<EmployeeDto> MultiSelectEmployees { get; set; }
        public List<SelectedEmployeeViewModel> MultiSelectedEmployees { get; set; }
        public string NewEmployeeEmail { get; set; }
        public bool NewEmployeeEmailVisible { get; set; }
        public string ExistingEmployeeEmail { get; set; }
        public bool ExistingEmployeeEmailVisible { get; set; }
        public List<ChecklistGeneratorChecklistViewModel> Checklists { get; set; }
        public string Message { get; set; }
        public PersonalRiskAssessementEmployeeChecklistStatusEnum PersonalRiskAssessementEmployeeChecklistStatus { get; set; }
        public bool Generating { get; set; }
        public ChecklistsToGenerateViewModel ChecklistsToGenerate { get; set; }
        public long CompanyId{ get; set; }
        public DateTime? CompletionDueDateForChecklists { get; set; }
        public bool? SendCompletedChecklistNotificationEmail { get; set; }
        public string CompletionNotificationEmailAddress { get; set; }

        public EmployeeChecklistGeneratorViewModel()
        {
            Employees = new List<AutoCompleteViewModel>();
            Sites = new List<AutoCompleteViewModel>();
            MultiSelectEmployees = new List<EmployeeDto>();
            MultiSelectedEmployees = new List<SelectedEmployeeViewModel>();
            Checklists = new List<ChecklistGeneratorChecklistViewModel>();
        }

        public string GetCompletionDueDateForChecklists
        {
            get
            {
                return CompletionDueDateForChecklists.HasValue
                           ? CompletionDueDateForChecklists.Value.ToShortDateString()
                           : string.Empty;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            
            if(Generating)
            {
                if (SingleEmployeesSectionVisible == false && MultipleEmployeesSectionVisible == false)
                {
                    results.Add(new ValidationResult("Sending to single or multiple employees is required", new[] { "singleEmployee", "multipleEmployees" }));
                }

                if (NewEmployeeEmailVisible)
                {
                    if (String.IsNullOrEmpty(NewEmployeeEmail))
                    {
                        results.Add(new ValidationResult("Email is required", new[] { "NewEmployeeEmail" }));
                    }
                    else if (Regex.Match(NewEmployeeEmail, @"^[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$").Success == false)
                    {
                        results.Add(new ValidationResult("Valid email address is required", new[] { "NewEmployeeEmail" }));
                    }
                }

                if (SingleEmployeesSectionVisible && ChecklistsToGenerate != null && ChecklistsToGenerate.RequestEmployees.Any() == false)
                {
                    results.Add(new ValidationResult("Employee is required", new[] { "Employee" }));
                }

                if (MultipleEmployeesSectionVisible && ChecklistsToGenerate != null && !ChecklistsToGenerate.RequestEmployees.Any())
                {
                    results.Add(new ValidationResult("Employee is required, select the Add option to add selected employee(s) to the required checklist.", new[] { "Employee" }));
                }

                if (MultipleEmployeesSectionVisible && ChecklistsToGenerate != null && ChecklistsToGenerate.RequestEmployees.Any())
                {
                    var invalidEmail = false;

                    foreach(var requestEmployee in ChecklistsToGenerate.RequestEmployees)
                    {
                        if(requestEmployee.NewEmail != null && Regex.Match(requestEmployee.NewEmail, @"^[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$").Success == false)
                        {
                            invalidEmail = true;
                        }
                    }

                    if (invalidEmail)
                    {
                        results.Add(new ValidationResult("All employee emails must be valid. ", new[] {"Employee"}));
                    }
                }

                if (ChecklistsToGenerate != null && ChecklistsToGenerate.ChecklistIds.Any() == false)
                {
                    results.Add(new ValidationResult("At least one checklist is required", new[] { "IncludeChecklist_1", "IncludeChecklist_2", "IncludeChecklist_3", "IncludeChecklist_4" }));
                }

                if (string.IsNullOrEmpty(Message))
                {
                    results.Add(new ValidationResult("Message is required", new[] { "Message" }));
                }

                if(SendCompletedChecklistNotificationEmail.GetValueOrDefault())
                {
                    if (String.IsNullOrEmpty(CompletionNotificationEmailAddress))
                    {
                        results.Add(new ValidationResult("Completion Notification Email is required", new[] { "CompletionNotificationEmailAddress" }));
                    }
                }
            }

            if (!String.IsNullOrEmpty(CompletionNotificationEmailAddress))
            {
                if (Regex.Match(CompletionNotificationEmailAddress, @"^[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$").Success == false)
                {
                    results.Add(new ValidationResult("Valid Completion Notification Email address is required", new[] { "CompletionNotificationEmailAddress" }));
                }
            }
            
            return results;
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext, ModelStateDictionary modelState)
        {
            var validationResults = Validate(new ValidationContext(this, null, null));
            if (validationResults.Any())
            {
                foreach (var error in validationResults)
                    foreach (var memberName in error.MemberNames)
                        modelState.AddModelError(memberName, error.ErrorMessage);
            }
            return validationResults;
        }

        public bool IsChecklistSelected(long checklistId)
        {
            return Checklists != null && 
                   Checklists.Any(x => x.Id == checklistId && x.Checked);       
            
        }
    }
}