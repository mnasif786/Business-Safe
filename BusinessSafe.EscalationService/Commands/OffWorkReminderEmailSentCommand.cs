using System;

using BusinessSafe.Domain.Entities;
using BusinessSafe.EscalationService.Activation;
using BusinessSafe.EscalationService.Entities;
using NHibernate;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.EscalationService.Commands
{
    public class OffWorkReminderEmailSentCommand : IOffWorkReminderEmailSentCommand
    {
        private readonly IBusinessSafeSessionManager _businessSafeSessionManager;

        public OffWorkReminderEmailSentCommand(IBusinessSafeSessionManager businessSafeSessionManager)
        {
            _businessSafeSessionManager = businessSafeSessionManager;
        }

        public void Execute(ISession session, long accidentRecordId, DateTime sentDate)
        {
            bool closeSession = false;
            if (session == null)
            {
                session = _businessSafeSessionManager.Session;
                closeSession = true;
            }

            var systemUser = session.Load<UserForAuditing>(SystemUser.Id);

            var offWorkReminderEscalation = new EscalationOffWorkReminder()
            {
                AccidentRecordId = accidentRecordId,
                CreatedBy = systemUser,
                CreatedOn = DateTime.Now,
                OffWorkReminderEmailSentDate = DateTime.Now                
            };

            session.Save(offWorkReminderEscalation);

            //Log4NetHelper.Log.Debug("Saved EscalationOffWorkReminder sent indicator");

            if (closeSession)
            {
                session.Close();
            }               
        }

    }
}
