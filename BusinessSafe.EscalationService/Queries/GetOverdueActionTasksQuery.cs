using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using BusinessSafe.EscalationService.Entities;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace BusinessSafe.EscalationService.Queries
{
    public class GetOverdueActionTasksQuery : IGetOverdueActionTasksQuery
    {
        public IList<Domain.Entities.Task> Execute(NHibernate.ISession session)
        {          
            return session
               .CreateCriteria<Task>()
               .SetFetchMode("ActionTask", FetchMode.Eager)
               .Add(Restrictions.Eq("TaskStatus", TaskStatus.Outstanding))
               .Add(Restrictions.Eq("Deleted", false))
               .Add(Restrictions.Eq("class", typeof(ActionTask)))
               .Add(Restrictions.Lt("TaskCompletionDueDate", DateTime.Now))
               .Add(Subqueries.PropertyNotIn("Id",
                                             DetachedCriteria.For<EscalationOverdueReview>()
                                                 .SetProjection(Projections.Property("TaskId"))
                                                 .Add(Restrictions.IsNotNull("OverdueEmailSentDate"))
                        ))
               .SetMaxResults(10000)
               .SetResultTransformer(new DistinctRootEntityResultTransformer())
               .List<Task>();
        }
    }
}
