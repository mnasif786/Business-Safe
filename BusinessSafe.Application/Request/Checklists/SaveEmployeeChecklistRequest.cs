using System;
using System.Collections.Generic;

namespace BusinessSafe.Application.Request.Checklists
{
    public class SaveEmployeeChecklistRequest
    {
        public Guid EmployeeChecklistId { get; set; }
        public IList<SubmitAnswerRequest> Answers { get; set; }
    }
}
