using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvaluationChecklist.Models
{
    public class ClientEmployeeViewModel
    {
        public Guid Id { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public string FullName { get; set; }

        public string EmailAddress { get; set; }
    }
}