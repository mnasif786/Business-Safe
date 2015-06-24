using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.WebSite.Areas.Employees.ViewModels
{
    public class EmergencyContactViewModel
    {        
        public int EmergencyContactId { get; set; }
        public string Title { get; set; }
        [Required(ErrorMessage = "Forename is required")]
        public string Forename { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Relationship is required")]
        public string Relationship { get; set; }
        public bool SameAddressAsEmployee { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public int? EmergencyContactCountryId { get; set; }
        public string PostCode { get; set; }
        public string WorkTelephone { get; set; }
        public string HomeTelephone { get; set; }
        public string MobileTelephone { get; set; }
        public int PreferredContactNumber { get; set; }
        public long CompanyId { get; set; }
        public Guid EmployeeId { get; set; }
        
        public string GetPreferredContactNumber()
        {
            if (PreferredContactNumber == 2)
                return HomeTelephone;            

            if (PreferredContactNumber == 3)
                return MobileTelephone;

            return WorkTelephone;
        }
    }
}