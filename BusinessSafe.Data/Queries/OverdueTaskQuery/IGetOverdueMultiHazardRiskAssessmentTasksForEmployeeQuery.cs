using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Entities;
using NHibernate;

namespace BusinessSafe.Data.Queries.OverdueTaskQuery
{
    public interface IGetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery<T> where T : MultiHazardRiskAssessment
    {
        List<MultiHazardRiskAssessmentFurtherControlMeasureTask> Execute(Guid employeeId, ISession session);
    }
}