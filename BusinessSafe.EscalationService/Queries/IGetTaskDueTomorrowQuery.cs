using System.Collections.Generic;
using BusinessSafe.Domain.Entities;
using NHibernate;

namespace BusinessSafe.EscalationService.Queries
{
    public interface IGetTaskDueTomorrowQuery
    {
        IList<Task> Execute(ISession session);
    }
}
