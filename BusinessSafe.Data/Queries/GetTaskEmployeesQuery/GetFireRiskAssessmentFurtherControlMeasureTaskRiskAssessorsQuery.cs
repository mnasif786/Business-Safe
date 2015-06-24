using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Data.Queries.GetTaskEmployeesQuery
{

    /// <summary>
    /// Returns risk assessor employees for fire risk assessments
    /// </summary>
    public class GetFireRiskAssessmentFurtherControlMeasureTaskRiskAssessorsQuery: IGetTaskEmployeesQuery
    {
        private readonly IQueryable<FireRiskAssessmentFurtherControlMeasureTask> _queryableTask;

        public GetFireRiskAssessmentFurtherControlMeasureTaskRiskAssessorsQuery(IQueryableWrapper<FireRiskAssessmentFurtherControlMeasureTask> queryableTask)
        {
            _queryableTask = queryableTask.Queryable();
        }

        public List<Employee> Execute()
        {
            return _queryableTask
                .Where(RiskAssessmentNotDeleted())
                .Where(RiskAssessorHasEmailAddress())
                .Where(task => task.Deleted == false)
                .Select(SelectRiskAssessorEmployee())
                .Distinct()
                .ToList();
        }

        private static Expression<Func<FireRiskAssessmentFurtherControlMeasureTask, bool>> RiskAssessmentNotDeleted()
        {
            return task => task.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.Deleted==false;
        }

        public static Expression<Func<FireRiskAssessmentFurtherControlMeasureTask, bool>> RiskAssessorHasEmailAddress()
        {
            return task => task.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessor != null
                && task.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessor.Employee.ContactDetails.Any(ecd => ecd.Email != "" && ecd.Email != null);
        }

        private static Expression<Func<FireRiskAssessmentFurtherControlMeasureTask, Employee>> SelectRiskAssessorEmployee()
        {
            return task => task.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessor.Employee;
        }
    }
}
