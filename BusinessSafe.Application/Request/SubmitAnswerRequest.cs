namespace BusinessSafe.Application.Request
{
    public class SubmitAnswerRequest
    {
        public long QuestionId { get; set; }
        public bool? BooleanResponse { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
