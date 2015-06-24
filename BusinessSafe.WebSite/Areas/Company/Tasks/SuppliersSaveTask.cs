using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;
using BusinessSafe.Domain.RepositoryContracts;
using System.Linq;
using BusinessSafe.WebSite.Areas.Company.ViewModels;

namespace BusinessSafe.WebSite.Areas.Company.Tasks
{
    public class SuppliersSaveTask :CompanyDefaultsSaveTask
    {
        private readonly ISuppliersService _suppliersService;
        private readonly ISupplierRepository _supplierRepository;
        private GuardDefaultExistsRequest _guardDefaultExistsRequest;

        public SuppliersSaveTask(ISuppliersService suppliersService, ISupplierRepository supplierRepository, IDoesCompanyDefaultAlreadyExistGuard companyDefaultAlreadyExistGuard) : base(companyDefaultAlreadyExistGuard)
        {
            _suppliersService = suppliersService;
            _supplierRepository = supplierRepository;
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

            if(request.Id == 0)
            {
                var result = _suppliersService.CreateSupplier(new SaveSupplierRequest()
                                                                  {
                                                                      Name = request.Name,
                                                                      CompanyId = request.CompanyId,
                                                                      UserId = request.UserId
                                                                  });

                return CompanyDefaultSaveResponse.CreateSavedSuccessfullyResponse(result);
            }

            _suppliersService.UpdateSupplier(new SaveSupplierRequest()
                                                {
                                                    Id = request.Id,
                                                    Name = request.Name,
                                                    CompanyId = request.CompanyId,
                                                    UserId = request.UserId
                                                });

            return CompanyDefaultSaveResponse.CreateSavedSuccessfullyResponse(request.Id);
        }

        private IEnumerable<MatchingName> RunQuery()
        {
            var hazards = _supplierRepository.GetAllByNameSearch(_guardDefaultExistsRequest.Name, _guardDefaultExistsRequest.ExcludeId, _guardDefaultExistsRequest.CompanyId, 100);
            return hazards.Select(x => new MatchingName() { Name = x.Name }).ToList();
        }
    }
}