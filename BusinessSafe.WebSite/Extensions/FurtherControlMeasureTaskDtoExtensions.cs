using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.WebSite.Extensions
{
    public static class FurtherControlMeasureTaskDtoExtensions
    {
        public static bool IsTaskCompletedNotificationRequired(
            this FurtherControlMeasureTaskDto furtherControlMeasureTaskDto)
        {
            return furtherControlMeasureTaskDto.SendTaskCompletedNotification &&
                   furtherControlMeasureTaskDto.RiskAssessment.RiskAssessor != null;
        }
    }
}
