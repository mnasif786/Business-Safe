using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Checklists.ViewModels
{
    public class QuestionViewModel
    {
        public long Id { get; set; }
        public int ListOrder { get; set; }
        public string Text { get; set; }
        public QuestionType QuestionType { get; set; }
        public AnswerViewModel Answer { get; set; }
    }
}
