using System.Collections.Generic;

namespace EvaluationChecklist.Models
{
    public class ClientQuestionViewModel 
    {
        public long ClientId { get; set; }
        public string ClientAccountNumber { get; set; }
        public List<QuestionViewModel> Questions { get; set; }

        public ClientQuestionViewModel()
        {
            Questions = new List<QuestionViewModel>();
        }
    }
}