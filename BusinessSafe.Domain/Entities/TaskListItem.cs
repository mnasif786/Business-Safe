using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSafe.Domain.Entities
{
    public class TaskListItem
    {
        public virtual long Id { get; set; }
        public LookupItem Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public EmployeeName EmployeeAssignedTo { get; set; }
        public LookupItem Site { get; set; }
        public DateTime? CompletionDueDate { get; set; }
        public string CompletedComments { get; set; }
        public DateTimeOffset? CompletedDate { get; set; }
        public EmployeeName CompletedBy { get; set; }
        public virtual DateTime? CreatedOn { get; set; }
        public TaskStatus Status { get; set; }
    }

    public class EmployeeName
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public virtual string FullName
        {
            get
            {
                string fullName = Forename;

                if (!String.IsNullOrEmpty(Forename) && !String.IsNullOrEmpty(Surname))
                    fullName += " ";

                fullName += Surname;
                return fullName;
            }
        }
        public string JobTitle { get; set; }
    }

    public class LookupItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
