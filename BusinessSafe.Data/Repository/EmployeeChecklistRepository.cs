using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using BusinessSafe.Domain.ParameterClasses;
using NHibernate.Transform;

namespace BusinessSafe.Data.Repository
{
    public class EmployeeChecklistRepository : Repository<EmployeeChecklist, Guid>, IEmployeeChecklistRepository
    {
        public EmployeeChecklistRepository(IBusinessSafeSessionManager sessionManager) : base(sessionManager) { }

        public IEnumerable<EmployeeChecklist> GetByPersonalRiskAssessmentId(long riskAssessmentId)
        {
            return SessionManager
                .Session
                .CreateCriteria<EmployeeChecklist>()
                .Add(Restrictions.Eq("PersonalRiskAssessment.Id", riskAssessmentId))
                .Add(Restrictions.Eq("Deleted", false))
                .SetMaxResults(1000)
                .List<EmployeeChecklist>();
        }

        public EmployeeChecklist GetByIdAndRiskAssessmentId(Guid employeeChecklistId, long riskAssessmentId)
        {
            var result = SessionManager
                           .Session
                           .CreateCriteria<EmployeeChecklist>()
                           .Add(Restrictions.Eq("Id", employeeChecklistId))
                           .Add(Restrictions.Eq("PersonalRiskAssessment.Id", riskAssessmentId))
                           .Add(Restrictions.Eq("Deleted", false))
                           .SetMaxResults(1)
                           .UniqueResult<EmployeeChecklist>();
            if (result == null)
                throw new EmployeeChecklistNotFoundException(employeeChecklistId);

            // TODO : Think we can get rid of this!!!
            
            //if (result.LastModifiedBy != null && result.LastModifiedBy.Site != null && result.LastModifiedBy.Site.Id == 0)
            //{
                //load site to prevent the error, "collection [BusinessSafe.Domain.Entities.SiteStructureElement.Children] was not processed by flush() " 
            //    NHibernateUtil.Initialize(result.LastModifiedBy);
            //}

            return result;
        }

        public IEnumerable<ExistingReferenceParameters> GetExistingReferencesForPrefixes(IEnumerable<string> prefixes)
        {
            return SessionManager
                .Session
                .CreateCriteria<EmployeeChecklist>()
                .Add(Restrictions.In("ReferencePrefix", prefixes.ToList()))
                .SetProjection(Projections.ProjectionList()
                    .Add(Projections.Alias(Projections.GroupProperty("ReferencePrefix"), "Prefix"))
                    .Add(Projections.Alias(Projections.Max("ReferenceIncremental"), "MaxIncremental")))
                .SetResultTransformer(Transformers.AliasToBean<ExistingReferenceParameters>())
                .List<ExistingReferenceParameters>();
        }
    }
}
