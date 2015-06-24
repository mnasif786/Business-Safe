using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities.SafeCheck
{
    public class ImmediateRiskNotification : BaseEntity<Guid>
    {
        public virtual string Reference { get; set; }
        public virtual string Title { get; set; }
        public virtual string SignificantHazardIdentified { get; set; }
        public virtual string RecommendedImmediateAction { get; set; }
        public virtual Checklist Checklist { get; set; }

        public static ImmediateRiskNotification Create(
            Guid id,
            string reference,
            string title,
            string significantHazardIdentified,
            string recommendedImmediateAction,
            Checklist checklist,
            UserForAuditing user)
        {
            var immediateRiskNotification = new ImmediateRiskNotification();
            immediateRiskNotification.Id = id;
            immediateRiskNotification.Reference = reference;
            immediateRiskNotification.Title = title;
            immediateRiskNotification.SignificantHazardIdentified = significantHazardIdentified;
            immediateRiskNotification.RecommendedImmediateAction = recommendedImmediateAction;
            immediateRiskNotification.Checklist = checklist;
            immediateRiskNotification.CreatedOn = DateTime.Now;
            immediateRiskNotification.CreatedBy = user;
            immediateRiskNotification.LastModifiedBy = user;
            immediateRiskNotification.LastModifiedOn = DateTime.Now;
            return immediateRiskNotification;
        }

        public virtual ImmediateRiskNotification Copy(UserForAuditing user, Checklist checklist)
        {
            var irn  = new ImmediateRiskNotification();

            irn.Id = Guid.NewGuid();
            irn.Reference = Reference;
            irn.Title = Title;
            irn.SignificantHazardIdentified = SignificantHazardIdentified;
            irn.RecommendedImmediateAction = RecommendedImmediateAction;

            irn.Checklist = checklist; 

            irn.CreatedOn = DateTime.Now;
            irn.CreatedBy = user;
            irn.LastModifiedBy = user;
            irn.LastModifiedOn = DateTime.Now;

            return irn;
        }
    }
}
