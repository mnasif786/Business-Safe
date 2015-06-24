using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.Application.Request
{
    public class SaveCompanyHazardDefaultRequest
    {
        public SaveCompanyHazardDefaultRequest()
        { }
        
        public SaveCompanyHazardDefaultRequest(long companyDefaultId, string companyDefaultValue, long companyId, long? riskAssessmentId, bool runMatchCheck, int[] riskAssessmentTypeApplicable, Guid userId)
        {
            Id = companyDefaultId;
            Name = companyDefaultValue;
            CompanyId = companyId;
            RiskAssessmentId = riskAssessmentId;
            RunMatchCheck = runMatchCheck;
            UserId = userId;
            RiskAssessmentTypeApplicable = riskAssessmentTypeApplicable;
        }

        public long Id { get; private set; }

        [Required(ErrorMessage = "Please enter Name.")]
        public string Name { get; private set; }

        public long CompanyId { get; private set; }
        public long? RiskAssessmentId { get; private set; }
        public bool RunMatchCheck { get; private set; }
        public int[] RiskAssessmentTypeApplicable { get; set; }

        public Guid UserId { get; set; }
    }
}