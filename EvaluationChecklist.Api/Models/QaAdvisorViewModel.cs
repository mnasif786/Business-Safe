using System;

namespace EvaluationChecklist.Models
{
    public class QaAdvisorViewModel
    {
        public Guid Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public string Fullname { get; set; }

        public string Initials { get; set; }
    }
}