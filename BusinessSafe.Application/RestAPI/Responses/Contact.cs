using System.Runtime.Serialization;

namespace BusinessSafe.Application.RestAPI.Responses
{
    [DataContract(Namespace = "")]
    public class Contact
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string ContactName { get; set; }

        [DataMember]
        public string Email { get; set; }

    }
}