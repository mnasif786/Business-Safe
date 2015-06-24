using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessSafe.WebSite.Areas.Responsibilities.ViewModels.Wizard
{
    public class FrequencyViewModel
    {
        public long Id { get; set; }
        public SelectList Frequencies { get; set; }
    }
}