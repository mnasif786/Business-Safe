using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Request.FireRiskAssessments
{
    public class SubmitFireAnswerRequest
    {
        public long QuestionId { get; set; }
        public YesNoNotApplicableEnum? YesNoNotApplicableResponse { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
