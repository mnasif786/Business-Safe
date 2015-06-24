using System.Collections.Generic;
using System.Web;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Employees.ViewModels
{
    public class EmergencyContactDetailViewModel
    {
        public EmergencyContactViewModel ContactDetail { get; set; }
        public IEnumerable<AutoCompleteViewModel> Titles { get; set; }
        public IEnumerable<AutoCompleteViewModel> Countries { get; set; }
        public bool SameAddressAsEmployee { get; set; }
        public string EmployeeAddress1 { get; set; }
        public string EmployeeAddress2 { get; set; }
        public string EmployeeAddress3 { get; set; }
        public string EmployeeTown { get; set; }
        public string EmployeeCounty { get; set; }
        public int? EmployeeCountryId { get; set; }
        public string EmployeePostCode { get; set; }

        public EmergencyContactDetailViewModel()
        {
            ContactDetail = new EmergencyContactViewModel();
        }

        public static EmergencyContactDetailViewModel CreateFrom(EmergencyContactViewModel emergencyContact, EmployeeViewModel employeeViewModel)
        {
            var contactDetail = emergencyContact ?? new EmergencyContactViewModel();
            
            return new EmergencyContactDetailViewModel()
                       {
                           ContactDetail = contactDetail,
                           Titles = employeeViewModel.Titles,
                           Countries =employeeViewModel.Countries,
                           SameAddressAsEmployee = emergencyContact != null ? emergencyContact.SameAddressAsEmployee : false,
                           EmployeeAddress1 = employeeViewModel.Address1,
                           EmployeeAddress2 = employeeViewModel.Address2,
                           EmployeeAddress3 = employeeViewModel.Address3,
                           EmployeeTown = employeeViewModel.Town,
                           EmployeeCounty = employeeViewModel.County,
                           EmployeeCountryId = employeeViewModel.CountryId,
                           EmployeePostCode = employeeViewModel.Postcode
                       };
        }

        public HtmlString IsPreferredContactNumber(string value)
        {
            if(ContactDetail.PreferredContactNumber.ToString() == value)
                return new HtmlString("checked='checked'");

            return new HtmlString("");
        }

        public string GetSaveButtonText()
        {
            if (ContactDetail.EmergencyContactId == 0)
                return "Add";

            return "Update";
        }

        //public bool IsSameEmployeeAddress()
        //{
        //    if (ContactDetail.EmergencyContactId == 0)
        //        return true;

        //    return ContactDetail.SameAddressAsEmployee;
        //}
    }
}