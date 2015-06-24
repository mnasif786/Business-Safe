using System;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;
using BusinessSafe.WebSite.Areas.Company.ViewModels;

namespace BusinessSafe.WebSite.Areas.Company.Tasks
{
    public class InjurySaveTask : CompanyDefaultsSaveTask
    {
        public InjurySaveTask(ICompanyDefaultService companyDefaultService, IDoesCompanyDefaultAlreadyExistGuard companyDefaultAlreadyExistGuard)
            : base(companyDefaultService, companyDefaultAlreadyExistGuard)
        {
        }

        public override CompanyDefaultSaveResponse Execute(SaveCompanyDefaultViewModel viewModel, Guid userId)
        {
            var request = new SaveInjuryRequest(viewModel.CompanyDefaultId, viewModel.CompanyDefaultValue,
                                                viewModel.CompanyId, viewModel.RiskAssessmentId, viewModel.RunMatchCheck,
                                                userId);

            var result = CompanyDefaultService.SaveInjury(request);

            return CompanyDefaultSaveResponse.CreateSavedSuccessfullyResponse(result);
        }
    }
}