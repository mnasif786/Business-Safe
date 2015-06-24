using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NHibernate;

namespace BusinessSafe.Data.Queries.CompletedTaskQuery
{
    public interface IGetCompletedHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery
    {
        List<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask> Execute(Guid employeeId, ISession session);
      
    }
}
