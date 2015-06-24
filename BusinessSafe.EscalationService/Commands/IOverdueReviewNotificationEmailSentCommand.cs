using System;
using NHibernate;

namespace BusinessSafe.EscalationService.Commands
{
    public interface IOverdueReviewNotificationEmailSentCommand
    {
        void Execute(ISession session, long taskId, DateTime sentDate);
    }
}