using System;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.CustomExceptions;

namespace BusinessSafe.Domain.Entities
{
    public class PeopleAtRisk : Entity<long>, ICompanyDefault
    {
        public virtual string Name { get; protected set; }
        public virtual long? CompanyId { get; protected set; }
        public virtual long? RiskAssessmentId { get; protected set; }

        public static PeopleAtRisk Create(string name, long companyId, long? riskAssessmentId, UserForAuditing creatingUser)
        {
            return new PeopleAtRisk
                       {
                           Name = name,
                           CompanyId = companyId,
                           RiskAssessmentId = riskAssessmentId,
                           CreatedOn = DateTime.Now,
                           CreatedBy = creatingUser
                       };
        }

        public virtual void Update(string name, long companyId, long? riskAssessmentId, UserForAuditing modifyingUser)
        {

            if (IsSystemDefault())
            {
                throw new AttemptingToUpdateSystemDefaultException(name);
            }

            Name = name;
            CompanyId = companyId;
            RiskAssessmentId = riskAssessmentId;
            SetLastModifiedDetails(modifyingUser);
        }

        public override void MarkForDelete(UserForAuditing deletingUser)
        {
            if (IsSystemDefault())
            {
                throw new AttemptingToDeleteSystemDefaultException(Name);
            }

            base.MarkForDelete(deletingUser);
        }


        private bool IsSystemDefault()
        {
            return CompanyId.HasValue == false;
        }

        public virtual PeopleAtRisk Clone(UserForAuditing user)
        {
            return new PeopleAtRisk
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