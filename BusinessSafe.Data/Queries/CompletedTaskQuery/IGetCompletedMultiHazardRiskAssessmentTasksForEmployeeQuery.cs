using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NHibernate;

namespace BusinessSafe.Data.Queries.CompletedTaskQuery
{
    public interface IGetCompletedMultiHazardRiskAssessmentTasksForEmployeeQuery<T> where T : MultiHazardRiskAssessment
    {
        List<MultiHazardRiskAssessmentFurtherControlMeasureTask> Execute(Guid employeeId, ISession session);
    }
}
