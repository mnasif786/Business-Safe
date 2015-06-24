using System.Collections.Generic;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Data.Common;
using NHibernate;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using NHibernate.Criterion;

namespace BusinessSafe.Data.Repository
{
    public class HazardousSubstancesRepository : Repository<HazardousSubstance, long>, IHazardousSubstancesRepository
    {
        public HazardousSubstancesRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IEnumerable<HazardousSubstance> GetForCompany(long companyId)
        {
            return SessionManager
                .Session
                .CreateCriteria<HazardousSubstance>()
                .Add(Restrictions.Eq("CompanyId", companyId))
                .Add(Restrictions.Eq("CompanyId", companyId))
                .List<HazardousSubstance>();
        }

        public IEnumerable<HazardousSubstance> Search(
            long companyId,
            long? supplierId,
            string substanceNameLike,
            bool showDeleted)
        {
            var criteria = SessionManager.Session
                .CreateCriteria<HazardousSubstance>()
                .SetFetchMode("HazardousSubstanceRiskAssessments", FetchMode.Join)
                .Add(Restrictions.Eq("CompanyId", companyId));

            if (supplierId.HasValue)
            {
                criteria.CreateAlias("Supplier", "supplier");
                criteria.Add((Restrictions.Eq("supplier.Id", supplierId.Value)));
            }

            if (!string.IsNullOrEmpty(substanceNameLike))
            {
                // search checks against the search term's individual words now; according to HSA07
                var parts = substanceNameLike.Split(' ');
                var disjunction = Restrictions.Disjunction();
                foreach(var part in parts)
                {
                    disjunction.Add(Restrictions.Like("Name", "%" + part + "%"));
                }
                disjunction.Add(Restrictions.Like("Name", "%" + substanceNameLike + "%"));
                criteria.Add(disjunction);
            }

            if (showDeleted)
            {
                criteria.Add(Restrictions.Eq("Deleted", true));
                criteria.AddOrder(new Order("LastModifiedOn", false));
            }
            else
            {
                criteria.Add(Restrictions.Eq("Deleted", false));
                criteria.AddOrder(new Order("Name", true));
            }

            return criteria.SetResultTransformer(new DistinctRootEntityResultTransformer()).List<HazardousSubstance>();
        }

        public IEnumerable<HazardousSubstance> GetByTermSearch(string searchTerm, long companyId, int pageLimit)
        {
            var nameCriteria = Restrictions.Or(
                                                   Restrictions.InsensitiveLike("Name", searchTerm + "%"),
                                                   Restrictions.InsensitiveLike("Name", "%" + searchTerm));

            return SessionManager
                           .Session
                           .CreateCriteria<HazardousSubstance>()
                           .Add(Restrictions.Eq("CompanyId", companyId))
                           .Add(Restrictions.Eq("Deleted", false))
                           .Add(nameCriteria)
                           .SetMaxResults(pageLimit)
                           .List<HazardousSubstance>();
        }

        public HazardousSubstance GetByIdAndCompanyId(long id, long companyId)
        {
            var result = SessionManager
                    .Session
                    .CreateCriteria<HazardousSubstance>()
                    .Add(Restrictions.Eq("Id", id))
                    .Add(Restrictions.Eq("CompanyId", companyId))
                    .Add(Restrictions.Eq("Deleted", false))
                    .SetMaxResults(1)
                    .UniqueResult<HazardousSubstance>();

            if (result == null)
                throw new HazardousSubstanceNotFoundException(id);
            return result;
        }

        public HazardousSubstance GetDeletedByIdAndCompanyId(long id, long companyId)
        {
            var result = SessionManager
                    .Session
                    .CreateCriteria<HazardousSubstance>()
                    .Add(Restrictions.Eq("Id", id))
                    .Add(Restrictions.Eq("CompanyId", companyId))
                    .Add(Restrictions.Eq("Deleted", true))
                    .SetMaxResults(1)
                    .UniqueResult<HazardousSubstance>();

            if (result == null)
                throw new HazardousSubstanceNotFoundException(id);
            return result;
        }

        public bool DoesHazardousSubstancesExistForSupplier(long supplierId, long companyId)
        {
            var hazardousSubstances = (int)SessionManager
                                         .Session
                                         .CreateCriteria<HazardousSubstance>()
                                         .Add(Restrictions.Eq("CompanyId", companyId))
                                         .Add(Restrictions.Eq("Deleted", false))
                                         .Add(Restrictions.Eq("Supplier.Id", supplierId))
                                         .SetMaxResults(1)
                                         .SetProjection(Projections.Count("Id")).UniqueResult();
            return hazardousSubstances > 0;
        }
    }
}
