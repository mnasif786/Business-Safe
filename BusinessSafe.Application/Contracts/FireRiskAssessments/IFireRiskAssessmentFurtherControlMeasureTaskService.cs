using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request.FireRiskAssessments;

namespace BusinessSafe.Application.Contracts.FireRiskAssessments
{
    public interface IFireRiskAssessmentFurtherControlMeasureTaskService
    {
        FireRiskAssessmentFurtherControlMeasureTaskDto AddFurtherControlMeasureTask(SaveFurtherControlMeasureTaskRequest request);
        GetFurtherControlMeasureTaskCountsForAnswerResponse GetFurtherControlMeasureTaskCountsForAnswer(GetFurtherControlMeasureTaskCountsForAnswerRequest request);
    }
}