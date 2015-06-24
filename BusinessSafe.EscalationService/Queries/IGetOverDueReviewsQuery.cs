using System.Collections.Generic;
using BusinessSafe.Domain.Entities;
using NHibernate;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.EscalationService.Queries
{
    public interface IGetOverDueReviewsQuery
    {
        IList<Task> Execute(ISession sessionManager);
    }
}