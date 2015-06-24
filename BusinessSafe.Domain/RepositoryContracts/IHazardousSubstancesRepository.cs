using System.Collections.Generic;

using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IHazardousSubstancesRepository : IRepository<HazardousSubstance, long>
    {
        IEnumerable<HazardousSubstance> GetForCompany(long companyId);
        IEnumerable<HazardousSubstance> Search(long companyId, long? supplierId, string substanceNameLike, bool showDeleted);
        IEnumerable<HazardousSubstance> GetByTermSearch(string term, long companyId, int pageLimit);
        HazardousSubstance GetByIdAndCompanyId(long hazardousSubstanceId, long companyId);
        HazardousSubstance GetDeletedByIdAndCompanyId(long hazardousSubstanceId, long companyId);
        bool DoesHazardousSubstancesExistForSupplier(long supplierId, long companyId);
    }
}
