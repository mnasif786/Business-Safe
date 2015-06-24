using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels
{
    public class SelectedEmployeeViewModel
    {
        public Guid EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}