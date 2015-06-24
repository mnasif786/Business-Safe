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
    public class GetOverDueReviewsQuery : IGetOverDueReviewsQuery
    {

        public IList<Task> Execute(ISession session)
        {
            return session
                .CreateCriteria<Task>()
                .SetFetchMode("RiskAssessment", FetchMode.Eager)
                .Add(Restrictions.Eq("TaskStatus", TaskStatus.Outstanding))
                .Add(Restrictions.Eq("Deleted", false))
                .Add(Restrictions.Eq("class", typeof(RiskAssessmentReviewTask)))
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