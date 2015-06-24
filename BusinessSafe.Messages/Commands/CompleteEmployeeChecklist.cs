using System;
using System.Collections.Generic;
using NServiceBus;

namespace BusinessSafe.Messages.Commands
{
    [Serializable]
    public class CompleteEmployeeChecklist: ICommand
    {
        public Guid EmployeeChecklistId { get; set; }
        public List<SubmitAnswer> Answers { get; set; }
        public bool CompletedOnBehalf { get; set; }
        public Guid? CompletedOnEmployeesBehalfBy { get; set; }
        public DateTime CompletedDate { get; set; }
    }

    [Serializable]
    public class SubmitAnswer
    {
        public long QuestionId { get; set; }
        public bool? BooleanResponse { get; set; }
        public string AdditionalInfo { get; set; }
    }

    
}
