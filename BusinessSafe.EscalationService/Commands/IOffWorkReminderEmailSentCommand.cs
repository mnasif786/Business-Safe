using System;
using NHibernate;

namespace BusinessSafe.EscalationService.Commands
{
    public interface IOffWorkReminderEmailSentCommand
    {
        void Execute(ISession session, long accidentRecordId, DateTime sentDate);
    }
}