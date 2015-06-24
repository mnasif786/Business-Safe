using System;
using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request.Responsibilities;
using BusinessSafe.Application.Response;

namespace BusinessSafe.Application.Contracts.Responsibilities
{
    public interface IResponsibilitiesService
    {
        IEnumerable<ResponsibilityCategoryDto> GetResponsibilityCategories();
        IEnumerable<ResponsibilityReasonDto> GetResponsibilityReasons();

        IEnumerable<ResponsibilityDto> Search(SearchResponsibilitiesRequest request);
        long SaveResponsibility(SaveResponsibilityRequest request);

        void CreateResponsibilitiesFromWizard(CreateResponsibilityFromWizardRequest request);

        ResponsibilityDto GetResponsibility(long id, long companyId);
        SaveResponsibilityTaskResponse SaveResponsibilityTask(SaveResponsibilityTaskRequest request);

        bool HasUndeletedTasks(long responsibilityId, long companyId);

        void Delete(long responsibilityId, long companyId, Guid actioningUserId);

        void Undelete(long responsibilityId, long companyId, Guid actioningUserId);
        List<ResponsibilityDto> GetStatutoryResponsibiltiesWithUncreatedStatutoryTasks(long companyId);
        void CreateResponsibilityTaskFromWizard(CreateResponsibilityTasksFromWizardRequest request);

        void CreateManyResponsibilityTaskFromWizard(CreateManyResponsibilityTaskFromWizardRequest request);
        List<ResponsibilityDto> GetStatutoryResponsibilities(long companydId);
        long CopyResponsibility(CopyResponsibilityRequest request);

        int Count(SearchResponsibilitiesRequest request);
    }
}