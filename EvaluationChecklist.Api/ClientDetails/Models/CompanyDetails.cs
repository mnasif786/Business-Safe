using System.Collections.Generic;
using BusinessSafe.Application.RestAPI.Responses;

namespace EvaluationChecklist.ClientDetails.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CompanyDetails
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CAN { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<SiteDetails> Sites { get; set; }

        public string ClientType { get; set; }
        public string Industry { get; set; }

        public CompanyDetails()
        {
            Id = 0;
            CompanyName = string.Empty;
            CAN = string.Empty;
            Sites = new List<SiteDetails>();
            Contacts = new List<Contact>();
        }
    }
}