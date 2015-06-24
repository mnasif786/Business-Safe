using System;

namespace BusinessSafe.WebSite.Areas.ActionPlans.ViewModels
{
    public class AssignActionPlanTaskViewModel
    {
        public long ActionId { get; set; }
        public Guid? AssignedTo { get; set; }
        public string DueDate { get; set; }
    }
}