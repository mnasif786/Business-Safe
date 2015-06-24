using System;

namespace BusinessSafe.Domain.Entities
{
    public class TaskHistoryRecord
    {
        public DateTime DueDate { get; set; }
        public string CompletedDate { get; set; }
        public string CompletedBy { get; set; }
    }
}