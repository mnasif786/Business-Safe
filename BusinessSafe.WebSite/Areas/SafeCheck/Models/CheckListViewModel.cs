using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.WebSite.Areas.SafeCheck.Models
{   
    public class AnswerViewModel
    {
        public List<string> PossibleResponses { get; set; }
        public int SelectedResponse { get; set; }

        public string Comment { get; set; }
    }

    public class QuestionViewModel
    {
        public string Text { get; set; }
        public AnswerViewModel Answer { get; set; }
    }

    public class CheckListViewModel
    {
        // Checklist
        public string CompanyName { get;  set; }  

        public List<QuestionViewModel> Questions { get;  set; }  
    }
}