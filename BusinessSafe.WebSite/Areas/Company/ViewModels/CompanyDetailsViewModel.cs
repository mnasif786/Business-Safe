
using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.WebSite.Areas.Company.ViewModels
{
    public class CompanyDetailsViewModel
    {
        public string BusinessSafeContact { get; set; }
        public Guid? BusinessSafeContactId { get; set; }

        [Display(Name = "Id")]
        [Required]
        public long Id { get; set; }

        [Display(Name = "Company Name:")]
        [StringLength(100)]
        [Required]
        public string CompanyName { get; set; }

        [Display(Name = "CAN:")]
        [Required]
        [StringLength(10)]
        public string CAN { get; set; }

        [Display(Name = "Main Address:")]
        [Required]
        public long MainSiteId { get; set; }

        [Display(Name = "Address Line 1")]
        [Required]
        [StringLength(500)]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        [StringLength(500)]
        public string AddressLine2 { get; set; }

        [Display(Name = "Address Line 3")]
        [StringLength(500)]
        public string AddressLine3 { get; set; }

        [Display(Name = "Address Line 4")]
        [StringLength(500)]
        public string AddressLine4 { get; set; }

        [Display(Name = "Postcode:")]
        [Required]
        [StringLength(10)]
        public string PostCode { get; set; }

        [Display(Name = "Telephone:")]
        [StringLength(20)]
        public string Telephone { get; set; }

        [Display(Name = "Website:")]
        [StringLength(500)]
        public string Website { get; set; }

        [Display(Name = "Main Contact:")]
        [StringLength(500)]
        [Required]
        public string MainContact { get; set; }
        
    }
}