using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessSafe.Infrastructure.Attributes;

namespace BusinessSafe.Application.DataTransferObjects
{
    [CoverageExclude]
    public class SiteAddressDto 
    {        
        [Display(Name = "Id")]
        public long? SiteStructureId { get; set; }
        
        [Display(Name = "SiteId")]
        public long SiteId { get; set; }

        [Display(Name = "Site Name:")]
        public string Name { get; set; }

        [Display(Name = "Site Reference:")]
        public string Reference { get; set; }

        [Display(Name = "Site Address:")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string AddressLine5 { get; set; }
        public string County { get; set; }

        [Display(Name = "Postcode:")]
        public string Postcode { get; set; }

        [Display(Name = "Telephone:")]
        public string Telephone { get; set; }
        
        [Display(Name = "Site Contact:")]
        public string SiteContact
        {
            get { return (SiteAddress != null) ? SiteAddress.ContactName : default(string); }
        }

        public ContactDto SiteAddress { get; set; }
        
        public SiteAddressDto()
        {
            
        }

        public SiteAddressDto(long siteId, string addressLine1, string addressLine2, string addressLine3, string addressLine4, string address5, string county, string postCode, string telephone, ContactDto siteAddress)
        {
            SiteId = siteId;   
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            AddressLine3 = addressLine3;
            AddressLine4 = addressLine4;
            AddressLine5 = address5;
            County = county;
            Postcode = postCode;
            Telephone = telephone;
            SiteAddress = siteAddress;
        }
    }
}