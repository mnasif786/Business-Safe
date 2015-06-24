using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class CompanyDetailsDto
    {
        [Display(Name = "Id")]
        [Required]
        public long Id { get; private set; }

        [Display(Name = "Company Name:")]
        [StringLength(100)]
        public string CompanyName { get; private set; }

        [Display(Name = "CAN:")]
        [Required]
        [StringLength(10)]
        public string CAN { get; private set; }

        [Display(Name = "Main Address:")]
        [Required]
        public long MainSiteId { get; private set; }

        [Required]
        public string AddressLine1 { get; private set; }
        public string AddressLine2 { get; private set; }
        public string AddressLine3 { get; private set; }
        public string AddressLine4 { get; private set; }

        [Display(Name = "Postcode:")]
        public string PostCode { get; private set; }

        [Display(Name = "Telephone:")]
        public string Telephone { get; private set; }

        [Display(Name = "Website:")]
        public string Website { get; private set; }

        [Display(Name = "Main Contact:")]
        public string MainContact { get; private set; }

        public CompanyDetailsDto()
        {

        }

        public CompanyDetailsDto(long id, string companyName, string can, string addressLine1, string addressLine2, string addressLine3, string addressLine4, long mainSiteId, string postCode, string telephone, string website, string mainContact)
        {            
            Id = id;
            CompanyName = companyName;
            CAN = can;
            MainSiteId = mainSiteId;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            AddressLine3 = addressLine3;
            AddressLine4 = addressLine4;
            PostCode = postCode;
            Telephone = telephone;
            Website = website;
            MainContact = mainContact;
        }
    }
}