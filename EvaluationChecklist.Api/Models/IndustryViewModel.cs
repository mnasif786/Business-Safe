using System;
using System.Collections.Generic;

namespace EvaluationChecklist.Models
{
    public class IndustryViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }
}