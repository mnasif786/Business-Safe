using System;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Request;

namespace BusinessSafe.WebSite.Areas.Company.Tasks
{
    public class HazardMarkAsDeletedTask: CompanyDefaultDeleteTask
    {
        public HazardMarkAsDeletedTask(ICompanyDefaultService companyDefaultService) : base(companyDefaultService)
        {
        }

        public override void Execute(long id, long companyId, Guid userId)
        {
            CompanyDefaultService.MarkHazardAsDeleted(new MarkCompanyDefaultAsDeletedRequest(id, companyId, userId));
        }
    }
}