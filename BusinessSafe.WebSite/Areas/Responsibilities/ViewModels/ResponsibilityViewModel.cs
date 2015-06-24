using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Responsibilities.ViewModels
{
    public class ResponsibilityViewModel
    {
        public long ResponsibilityId { get; set; }
        public long CompanyId { get; set; }
        public long? CategoryId { get; set; }
        public string Category { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public long? SiteId { get; set; }
        public string Site { get; set; }
        public bool IsCreatorResponsibilityOwner { get; set; }
        public Guid? OwnerId { get; set; }
        public string Owner { get; set; }
        public long? ReasonId { get; set; }
        public string Reason { get; set; }
        public int? FrequencyId { get; set; }
        public string Frequency { get; set; }
        public bool HasMultipleFrequencies { get; set; }
        public bool CreateResponsibilityTask { get; set; }
        public bool ShowCreateResponsibilityTaskDialogOnLoad { get; set; }
        public IEnumerable<AutoCompleteViewModel> Sites { get; set; }
        public IEnumerable<AutoCompleteViewModel> Employees { get; set; }
        public IEnumerable<AutoCompleteViewModel> FrequencyOptions { get; set; }
        public IEnumerable<AutoCompleteViewModel> Categories { get; set; }
        public IEnumerable<AutoCompleteViewModel> Reasons { get; set; }
        public IEnumerable<ResponsibilityTasksViewModel> ResponsibilityTasks { get; set; }
        public IEnumerable<ResponsibilityTasksViewModel> CompletedResponsibilityTasks { get; set; }

        public ResponsibilityViewModel()
        {
            ResponsibilityTasks = new List<ResponsibilityTasksViewModel>();           
        }

        public string GetPostAction()
        {
            return IsExistingResponsibility() ? "Update" : "Create";
        }

        public bool IsExistingResponsibility()
        {
            return ResponsibilityId != default(long);
        }

        public string GetOwnerText()
        {
            var ownerText = "If not, please select an employee:";
            if (IsExistingResponsibility())
            {
                ownerText = "Responsibility Owner:";
            }
            return ownerText;
        }
    }
}