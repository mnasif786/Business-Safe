using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NHibernate;

namespace BusinessSafe.EscalationService.Queries
{
    interface IGetOverdueActionTasksQuery
    {
        IList<Task> Execute(ISession session);
    }
}
