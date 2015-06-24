using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.Application.Request
{
    public class SaveCompanyDefaultRequest
    {
        public SaveCompanyDefaultRequest()
        {}

        public SaveCompanyDefaultRequest(bool runMatchCheck)
        {
            RunMatchCheck = runMatchCheck;
        }

        public SaveCompanyDefaultRequest(long id, string name, long companyId, long? riskAssessmentId, bool runMatchCheck, Guid userId)
        {
            Id = id;
            Name = name;
            CompanyId = companyId;
            RiskAssessmentId = riskAssessmentId;
            RunMatchCheck = runMatchCheck;
            UserId = userId;
        }
        
        public long Id { get; private set; }

        [Required(ErrorMessage = "Please enter Name.")]
        public string Name { get; private set; }

        public long CompanyId { get; private set; }
        public long? RiskAssessmentId { get; private set; }
        public bool RunMatchCheck { get; private set; }

        public Guid UserId { get; set; }
    }
}