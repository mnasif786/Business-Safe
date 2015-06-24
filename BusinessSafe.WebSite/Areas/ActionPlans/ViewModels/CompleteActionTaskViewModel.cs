using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessSafe.WebSite.Areas.ActionPlans.ViewModels
{
    public class CompleteActionTaskViewModel
    {
        public long CompanyId { get; set; }
        public long ActionId { get; set; }
        public long ActionTaskId { get; set; }
        
        public ActionSummaryViewModel ActionSummary { get; set; }

        [StringLength(250)]
        public string CompletedComments { get; set; }
        
        public ViewActionTaskViewModel ActionTask { get; set; }
        
        public CompleteActionTaskViewModel()
        {
            ActionSummary = new ActionSummaryViewModel();
            ActionTask = new ViewActionTaskViewModel();
        }
    }
}

 