using BusinessSafe.Application.DataTransferObjects;
using SaveFurtherControlMeasureTaskRequest = BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments.SaveFurtherControlMeasureTaskRequest;

namespace BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments
{
    public interface IHazardousSubstanceRiskAssessmentFurtherControlMeasureTaskService
    {
        HazardousSubstanceRiskAssessmentFurtherControlMeasureTaskDto AddFurtherControlMeasureTask(SaveFurtherControlMeasureTaskRequest request);
        
    }
}
