using System.Collections.Generic;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Data.Queries.GetTaskEmployeesQuery
{
    //we require a different query for each type of task because of the crazy inheritance. ALP
    //Therefore there is more than one instance of IGetTasksEmployeesQuery

    /// <summary>
    /// returns a list of employees that are risk assessors or assigned a task 
    /// </summary>
    public interface IGetTaskEmployeesQuery
    {
        List<Employee> Execute();
    }
}