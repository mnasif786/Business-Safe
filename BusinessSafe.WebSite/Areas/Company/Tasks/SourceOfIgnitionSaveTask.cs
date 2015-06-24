using System;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;
using BusinessSafe.WebSite.Areas.Company.ViewModels;

namespace BusinessSafe.WebSite.Areas.Company.Tasks
{
    public class SourceOfIgnitionSaveTask : CompanyDefaultsSaveTask
    {
        public SourceOfIgnitionSaveTask(ICompanyDefaultService companyDefaultService, IDoesCompanyDefaultAlreadyExistGuard companyDefaultAlreadyExistGuard)
            : base(companyDefaultService, companyDefaultAlreadyExistGuard)
        {
        }

        public override CompanyDefaultSaveResponse Execute(SaveCompanyDefaultViewModel viewModel, Guid userId)
        {
            var request = new SaveCompanyDefaultRequest(viewModel.CompanyDefaultId, viewModel.CompanyDefaultValue,
                                                        viewModel.CompanyId, viewModel.RiskAssessmentId, viewModel.RunMatchCheck,
                                                        userId);

            var result =
                CompanyDefaultService.SaveSourceOfIgnition(new SaveCompanyDefaultRequest(request.Id,
                                                                                     request.Name,
                                                                                     request.CompanyId,
                                                                                     request.RiskAssessmentId,
                                                                                     false,
                                                                                     userId));

            return CompanyDefaultSaveResponse.CreateSavedSuccessfullyResponse(result);

        }
    }
}