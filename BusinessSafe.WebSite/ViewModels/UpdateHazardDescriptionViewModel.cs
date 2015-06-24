﻿using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.WebSite.ViewModels
{
    public class UpdateHazardTitleViewModel
    {
        public long RiskAssessmentId { get; set; }
        public long RiskAssessmentHazardId { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        [StringLength(300, ErrorMessage = "Description can not be greater than 300 characters")]
        public string Title { get; set; }
    }
}