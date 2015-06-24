using System.Collections.Generic;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.Application.Contracts.RiskAssessments
{
    public interface IFurtherControlMeasureTaskService
    {
        void CompleteFurtherControlMeasureTask(CompleteTaskRequest request);
        FurtherControlMeasureTaskDto GetByIdAndCompanyId(long id, long companyId);
        void Update(UpdateFurtherControlMeasureTaskRequest request);
        MultiHazardRiskAssessmentFurtherControlMeasureTaskDto AddFurtherControlMeasureTask(SaveFurtherControlMeasureTaskRequest request);
        FurtherControlMeasureTaskDto GetByIdIncludeDeleted(long id);
        void SendTaskCompletedEmail(CompleteTaskRequest request);
        IEnumerable<TaskDocumentDto> AddDocumentsToTask(AddDocumentsToTaskRequest request);

    }
}