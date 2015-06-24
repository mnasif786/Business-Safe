using System.Collections.Generic;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class SignificantFindingDto
    {
        public long Id { get; set; }
        public FireAnswerDto FireAnswer { get; set; }
        public IEnumerable<TaskDto> FurtherActionTasks { get; set; } 
    }
}
