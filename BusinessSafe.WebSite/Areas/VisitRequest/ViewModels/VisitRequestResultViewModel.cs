using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessSafe.WebSite.Areas.VisitRequest.ViewModels
{
    public class VisitRequestResultViewModel
    {
        public bool Success { get; set; }
        public string[] Errors { get; set; }
        public long Id { get; set; }       
    }
}