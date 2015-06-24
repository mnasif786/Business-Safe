using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Entities;
using BusinessSafe.EscalationService.Entities;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.EscalationService.Queries
{
    public class GetNextReoccurringTasksLiveQuery: IGetNextReoccurringTasksLiveQuery
    {
       
        public IList<Task> Execute(ISession session)
        {
            return session
                .CreateCriteria<Task>()
                .Add(Restrictions.Eq("TaskStatus", TaskStatus.Outstanding))
                .Add(Restrictions.Not(Restrictions.Eq("TaskReoccurringType", TaskReoccurringType.None)))
                .Add(Restrictions.Eq("Deleted", false))
                .Add(Restrictions.Gt("TaskCompletionDueDate", DateTime.Now))
                .Add(Subqueries.PropertyNotIn("Id",
                                              DetachedCriteria.For<EscalationNextReoccurringLiveTask>()
                                                  .SetProjection(Projections.Property("TaskId"))
                                                  .Add(Restrictions.IsNotNull("NextReoccurringLiveTaskEmailSentDate"))
                         ))
                .SetMaxResults(10000)
                .SetResultTransformer(new DistinctRootEntityResultTransformer())
                .List<Task>();
        }
    }
}