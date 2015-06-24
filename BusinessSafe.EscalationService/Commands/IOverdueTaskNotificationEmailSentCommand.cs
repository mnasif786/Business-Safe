using System;
using NHibernate;

namespace BusinessSafe.EscalationService.Commands
{
    public interface IOverdueTaskNotificationEmailSentCommand
    {
        void Execute(ISession session, long taskId, DateTime sentDate);
    }
}