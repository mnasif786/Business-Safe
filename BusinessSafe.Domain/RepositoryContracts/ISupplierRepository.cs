using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface ISupplierRepository : IRepository<Supplier, long>
    {
        IEnumerable<Supplier> GetByCompanyId(long companyId);
        Supplier GetByIdAndCompanyId(long companyDefaultId, long companyId);
        IEnumerable<Supplier> GetAllByNameSearch(string name, long excludeSupplierId, long companyId, int pageLimit);
    }
}
