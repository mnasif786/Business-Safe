using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NHibernate;

namespace BusinessSafe.Data.Queries.OverdueTaskQuery
{
    public interface IGetOverdueActionTasksForEmployeeQuery
    {
        List<ActionTask> Execute(Guid employeeId, ISession session);
        
    }
}
