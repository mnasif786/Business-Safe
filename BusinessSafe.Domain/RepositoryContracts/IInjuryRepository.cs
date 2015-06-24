using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IInjuryRepository : IRepository<Injury, long>
    {
        IEnumerable<Injury> GetAllInjuriesForAccidentRecord(long companyId, long accidentRecord);
    }
}