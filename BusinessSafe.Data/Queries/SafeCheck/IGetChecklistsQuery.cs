using System;
using System.Collections.Generic;

namespace BusinessSafe.Data.Queries.SafeCheck
{
    public interface IGetChecklistsQuery
    {
        IGetChecklistsQuery WithClientId(int clientId);
        IGetChecklistsQuery WithConsultantName(string consultantName);
        IGetChecklistsQuery WithVisitDate(DateTime visitDate);
        IGetChecklistsQuery WithStatus(string status);
        IGetChecklistsQuery WithDeletedOnly();
        IGetChecklistsQuery ExcludeSubmitted();
        IGetChecklistsQuery WithQaAdvisor(Guid qaAdvisorId);
        IGetChecklistsQuery WithStatusDateBetween(DateTime? fromDate, DateTime? toDate);

        List<ChecklistIndex> Execute();
        int Count(int? clientId, string consultantName, DateTime? visitDate, string status, bool includeDeleted, bool excludeSubmitted, Guid? qaAdvisorId);

        
    }
}