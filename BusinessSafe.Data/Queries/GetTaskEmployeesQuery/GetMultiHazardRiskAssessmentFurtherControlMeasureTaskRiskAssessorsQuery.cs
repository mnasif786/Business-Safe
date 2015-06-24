using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Data.Queries.GetTaskEmployeesQuery
{
    /// <summary>
    /// Returns risk assessors employee for General Risk Assessment and Personal Risk Assessments
    /// </summary>
    public class GetMultiHazardRiskAssessmentFurtherControlMeasureTaskRiskAssessorsQuery : IGetTaskEmployeesQuery
    {
        private readonly IQueryable<MultiHazardRiskAssessmentFurtherControlMeasureTask> _queryableTask;

        public GetMultiHazardRiskAssessmentFurtherControlMeasureTaskRiskAssessorsQuery(IQueryableWrapper<MultiHazardRiskAssessmentFurtherControlMeasureTask> queryableTask)
        {
            _queryableTask = queryableTask.Queryable();
        }

        private IQueryable<MultiHazardRiskAssessmentFurtherControlMeasureTask> CreateQuery()
        {
            var query = _queryableTask
                .Where(x => !x.Deleted)
                .Where(RiskAssessmentIsNotDeleted())
                .Where(RiskAssessorHasEmailAddress());

            return query;
        }

        private static Expression<Func<MultiHazardRiskAssessmentFurtherControlMeasureTask, bool>> RiskAssessmentIsNotDeleted()
        {
            return x => x.MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment.Deleted == false;
        }

        private static Expression<Func<MultiHazardRiskAssessmentFurtherControlMeasureTask, bool>> RiskAssessorHasEmailAddress()
        {
            return x => x.MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment.RiskAssessor.Employee != null
                        && x.MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment.RiskAssessor.Employee.ContactDetails.Any(ecd => ecd.Email != "");
        }

        public List<Employee> Execute()
        {
            var query = CreateQuery();
            return query.Select(x => x.MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment.RiskAssessor.Employee)
                .Distinct()
                .ToList();
        }
    }
}