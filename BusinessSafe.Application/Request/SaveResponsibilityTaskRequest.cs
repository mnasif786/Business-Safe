using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.Application.Request
{
    public class SaveResponsibilityTaskRequest
    {
        public long Id { get; set; }
        [Display(Name="Ref")]
        [StringLength(50)]
        public string Reference { get;  set; }
        [Required(ErrorMessage="Please enter Task Title.")]
        [StringLength(200)]
        public string Title { get;  set; }
        [Display(Name = "Assigned To")]
        public Guid? AssignedToId { get;  set; }
        [Display(Name = "Task Category")]
        public long TaskCategoryId { get;  set; }
        [StringLength(500)]
        public string Description { get;  set; }
        [Required(ErrorMessage = "Please enter Completion Due Date.")]
        [Display(Name = "Completion Due Date")]
        [DisplayFormat(ApplyFormatInEditMode= true, DataFormatString ="{0:MM/dd/yyyy}")]
        public DateTime CompletionDueDate { get;  set; }
        public bool Urgent { get;  set; }
        public DateTime? CompletionDate { get; set; }
        
        public SaveResponsibilityTaskRequest()
        {
            CompletionDueDate = DateTime.Now.AddHours(2);
        }

        public SaveResponsibilityTaskRequest(string taskRef, string title, Guid? assignedTo, string description, long taskCategoryId,
            DateTime completionDueDate, bool urgent, DateTime? completionDate)
        {            
            Reference = taskRef;
            Title = title;
            AssignedToId = assignedTo;
            TaskCategoryId = taskCategoryId;
            Description = description;
            CompletionDueDate = completionDueDate;
            Urgent = urgent;
            CompletionDate = completionDate;
        }

        
    }
}