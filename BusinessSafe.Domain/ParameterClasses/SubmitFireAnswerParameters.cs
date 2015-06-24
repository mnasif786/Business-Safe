using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.ParameterClasses
{
    public class SubmitFireAnswerParameters
    {
        public Question Question { get; set; }
        public virtual YesNoNotApplicableEnum? YesNoNotApplicableResponse { get; set; }
        public virtual string AdditionalInfo { get; set; }
    }
}
