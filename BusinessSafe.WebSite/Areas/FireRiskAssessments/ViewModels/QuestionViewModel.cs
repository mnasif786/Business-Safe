namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels
{
    public class QuestionViewModel
    {
        public long Id { get; set; }
        public int ListOrder { get; set; }
        public string Text { get; set; }
        public string Information { get; set; }
        public FireAnswerViewModel Answer { get; set; }
        public bool IsQuestionValid { get; private set; }

        public QuestionViewModel()
        {
            IsQuestionValid = true;
        }

        public void MarkAsInvalid()
        {
            IsQuestionValid = false;
        }
    }
}
