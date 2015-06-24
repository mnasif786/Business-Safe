using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Responsibilities.ViewModels
{
    public class CopyResponsibilityViewModel
    {
        public long ResponsibilityId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
    }
} 