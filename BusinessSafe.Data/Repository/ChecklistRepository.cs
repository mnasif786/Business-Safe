using System.Collections.Generic;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class ChecklistRepository : Repository<Checklist, long>, IChecklistRepository
    {
        public ChecklistRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IEnumerable<Checklist> GetAllUndeleted()
        {
            var result = SessionManager
                .Session
                .CreateCriteria<Checklist>()
                .Add(Restrictions.Eq("Deleted", false))
                .List<Checklist>();

            return result;
        }

        public IEnumerable<Checklist> GetByRiskAssessmentType(ChecklistRiskAssessmentType checklistRiskAssessmentType)
        {
            var result = SessionManager
                 .Session
                 .CreateCriteria<Checklist>()
                 .Add(Restrictions.Eq("Deleted", false))
                 .Add(Restrictions.Eq("ChecklistRiskAssessmentType", checklistRiskAssessmentType))
                 .List<Checklist>();

            return result;
        }

        public Checklist GetFireRiskAssessmentChecklist()
        {
            var result = SessionManager
                 .Session
                 .CreateCriteria<Checklist>()
                 .Add(Restrictions.Eq("Deleted", false))
                 .Add(Restrictions.Eq("Id", 5L ))
                 .UniqueResult<Checklist>();

            return result;
        }
    }
}