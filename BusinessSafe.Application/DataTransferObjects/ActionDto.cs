using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class ActionDto
    {
        public long Id { get; set; }      
        public string Title { get; set; }
        public string AreaOfNonCompliance { get; set; }
        public string ActionRequired { get; set; }
        public string GuidanceNote { get; set; }
        public string TargetTimescale { get; set; }
        public Guid? AssignedTo { get; set; }
        public DateTime? DueDate { get; set; }
        public DerivedTaskStatusForDisplay Status { get; set; }
        public ActionQuestionStatus? QuestionStatus { get; set; }
        public string Reference  { get; set; }
        public ActionCategory Category { get; set; }
        public virtual IEnumerable<ActionTaskDto> ActionTasks { get; set; }
        public ActionPlan ActionPlan { get; set; }
        
        public ActionDto()
        {
            ActionTasks = new List<ActionTaskDto>();
        }
    }
}
