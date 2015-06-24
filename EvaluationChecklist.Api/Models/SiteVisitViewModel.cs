using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvaluationChecklist.Models
{
    public class SiteVisitViewModel
    {
        public PersonSeenViewModel PersonSeen { get; set; }
        public string AreasVisited { get; set; }
        public string AreasNotVisited { get; set; }
        public string EmailAddress { get; set; }
        public string VisitDate { get; set; }
        public string VisitBy { get; set; }
        public string VisitType { get; set; }
        public ImpressionTypeViewModel SelectedImpressionType { get; set; }
       // public Guid? ImpressionTypeId { get; set; }

    }
}