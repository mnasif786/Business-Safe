using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessSafe.WebSite.Areas.ActionPlans.ViewModels
{
    public class ActionIndexViewModel
    {      
        public IEnumerable<BusinessSafe.Domain.Entities.Action> Actions { get; set; }
    }
}