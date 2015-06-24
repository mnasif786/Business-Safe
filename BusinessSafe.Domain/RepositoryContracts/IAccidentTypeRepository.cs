using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IAccidentTypeRepository : IRepository<AccidentType, long>
    {
        IEnumerable<AccidentType> GetAllForCompany(long companyId);
    }
}