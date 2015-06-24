using System.Runtime.Serialization;

namespace BusinessSafe.Application.RestAPI.Responses
{
    [DataContract(Namespace = "")]
    public partial class CompanyDetailsResponse
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public string CAN { get; set; }

        [DataMember]
        public string Website { get; set; }

        [DataMember]
        public Contact MainContact { get; set; }

        [DataMember]
        public SiteAddressResponse MainSiteAddress { get; set; }

        [DataMember]
        public string Industry { get; set; }

        public CompanyDetailsResponse()
        {
            Id = 0;
            CompanyName = string.Empty;
            CAN = string.Empty;
            Website = string.Empty;
            MainContact = new Contact();
            MainSiteAddress = new SiteAddressResponse();
            Industry = string.Empty;
        }
    }

    public partial class CompanyDetailsResponse
    {
        public static CompanyDetailsResponse Empty
        {
            get
            {
                return new CompanyDetailsResponse();
            }
        }
    }
}

