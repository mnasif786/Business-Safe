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
    public class PeopleAtRiskSaveTask : CompanyDefaultsSaveTask
    {
        private readonly IPeopleAtRiskRepository _peopleAtRiskRepository;
        private GuardDefaultExistsRequest _guardDefaultExistsRequest;

        public PeopleAtRiskSaveTask(ICompanyDefaultService companyDefaultService, IPeopleAtRiskRepository peopleAtRiskRepository, IDoesCompanyDefaultAlreadyExistGuard companyDefaultAlreadyExistGuard)
            : base(companyDefaultService, companyDefaultAlreadyExistGuard)
        {
            _peopleAtRiskRepository = peopleAtRiskRepository;
        }

        public override CompanyDefaultSaveResponse Execute(SaveCompanyDefaultViewModel viewModel, Guid userId)
        {
            var request = new SaveCompanyDefaultRequest(viewModel.CompanyDefaultId, viewModel.CompanyDefaultValue,
                                            viewModel.CompanyId, viewModel.RiskAssessmentId, viewModel.RunMatchCheck,
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

            var result = CompanyDefaultService.SavePeopleAtRisk(request);
            return CompanyDefaultSaveResponse.CreateSavedSuccessfullyResponse(result);
        }

        private IEnumerable<MatchingName> RunQuery()
        {
            var hazards = _peopleAtRiskRepository.GetAllByNameSearch(_guardDefaultExistsRequest.Name, _guardDefaultExistsRequest.ExcludeId, _guardDefaultExistsRequest.CompanyId);
            return hazards.Select(x => new MatchingName() {Name = x.Name}).ToList();
        }
    }
}