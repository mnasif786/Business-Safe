using System;
using System.Collections.Generic;

namespace BusinessSafe.Application.Request.Checklists
{
    public class CompleteEmployeeChecklistRequest
    {
        public Guid EmployeeChecklistId { get; set; }
        public IList<SubmitAnswerRequest> Answers { get; set; }
        public bool CompletedOnBehalf { get; set; }
        public Guid? CompletedOnEmployeesBehalfBy { get; set; }
        public DateTime CompletedDate { get; set; }
    }
}
