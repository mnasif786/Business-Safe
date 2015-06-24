using System.Runtime.Serialization;

namespace BusinessSafe.Application.RestAPI.Responses
{
    [DataContract(Namespace = "")]
    public class Links
    {
        [DataMember]
        public string All { get; set; }
 
        [DataMember]
        public string[] Urls { get; set; }
    }
}