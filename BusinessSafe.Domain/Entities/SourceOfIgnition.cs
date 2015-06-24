using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class SourceOfIgnition : Entity<long>, ICompanyDefault
    {
        public virtual string Name { get; set; }
        public virtual long? CompanyId { get; set; }
        public virtual long? RiskAssessmentId { get; set; }

        public static SourceOfIgnition Create(string name, long companyId, long? riskAssessmentId, UserForAuditing user)
        {
            return new SourceOfIgnition
                       {
                           Name = name,
                           CompanyId = companyId,
                           RiskAssessmentId = riskAssessmentId,
                           CreatedOn = DateTime.Now,
                           CreatedBy = user
                       };
        }

        public virtual SourceOfIgnition Clone(UserForAuditing user)
        {
            return new SourceOfIgnition
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