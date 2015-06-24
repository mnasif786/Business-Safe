using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Models;

namespace BusinessSafe.WebSite.ViewModels
{
    public class RiskAssessmentReviewsViewModel
    {
        public long CompanyId { get; set; }
        public long RiskAssessmentId { get; set; }
        public IEnumerable<RiskAssessmentReviewDto> RiskAssessmentReviews { get; set; }
        public bool CanAddReview { get; set; }
        public RiskAssessmentType RiskAssessmentType { get; set; }

        //public bool CanAddReview(IPrincipal user, IEnumerable<RiskAssessmentReviewDto> riskAssessmentReviews)
        //{
        //    if (!user.IsInRole(Permissions.EditRiskAssessmentTasks.ToString()))
        //        return false;

        //    if (riskAssessmentReviews.Any(r => r.CompletedDate == null || r.CompletedDate.Equals(string.Empty)))
        //        return false;

        //    return true;
        //}

        public string GetCompletedReviewDate(RiskAssessmentReviewDto review)
        {
            return review.CompletedDate.HasValue ? review.CompletedDate.Value.ToShortDateString() : string.Empty;
        }
    }
}