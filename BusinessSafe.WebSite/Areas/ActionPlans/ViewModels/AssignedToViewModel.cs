using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessSafe.WebSite.Areas.ActionPlans.ViewModels
{
    public class AssignedToViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Guid? SelectedAssignedToId { get; set; }
    }
}