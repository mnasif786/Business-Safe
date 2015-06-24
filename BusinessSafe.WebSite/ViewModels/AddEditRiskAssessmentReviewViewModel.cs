using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request.Attributes;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.CustomValidators;
using BusinessSafe.WebSite.Extensions;

namespace BusinessSafe.WebSite.ViewModels
{
    public class AddEditRiskAssessmentReviewViewModel
    {
        public long? RiskAssessmentReviewId { get; set; }

        [Required]
        public long CompanyId { get; set; }

        [Required]
        public long RiskAssessmentId { get; set; }

        [Required(ErrorMessage = "The review date field is required")]
        [DateNotInPastValidation("Review Date")]
        public string ReviewDate { get; set; }

        [Required(ErrorMessage = "Please Select an Employee to Review")]
        [MustBeNotEmptyGuid(ErrorMessage = "Please Select an Employee to Review")]
        public Guid ReviewingEmployeeId { get; set; }

        public IEnumerable<AutoCompleteViewModel> RiskAssessmentReviewers { get; set; }

        public bool DoNotSendTaskNotification { get; set; }

        public static AddEditRiskAssessmentReviewViewModel CreateFrom(RiskAssessmentDto riskAssessment,
                                                                      IEnumerable<EmployeeDto> employees,
                                                                      IRiskAssessmentService riskAssessmentService)
        {
            var reviewDate = riskAssessmentService.GetDefaultDateOfNextReviewById(riskAssessment.Id);

            var viewModel = new AddEditRiskAssessmentReviewViewModel
                                {
                                    CompanyId = riskAssessment.CompanyId,
                                    RiskAssessmentId = riskAssessment.Id,
                                    ReviewingEmployeeId =
                                        riskAssessment.RiskAssessor != null
                                            ? riskAssessment.RiskAssessor.Employee.Id
                                            : Guid.Empty,
                                    ReviewDate = reviewDate.HasValue ? reviewDate.Value.ToString("dd/MM/yyyy") : null,
                                    RiskAssessmentReviewers = GetReviewersSelectList(employees)
                                };

            return viewModel;
        }

        public static AddEditRiskAssessmentReviewViewModel CreateFrom(RiskAssessmentReviewDto riskAssessmentReview,
                                                                      IEnumerable<EmployeeDto> employees)
        {
            var viewModel = new AddEditRiskAssessmentReviewViewModel
                                {
                                    CompanyId = riskAssessmentReview.RiskAssessment.CompanyId,
                                    RiskAssessmentId = riskAssessmentReview.RiskAssessment.Id,
                                    ReviewingEmployeeId = riskAssessmentReview.ReviewAssignedTo.Id,
                                    RiskAssessmentReviewId = riskAssessmentReview.Id,
                                    ReviewDate =
                                        riskAssessmentReview.CompletionDueDate.GetValueOrDefault().ToShortDateString(),
                                    RiskAssessmentReviewers = GetReviewersSelectList(employees)
                                };


            return viewModel;
        }

        private static IEnumerable<AutoCompleteViewModel> GetReviewersSelectList(IEnumerable<EmployeeDto> employees)
        {
            return employees.Select(AutoCompleteViewModel.ForEmployee).AddDefaultOption(Guid.Empty.ToString());
        }
    }
}