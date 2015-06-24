using System;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class QuestionDto
    {
        public long Id { get; set; }
        public QuestionType QuestionType { get; set; }
        public int ListOrder { get; set; }
        public bool IsRequired { get; set; }
        public string Text { get; set; }
        public string Information { get; set; }
    }
}
