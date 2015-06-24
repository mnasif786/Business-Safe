using System;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Request;

namespace BusinessSafe.WebSite.Areas.Company.Tasks
{
    public class PersonAtRiskMarkAsDeletedTask : CompanyDefaultDeleteTask
    {
        public PersonAtRiskMarkAsDeletedTask(ICompanyDefaultService companyDefaultService)
            : base(companyDefaultService)
        {
        }

        public override void Execute(long id, long companyId, Guid userId)
        {
            CompanyDefaultService.MarkPersonAtRiskAsDeleted(new MarkCompanyDefaultAsDeletedRequest(id, companyId, userId));
        }
    }
}