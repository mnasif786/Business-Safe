using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Contracts.TaskList
{
    public interface ITaskListService
    {
        IEnumerable<TaskListItem> Search(long companyId, IEnumerable<Guid> employeeIds, bool showCompleted, IList<long> allowedSiteIds);
        IEnumerable<TaskListItem> GetFurtherControlMeasureTasksByRiskAssessmentId(long riskAssessmentId, long companyId);
    }
}