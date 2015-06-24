using BusinessSafe.Domain.Entities;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels
{
    public class FireAnswerViewModel
    {
        public long Id { get; set; }
        public YesNoNotApplicableEnum? YesNoNotApplicableResponse { get; set; }
        public string AdditionalInfo { get; set; }
    }
}