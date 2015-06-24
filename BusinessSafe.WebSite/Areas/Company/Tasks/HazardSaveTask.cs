using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.WebSite.Areas.Company.ViewModels;

namespace BusinessSafe.WebSite.Areas.Company.Tasks
{
    public class HazardSaveTask : CompanyDefaultsSaveTask
    {
        private readonly IHazardRepository _hazardRepository;
        private GuardDefaultExistsRequest _guardDefaultExistsRequest;

        public HazardSaveTask(ICompanyDefaultService companyDefaultService, IHazardRepository hazardRepository, IDoesCompanyDefaultAlreadyExistGuard companyDefaultAlreadyExistGuard)
            : base(companyDefaultService, companyDefaultAlreadyExistGuard)
        {
            _hazardRepository = hazardRepository;
        }

        public override CompanyDefaultSaveResponse Execute(SaveCompanyDefaultViewModel viewModel, Guid userId)
        {
            // Test there no Risk Assessment Types Applicable
            if (viewModel.RiskAssessmentTypeApplicable == null || viewModel.RiskAssessmentTypeApplicable.Any() == false)
            {
                return CompanyDefaultSaveResponse.CreateValidationFailedResponse("Applicable risk types required");
            }

            var request = new SaveCompanyHazardDefaultRequest(viewModel.CompanyDefaultId,
                                                        viewModel.CompanyDefaultValue,
                                                        viewModel.CompanyId,
                                                        viewModel.RiskAssessmentId,
                                                        viewModel.RunMatchCheck,
                                                        viewModel.RiskAssessmentTypeApplicable,
                                                        userId);
            if (request.RunMatchCheck)
            {
                _guardDefaultExistsRequest = new GuardDefaultExistsRequest(request.Name, request.Id, request.CompanyId);
                var existResult = CompanyDefaultAlreadyExistGuard.Execute(RunQuery);

                if (existResult.Exists)
                {
                    return CompanyDefaultSaveResponse.CompanyDefaultMatches(existResult.MatchingResults);
                }
            }

            var result = CompanyDefaultService.SaveHazard(request);
            return CompanyDefaultSaveResponse.CreateSavedSuccessfullyResponse(result);
        }

        private IEnumerable<MatchingName> RunQuery()
        {
            var hazards = _hazardRepository.GetAllByNameSearch(_guardDefaultExistsRequest.Name, _guardDefaultExistsRequest.ExcludeId, _guardDefaultExistsRequest.CompanyId);
            return hazards.Select(x => new MatchingName() { Name = x.Name }).ToList();
        }
    }
}