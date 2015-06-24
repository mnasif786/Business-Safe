using System.ComponentModel.DataAnnotations;
using System;

namespace BusinessSafe.Application.Request
{
    public class CreateUpdateSiteRequest 
    {
        public long Id { get; set; }

        public long? SiteId { get; set; }
        public long? ParentId { get; set; }
        public long ClientId { get; set; }

        [Required(ErrorMessage = "Please enter Name")]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Reference { get;  set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string AddressLine5 { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public string Telephone { get; set; }       
        public string SiteContact { get; set; }
        public long LinkToSiteId { get; set; }
        public long LinkToSiteGroupId { get; set; }
        public Guid CurrentUserId { get; set; }
        public bool SiteClosed { get; set; }
        public bool IsSiteOpenRequest { get; set; }
        public bool IsSiteClosedRequest { get; set; }

        public CreateUpdateSiteRequest()
        {}

        public CreateUpdateSiteRequest(long id, long? siteId, long? parentId, long clientId, string name, string reference, string addressLine1, string addressLine2, string addressLine3, string addressLine4, string addressLine5, string county, bool siteClosed)
        {
            Id = id;
            SiteId = siteId;
            ParentId = parentId;
            ClientId = clientId;
            Name = name;
            Reference = reference;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            AddressLine3 = addressLine3;
            AddressLine4 = addressLine4;
            AddressLine5 = addressLine5;
            County = county;
            SiteClosed = siteClosed;
        }
    }
}