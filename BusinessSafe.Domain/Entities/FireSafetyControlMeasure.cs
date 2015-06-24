using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class FireSafetyControlMeasure: Entity<long>, ICompanyDefault
    {
        public virtual string Name { get; set; }
        public virtual long? CompanyId { get; set; }
        public virtual long? RiskAssessmentId{ get; set; }

        public static FireSafetyControlMeasure Create(string name, long companyId, long? riskAssessmentId, UserForAuditing user)
        {
            return new FireSafetyControlMeasure
            {
                Name = name,
                CompanyId = companyId,
                RiskAssessmentId = riskAssessmentId,
                CreatedOn = DateTime.Now,
                CreatedBy = user
            };
        }

    }
}