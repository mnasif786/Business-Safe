using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.CustomExceptions;

namespace BusinessSafe.Domain.Entities
{
    public class Hazard : Entity<long>, ICompanyDefault
    {
        public virtual string Name { get; set; }
        public virtual long? CompanyId { get; protected set; }
        public virtual IList<HazardType> HazardTypes { get; set; }
        public virtual RiskAssessment RiskAssessment { get; set; }
        
        public Hazard()
        {
            HazardTypes = new List<HazardType>();
        }

        public static Hazard Create(string name, long companyId, UserForAuditing creatingUser, IList<HazardType> hazardTypes, RiskAssessment riskAssessment)
        {
            return new Hazard
            {
                Name = name,
                CompanyId = companyId,
                HazardTypes = hazardTypes,
                CreatedOn = DateTime.Now,
                CreatedBy = creatingUser,
                RiskAssessment = riskAssessment
            };
        }

        public virtual long? RiskAssessmentId
        {
            get
            {
                if (RiskAssessment == null) return null;
                return RiskAssessment.Id;
            }
        }

        public virtual void Update(string name, long companyId, UserForAuditing updatingUser, IList<HazardType> hazardTypes)
        {

            if (IsSystemDefault())
            {
                throw new AttemptingToUpdateSystemDefaultException(name);
            }

            Name = name;
            CompanyId = companyId;
            HazardTypes = hazardTypes;

            SetLastModifiedDetails(updatingUser);
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
    }
}