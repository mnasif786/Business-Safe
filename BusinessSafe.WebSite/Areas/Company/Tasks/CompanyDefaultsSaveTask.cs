using System;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Application.Response;
using BusinessSafe.WebSite.Areas.Company.ViewModels;

namespace BusinessSafe.WebSite.Areas.Company.Tasks
{
    public interface ICompanyDefaultsSaveTask
    {
        CompanyDefaultSaveResponse Execute(SaveCompanyDefaultViewModel viewModel, Guid userId);
    }

    public abstract class CompanyDefaultsSaveTask : ICompanyDefaultsSaveTask
    {
        protected readonly ICompanyDefaultService CompanyDefaultService;
        protected readonly IDoesCompanyDefaultAlreadyExistGuard CompanyDefaultAlreadyExistGuard;

        protected CompanyDefaultsSaveTask(IDoesCompanyDefaultAlreadyExistGuard companyDefaultAlreadyExistGuard):this(null,companyDefaultAlreadyExistGuard)
        {}

        protected CompanyDefaultsSaveTask(ICompanyDefaultService companyDefaultService, IDoesCompanyDefaultAlreadyExistGuard companyDefaultAlreadyExistGuard)
        {
            CompanyDefaultService = companyDefaultService;
            CompanyDefaultAlreadyExistGuard = companyDefaultAlreadyExistGuard;
        }

        public abstract CompanyDefaultSaveResponse Execute(SaveCompanyDefaultViewModel viewModel, Guid userId);
    }
}