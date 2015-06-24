using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.WebSite.Areas.ActionPlans.ViewModels
{
    public class SearchActionResultViewModel
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public string AreaOfNonCompliance { get; set; }
        public string ActionRequired { get; set; }
        public string GuidanceNote { get; set; }
        public string TargetTimescale { get; set; }
        public Guid? AssignedTo { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; }
        public string QuestionStatus { get; set; }

        public string DueDateFormatted
        {
            get
            {
                return (DueDate.HasValue) ? DueDate.Value.ToShortDateString() : string.Empty;
            }
        }

        public bool HasTask { get; set; }
    }
}