using System;

namespace EvaluationChecklist.Models
{
    public class QuestionAnswerViewModel
    {
        public QuestionViewModel Question { get; set; }
        public AnswerViewModel Answer { get; set; }

        public int QuestionNumber { get; set; }
        public int CategoryNumber { get; set; }
    }
}