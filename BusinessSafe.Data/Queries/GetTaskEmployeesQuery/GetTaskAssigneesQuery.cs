using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Data.Queries.GetTaskEmployeesQuery
{
    /// <summary>
    /// Returns employees with email addresses that have been assigned a task
    /// </summary>
    public class GetTaskAssigneesQuery : IGetTaskEmployeesQuery
    {
        private readonly IQueryable<Task> _queryableTask;

        public GetTaskAssigneesQuery(IQueryableWrapper<Task> queryableTask)
        {
            _queryableTask = queryableTask.Queryable();
        }

        private IQueryable<Task> CreateQuery()
        {
            var query = _queryableTask
                .Where(x => !x.Deleted)
                .Where(TaskAssigneeHasEmailAddress());

            return query;
        }

        private static Expression<Func<Task, bool>> TaskAssigneeHasEmailAddress()
        {
            return x => x.TaskAssignedTo != null
                        && x.TaskAssignedTo.ContactDetails.Any(ecd => ecd.Email != "");
        }

        public List<Employee> Execute()
        {
            var query = CreateQuery();
            return query.Select(x => x.TaskAssignedTo)
                .ToList();
        }
    }
}