using System;
using System.Collections.Generic;
using System.ComponentModel;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public enum ActionPlanOrderByColumn
    {
          None,
		  Title,
		  Site,
		  DateOfVisit,
		  VisitBy,
		  SubmittedOn
    }

    public interface IActionPlanRepository : IRepository<ActionPlan, long>
    {
        //ActionPlan GetById(long actionPlanId);

        int Count(IList<long> allowedSiteIds,long companyId, long? siteGroupId, long? siteId, DateTime? submittedFrom, DateTime? submittedTo, bool showArchived);
        IEnumerable<ActionPlan> Search(IList<long> allowedSiteIds, long companyId, long? siteGroupId, long? siteId, DateTime? submittedFrom, DateTime? submittedTo, bool showArchived, int page, int pageSize, ActionPlanOrderByColumn orderBy, bool @ascending);
        ActionPlan GetByIdAndCompanyId(long actionPlanId, long companyId);
    }
}
