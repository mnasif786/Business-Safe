using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;
using Action = BusinessSafe.Domain.Entities.Action;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public enum ActionOrderByColumn
    {
          None,
		
    }

    public interface IActionRepository : IRepository< BusinessSafe.Domain.Entities.Action, long>
    {
        int Count(long actionPlanId);
        IEnumerable<BusinessSafe.Domain.Entities.Action> Search(long actionPlanId, int page, int pageSize, ActionOrderByColumn orderBy, bool ascending);
        Action GetByIdAndCompanyId(long actionId, long companyId);
    }

}
