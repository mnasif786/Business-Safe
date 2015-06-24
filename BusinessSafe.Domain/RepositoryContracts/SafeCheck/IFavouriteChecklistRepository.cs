using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities.SafeCheck;

namespace BusinessSafe.Domain.RepositoryContracts.SafeCheck
{
    public interface IFavouriteChecklistRepository : IRepository<FavouriteChecklist, Guid>
    {
        FavouriteChecklist Get(Guid checklistId, string user);
        FavouriteChecklist Get(Guid checklistId, string user, bool deleted);

        IEnumerable<FavouriteChecklist> GetByUser(string user, bool deleted);
    }
}
