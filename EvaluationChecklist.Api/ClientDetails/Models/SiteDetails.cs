using System.Runtime.Serialization;
using BusinessSafe.Application.RestAPI.Responses;

namespace EvaluationChecklist.ClientDetails.Models
{
    [DataContract(Namespace = "")]
    public class SiteDetails: SiteAddressResponse
    {
        public SiteDetails()
        {
            Checklist = new ChecklistDetails();
        }
        [DataMember]
        public ChecklistDetails Checklist { get; set; }
    }
}