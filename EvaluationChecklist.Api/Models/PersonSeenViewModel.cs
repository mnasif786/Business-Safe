using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvaluationChecklist.Models
{
    public class PersonSeenViewModel
    {
        public string JobTitle { get; set; }
        public string Name { get; set; }
        public string Salutation { get; set; }
        public Guid Id { get; set; }
    }
}
