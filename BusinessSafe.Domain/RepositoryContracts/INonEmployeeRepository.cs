using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface INonEmployeeRepository : IRepository<NonEmployee, long>
    {
        IEnumerable<NonEmployee> GetByTermSearch(string termToSearch, long companyId, int pageLimit);
        IEnumerable<NonEmployee> GetAllByNameSearch(string nameToSearch, long nonEmployeeId, long companyId);
        NonEmployee GetByIdAndCompanyId(long nonEmployeeId, long companyId);
        IEnumerable<NonEmployee> GetAllNonEmployeesForCompany(long companyId);
    }
}