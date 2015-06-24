using System;
using NHibernate;

namespace BusinessSafe.EscalationService.Commands
{
    public interface INextReoccurringTaskNotificationEmailSentCommand
    {
        void Execute(ISession session, long taskId, DateTime sentDate);
    }
}