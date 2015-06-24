using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Data.Queries.OverdueTaskQuery
{
    public interface IGetOverduePersonalRiskAssessmentTasksForEmployeeQuery
    {
        List<MultiHazardRiskAssessmentFurtherControlMeasureTask> Execute(Guid employeeId);
    }
}