using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class SourceOfFuel : Entity<long>, ICompanyDefault
    {
        public virtual string Name { get; set; }
        public virtual long? CompanyId { get; set; }
        public virtual long? RiskAssessmentId { get; set; }

        public static SourceOfFuel Create(string name, long companyId, long? riskAssessmentId, UserForAuditing user)
        {
            return new SourceOfFuel
                       {
                           Name = name,
                           CompanyId = companyId,
                           RiskAssessmentId = riskAssessmentId,
                           CreatedOn = DateTime.Now,
                           CreatedBy = user
                       };
        }

        public virtual SourceOfFuel Clone(UserForAuditing user)
        {
            return new SourceOfFuel
            {
                Name = Name,
                CompanyId = CompanyId,
                RiskAssessmentId = null,
                CreatedOn = DateTime.Now,
                CreatedBy = user
            };
        }
    }
}