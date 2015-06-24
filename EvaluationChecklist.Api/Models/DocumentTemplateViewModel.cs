using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EvaluationChecklist.Models
{
    public class DocumentTemplateViewModel
    {
        public string CoverLetterTemplate { get; set; }

        public string Address { get; set; }

        public string AreaVisited { get; set; }

        public string AreaNotVisited { get; set; }

        public string PersonSeen { get; set; }
        
        public DateTime DateVisited { get; set; }

        public ChecklistViewModel CheckList { get; set; }

        public string Notes { get; set; }
    }

    public class DocumentTemplateQuestionAnswerViewModel
    {

    }


}