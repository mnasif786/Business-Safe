using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class FireAnswerDto : AnswerDto
    {
        public virtual YesNoNotApplicableEnum? YesNoNotApplicableResponse { get; set; }
        public virtual FireRiskAssessmentChecklistDto FireRiskAssessmentChecklist { get; set; }
    }
}
