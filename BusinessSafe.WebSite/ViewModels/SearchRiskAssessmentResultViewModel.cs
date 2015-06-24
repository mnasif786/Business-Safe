using System;

namespace BusinessSafe.WebSite.ViewModels
{
    public class SearchRiskAssessmentResultViewModel
    {
        public long Id { get; set; }
        public string Reference { get; set; }
        public string Title { get; set; }
        public string Site { get; set; }
        public string SiteGroup { get; set; }
        public string AssignedTo { get; set; }
        public string Status { get; set; }
        public DateTime? AssessmentDate { get; set; }
        public DateTime? NextReviewDate { get; set; }
        public bool IsDeleted { get; set; }

        public string AssessmentDateFormatted
        {
            get
            {
                if (AssessmentDate.HasValue)
                    return AssessmentDate.Value.ToShortDateString();

                return string.Empty;
            }
        }

        public string NextReviewDateFormatted
        {
            get
            {
                if (NextReviewDate.HasValue)
                    return NextReviewDate.Value.ToShortDateString();

                return string.Empty;
            }
        }

        
    }
}