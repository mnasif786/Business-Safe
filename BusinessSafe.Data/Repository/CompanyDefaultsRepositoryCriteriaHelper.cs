using NHibernate.Criterion;

namespace BusinessSafe.Data.Repository
{
    public static class CompanyDefaultsRepositoryCriteriaHelper
    {
        public static DetachedCriteria GetCompanyDefaultExistsDetachedCriteria<T>(long id, long companyId, string nameToSearch)
        {

            var clientCriteria = DetachedCriteria.For(typeof(T));
            clientCriteria
                .Add(Restrictions.Not(Restrictions.Eq("Id", id)))
                .Add(Restrictions.Eq("CompanyId", companyId))
                .Add(Restrictions.Eq("Deleted", false))
                .Add(Restrictions.Or(
                    Restrictions.InsensitiveLike("Name", nameToSearch + "%"),
                    Restrictions.InsensitiveLike("Name", "%" + nameToSearch))
                )
                .SetMaxResults(100);
            return clientCriteria;
        }

    }
}