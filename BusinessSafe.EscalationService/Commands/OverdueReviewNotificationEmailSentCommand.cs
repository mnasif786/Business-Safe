using System;
using BusinessSafe.Domain.Entities;
using BusinessSafe.EscalationService.Activation;
using BusinessSafe.EscalationService.Entities;
using NHibernate;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.EscalationService.Commands
{
    public class OverdueReviewNotificationEmailSentCommand : IOverdueReviewNotificationEmailSentCommand
    {
        private readonly IBusinessSafeSessionManager _businessSafeSessionManager;

        public OverdueReviewNotificationEmailSentCommand(IBusinessSafeSessionManager businessSafeSessionManager)
        {
            _businessSafeSessionManager = businessSafeSessionManager;
        }

        public void Execute(ISession session, long taskId, DateTime sentDate)
        {
            bool closeSession = false;
            if (session == null)
            {
                session = _businessSafeSessionManager.Session;
                closeSession = true;
            }

            var systemUser = session.Load<UserForAuditing>(SystemUser.Id);

            var review = new EscalationOverdueReview()
            {
                TaskId = taskId,
                OverdueEmailSentDate = sentDate,
                CreatedBy = systemUser,
                CreatedOn = DateTime.Now
            };
            session.Save(review);

            //Log4NetHelper.Log.Debug("Saved EscalationOverdueReview sent indicator");

            if (closeSession)
            {
                session.Close();
            }
            
        }
    }
}