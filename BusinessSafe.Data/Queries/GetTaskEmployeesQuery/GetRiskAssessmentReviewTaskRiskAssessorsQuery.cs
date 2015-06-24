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
    /// Returns risk assessor employees for Review risk assessments
    /// </summary>
    public class GetRiskAssessmentReviewTaskRiskAssessorsQuery: IGetTaskEmployeesQuery
    {
        private readonly IQueryable<RiskAssessmentReviewTask> _queryableTask;

        public GetRiskAssessmentReviewTaskRiskAssessorsQuery(IQueryableWrapper<RiskAssessmentReviewTask> queryableTask)
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

        private static Expression<Func<RiskAssessmentReviewTask, bool>> RiskAssessmentNotDeleted()
        {
            return task => task.RiskAssessmentReview.RiskAssessment.Deleted==false;
        }

        public static Expression<Func<RiskAssessmentReviewTask, bool>> RiskAssessorHasEmailAddress()
        {
            return task => task.RiskAssessmentReview.RiskAssessment.RiskAssessor != null
                && task.RiskAssessmentReview.RiskAssessment.RiskAssessor.Employee.ContactDetails.Any(ecd => ecd.Email != "" && ecd.Email != null);
        }

        private static Expression<Func<RiskAssessmentReviewTask, Employee>> SelectRiskAssessorEmployee()
        {
            return task => task.RiskAssessmentReview.RiskAssessment.RiskAssessor.Employee;
        }
    }
}
