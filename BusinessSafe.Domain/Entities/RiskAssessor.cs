using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class RiskAssessor : Entity<long>
    {
        public virtual Employee Employee { get; set; }
        public virtual bool HasAccessToAllSites { get; set; }
        public virtual SiteStructureElement Site { get; set; }
        public virtual bool DoNotSendTaskOverdueNotifications { get; set; }
        public virtual bool DoNotSendTaskCompletedNotifications { get; set; }
        public virtual bool DoNotSendReviewDueNotification { get; set; }
        
        public virtual string FormattedName
        {
            get { return Employee !=null ? Employee.FullName: string.Empty; }
        }

        public static RiskAssessor Create(
            Employee employee,
            SiteStructureElement site,
            bool doNotSendTaskOverdueNotifications,
            bool doNotSendTaskCompletedNotifications,
            bool doNotSendReviewDueNotification,
            bool hasAccessToAllSites,
            UserForAuditing creatingUser)
        {
            var riskAssessor = new RiskAssessor();
            riskAssessor.Employee = employee;
            riskAssessor.Site = site;
            riskAssessor.HasAccessToAllSites = hasAccessToAllSites;
            riskAssessor.DoNotSendTaskOverdueNotifications = doNotSendTaskOverdueNotifications;
            riskAssessor.DoNotSendTaskCompletedNotifications = doNotSendTaskCompletedNotifications;
            riskAssessor.CreatedBy = creatingUser;
            riskAssessor.CreatedOn = DateTime.Now;
            riskAssessor.LastModifiedBy = creatingUser;
            riskAssessor.LastModifiedOn = DateTime.Now;

            return riskAssessor;
        }


         public virtual void Update(
            SiteStructureElement site,
            bool hasAccessToAllSites,
            bool doNotSendReviewDueNotification,
            bool doNotSendTaskOverdueNotifications,
            bool doNotSendTaskCompletedNotifications,
            UserForAuditing user
            )
        {
            Site = site;
            HasAccessToAllSites = hasAccessToAllSites;
            DoNotSendReviewDueNotification = doNotSendReviewDueNotification;
            DoNotSendTaskOverdueNotifications = doNotSendTaskOverdueNotifications;
            DoNotSendTaskCompletedNotifications = doNotSendTaskCompletedNotifications;
            SetLastModifiedDetails(user);
        }
    }
}
