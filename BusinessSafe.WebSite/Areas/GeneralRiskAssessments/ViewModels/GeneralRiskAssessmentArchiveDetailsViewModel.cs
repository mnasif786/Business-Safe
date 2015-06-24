using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels
{
    public class GeneralRiskAssessmentArchiveDetailsViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Reference { get; set; }

        [Display(Name = "Creation Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Assessment Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateOfAssessment { get; set; }

        [Display(Name = "Assessment Date")]
        public string RiskAssessor { get; set; }

        [Display(Name = "Location/Area/Department")]
        public string LocationAreaDepartment { get; set; }

        [Display(Name = "Task/Process Description")]
        public string TaskProcessDescription { get; set; }
        public string Site { get; set; }

        public IEnumerable<Person> Employees { get; set; }

        [Display(Name = "Non-Employees")]
        public IEnumerable<Person> NonEmployees { get; set; }
        public IEnumerable<Hazard> Hazards { get; set; } 

    }

    public class Person
    {
        public string Name { get; set; }
    }

    public class Hazard
    {
        public string Name { get; set; }
        public string Description { get; set; }

        [Display(Name = "Control Measures")]
        public IEnumerable<ControlMeasure> ControlMeasures { get; set; }

        [Display(Name = "Further Control Measures")]
        public IEnumerable<FurtherActionTask> FurtherActionTasks { get; set; }

        [Display(Name = "Date Added to Assessment")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime HazardAddedToRiskAssessment { get; set; }
    }

    public class ControlMeasure
    {
        public string Description { get; set; }

        [Display(Name = "Date Added to Assessment")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime ControlMeasureAddedToRiskAssessment { get; set; }
    }

    public class FurtherActionTask
    {
        public string Reference { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [Display(Name = "Assigned To")]
        public string AssignedTo { get; set; }

        [Display(Name = "Due Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime CompletionDueDate { get; set; }

        [Display(Name = "Date Added to Assessment")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime FurtherActionTaskAddedToRiskAssessment { get; set; }
    }
}