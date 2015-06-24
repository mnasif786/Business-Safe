namespace BusinessSafe.Application.DataTransferObjects
{
    public class AnswerDto
    {
        public long Id { get; set; }
        public QuestionDto Question { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
