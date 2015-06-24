using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Data.Queries.GetTaskEmployeesQuery
{
    public class GetHazardousSubstancesRiskAssessmentFurtherControlMeasuresRiskAssessorsQuery : IGetTaskEmployeesQuery
    {       
        private readonly IQueryable<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask> _queryableTask;

        public GetHazardousSubstancesRiskAssessmentFurtherControlMeasuresRiskAssessorsQuery(IQueryableWrapper<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask> queryableTask)
        {
            _queryableTask = queryableTask.Queryable();
        }
        
        private static Expression<Func<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask, bool>> RiskAssessmentIsNotDeleted()
        {
            return x => x.HazardousSubstanceRiskAssessment.Deleted == false;
        }

        private static Expression<Func<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask, bool>> RiskAssessorHasEmailAddress()
        {
            return x => x.HazardousSubstanceRiskAssessment.RiskAssessor.Employee != null
                        && x.HazardousSubstanceRiskAssessment.RiskAssessor.Employee.ContactDetails.Any(ecd => ecd.Email != "");
        }

        private static Expression<Func<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask, Employee>> SelectRiskAssessorEmployee()
        {
            return task => task.HazardousSubstanceRiskAssessment.RiskAssessor.Employee;
        }

        public List<Employee> Execute()
        {

            return  _queryableTask
               .Where(RiskAssessmentIsNotDeleted())
               .Where(RiskAssessorHasEmailAddress())
               .Where(task => task.Deleted == false)
               .Select(SelectRiskAssessorEmployee())
               .Distinct()
               .ToList();
        }
    }
}
