using System;
using System.ComponentModel.DataAnnotations;
using BusinessSafe.WebSite.CustomValidators;
using BusinessSafe.WebSite.Models;

namespace BusinessSafe.WebSite.ViewModels
{
    public class CompleteReviewViewModel
    {
        [Required]
        public RiskAssessmentType RiskAssessmentType { get; set; }

        [Required]
        public long CompanyId { get; set; }

        [Required]
        public long RiskAssessmentId { get; set; }

        public string RiskAssessmentTitle { get; set; }
        public string RiskAssessmentReference { get; set; }

        [Required]
        public long RiskAssessmentReviewId { get; set; }

        [MustBeTrue(ErrorMessage = "The risk assessment review must be marked as completed.")]
        public bool IsComplete { get; set; }

        [Required(ErrorMessage = "Please enter your comments.")]
        [MaxLength(500, ErrorMessage = "Comments can not be greater than 500 characters")]
        public string CompletedComments { get; set; }

        public DateTime? NextReviewDate { get; set; }
        public bool Archive { get; set; }
        public bool HasUncompletedTasks { get; set; }
    }
}