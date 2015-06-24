using System;
using BusinessSafe.Application.Contracts.Company;

namespace BusinessSafe.WebSite.Areas.Company.Tasks
{
    public interface ICompanyDefaultDeleteTask
    {
        void Execute(long id, long companyId, Guid userId);
    }

    public abstract class CompanyDefaultDeleteTask : ICompanyDefaultDeleteTask
    {
        protected readonly ICompanyDefaultService CompanyDefaultService;

        protected CompanyDefaultDeleteTask()
        {}

        protected CompanyDefaultDeleteTask(ICompanyDefaultService companyDefaultService)
        {
            CompanyDefaultService = companyDefaultService;
        }

        public abstract void Execute(long id, long companyId, Guid userId);
    }
}