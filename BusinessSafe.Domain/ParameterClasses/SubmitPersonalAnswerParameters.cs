using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.ParameterClasses
{
    public class SubmitPersonalAnswerParameters
    {
        public Question Question { get; set; }
        public virtual bool? BooleanResponse { get; set; }
        public virtual string AdditionalInfo { get; set; }
    }
}
