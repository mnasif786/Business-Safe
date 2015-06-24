using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace SafeCheckSpike.Models
{
    public class Answer
    {
        public Guid Id { get; set; }

        public int AnswerType { get; set; }

        public string Comment { get; set; }

        public Guid ChecklistId { get; set; }

        public Guid QuestionId { get; set; }
    }

    public enum AnswerType
    {
        Acceptable = 1,
        AttentionRequired = 2,
        Unacceptable = 3,
        NotRequired = 4
    }
}