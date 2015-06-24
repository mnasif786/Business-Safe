using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Data.Queries
{
    public interface IGetEmployeeNamesQuery
    {
        List<EmployeeName> Execute(long companyId);
    }

    public class GetEmployeeNamesQuery : IGetEmployeeNamesQuery
    {
        private readonly IQueryable<Employee> _queryableEmployees;

        public GetEmployeeNamesQuery(IQueryableWrapper<Employee> queryableEmployees)
        {
            _queryableEmployees = queryableEmployees.Queryable();
        }

        private IQueryable<Employee> CreateQuery(long clientId)
        {
            var query = _queryableEmployees.Where(x => x.CompanyId == clientId && !x.Deleted);
            return query;
        }

        public List<EmployeeName> Execute(long clientId)
        {
            var query = CreateQuery(clientId);
            return query.Select(x => new EmployeeName()
                                         {
                                             Id = x.Id,
                                             Forename = x.Forename,
                                             Surname = x.Surname,
                                             JobTitle = x.JobTitle
                                         }).ToList();
        }
    }
}
