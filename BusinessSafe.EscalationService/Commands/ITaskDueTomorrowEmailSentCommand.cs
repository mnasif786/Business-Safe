using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace BusinessSafe.EscalationService.Commands
{
    public interface ITaskDueTomorrowEmailSentCommand
    {
        void Execute(ISession session, long taskId, DateTime sentDate);
    }
}
