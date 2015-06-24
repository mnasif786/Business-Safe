using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IEmployeeRepository : IRepository<Employee, Guid>
    {
        IEnumerable<Employee> Search(long companyId, string employeeReferenceLike, string fornameLike,
                                     string surnameLike, long[] siteIds, bool showDeleted, int maximumResults,
                                     bool includeSiteless, bool excludeWithActiveUser, string orderBy, bool @ascending);

        Employee GetByIdAndCompanyId(Guid employeeId, long companyId);
        IEnumerable<Employee> GetByTermSearch(string searchTerm, long companyId, int pageLimit);
        IEnumerable<Employee> GetBySite(long siteId);
    }
}