using System.Collections.Generic;
using BusinessSafe.Domain.Entities;
using NHibernate;

namespace BusinessSafe.EscalationService.Queries
{
    public interface IGetAccidentRecordOffWorkReminderQuery
    {
        IList<AccidentRecord> Execute(ISession sessionManager);
    }
}