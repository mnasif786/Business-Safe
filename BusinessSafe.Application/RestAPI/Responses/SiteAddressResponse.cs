using System.Runtime.Serialization;

namespace BusinessSafe.Application.RestAPI.Responses
{
    [DataContract(Namespace = "")]
    public class SiteAddressResponse
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string SiteName { get; set; }

        [DataMember]
        public string Address1 { get; set; }

        [DataMember]
        public string Address2 { get; set; }
        
        [DataMember]
        public string Address3 { get; set; }

        [DataMember]
        public string Address4 { get; set; }

        [DataMember]
        public string Address5 { get; set; }

        [DataMember]
        public string County { get; set; }

        [DataMember]
        public string Postcode { get; set; }

        [DataMember]
        public string Telephone { get; set; }

        [DataMember]
        public string Fax { get; set; }

        [DataMember]
        public Contact SiteContact { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool IsArchivedHealthAndSafetySite { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool IsPrincipalHealthAndSafetySite { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool IsAdditionalHealthAndSafetySite { get; set; }

        [DataMember]
        public bool IsMainSite { get; set; }

        public SiteAddressResponse()
        {
            Id = 0;
            Address1 = default(string);
            Address2 = default(string);
            Address3 = default(string);
            Address4 = default(string);
            Address5 = default(string);
            County = default(string);
            Postcode = default(string);
            Telephone = default(string);
            SiteContact = default(Contact);
            SiteName = default(string);

        }
    }
}