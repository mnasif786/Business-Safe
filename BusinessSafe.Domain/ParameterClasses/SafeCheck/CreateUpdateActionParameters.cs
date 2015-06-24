using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.Entities.SafeCheck;

namespace BusinessSafe.Domain.ParameterClasses.SafeCheck
{
    public class CreateUpdateActionParameters
    {
        public long ActionPlanId { get; set; }
        public string Reference { get; set; }
        public string AreaOfNonCompliance { get; set; }
        public string ActionRequired { get; set; }
        public string GuidanceNotes { get; set; }
        public Timescale TimeScale { get; set; }
        public Employee AssignedTo { get; set; }
        public ActionPlanStatus Status { get; set; }
        public DateTime DueDate { get; set; }
        public ActionCategory Category { get; set; }
        public string Title { get; set; }

        public DateTime? CreatedOn { get; set; }
        public UserForAuditing CreatedBy { get; set; }
        
        public DateTime? SubmittedOn { get; set; }
        public UserForAuditing SubmittedBy { get; set; }
        
        public UserForAuditing LastModifiedBy { get; set; }
        public virtual DateTime? LastModifiedOn { get; set; }
    }
}
