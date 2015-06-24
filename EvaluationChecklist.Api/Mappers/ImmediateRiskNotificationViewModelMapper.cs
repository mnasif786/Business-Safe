using BusinessSafe.Domain.Entities.SafeCheck;
using EvaluationChecklist.Models;
using System.Collections.Generic;
using System.Linq;

namespace EvaluationChecklist.Mappers
{
    public static class ImmediateRiskNotificationViewModelMapper
    {
        public static ImmediateRiskNotificationViewModel Map(ImmediateRiskNotification immediateRiskNotification)
        {
            var immediateRiskNotificationViewModel = new ImmediateRiskNotificationViewModel
                                                         {
                                                             Id = immediateRiskNotification.Id,
                                                             Reference = immediateRiskNotification.Reference,
                                                             Title = immediateRiskNotification.Title,
                                                             SignificantHazard =
                                                                 immediateRiskNotification.SignificantHazardIdentified,
                                                             RecommendedImmediate =
                                                                 immediateRiskNotification.RecommendedImmediateAction,
                                                         };

            return immediateRiskNotificationViewModel;
        }

        public static List<ImmediateRiskNotificationViewModel> Map(IList<ImmediateRiskNotification> immediateRiskNotifications)
        {
            return immediateRiskNotifications.Select(Map).ToList();
        }
    }
}