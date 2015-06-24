using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities.SafeCheck;

namespace BusinessSafe.Domain.RepositoryContracts.SafeCheck
{
    public interface ICheckListRepository : IRepository<Checklist, Guid>
    {
        IEnumerable<Checklist> GetByClientId(long clientId);
        IEnumerable<Checklist> GetByClientId(long clientId, bool includeDeleted);
        IEnumerable<Checklist> Search(int? clientId, string createdBy, string visitDate, string status, bool includeDeleted, Guid? QaAdvisorId);
        IList<string> GetDistinctCreatedBy();
    }
}