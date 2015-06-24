using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.ActionPlans.ViewModels
{
    public class SearchActionsResultViewModel
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public string AreOfNonCompliance { get; set; }
        public string ActionRequired { get; set; }
        public string GuidanceNotes { get; set; }
        public string TargetTimescales { get; set; }
        public string Status { get; set; }
        public long AssignedToId { get; set; }
        public DateTime DueDate { get; set; }

        public string DueDateFormatted
        {
            get
            {
                return DueDate.ToShortDateString();
            }
        }
    }
}
