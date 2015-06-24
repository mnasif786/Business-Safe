using System.Collections.Generic;
using BusinessSafe.Application.Contracts.ActionPlan;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.ActionPlan
{
    public class ActionPlanService : IActionPlanService
    {
        private readonly IActionPlanRepository _actionPlanRepository;

        public ActionPlanService(IActionPlanRepository actionPlanRepository)
        {
            _actionPlanRepository = actionPlanRepository;
        }

        public int Count(SearchActionPlanRequest request)
        {
            return _actionPlanRepository.Count(request.AllowedSiteIds, request.CompanyId, request.SiteGroupId, request.SiteId, request.SubmittedFrom, request.SubmittedTo, request.ShowArchived);
        }


        public ActionPlanDto GetByIdAndCompanyId(long actionPlanId, long companyId)
        {
            var actionPlan = _actionPlanRepository.GetByIdAndCompanyId(actionPlanId, companyId);

            ActionPlanDto dto = new ActionPlanDtoMapper()
                .WithSites()
                .WithActions()
                .WithActionTasks()
                .Map(actionPlan);
            return dto;
        }

        public IEnumerable<ActionPlanDto> Search(SearchActionPlanRequest request)
        {
            var actionPlans = _actionPlanRepository.Search(request.AllowedSiteIds, request.CompanyId, 
                                                           request.SiteGroupId, request.SiteId, request.SubmittedFrom,
                                                           request.SubmittedTo, request.ShowArchived,
                                                           request.Page, request.PageSize, request.OrderBy, 
                                                           request.Ascending);
            return new ActionPlanDtoMapper()
                .WithSites()
                .WithActions()
                .WithStatus()
                .Map(actionPlans);
        }
    }
}