using System;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.Request;

namespace BusinessSafe.WebSite.Areas.Company.Tasks
{
    public class SuppliersMarkAsDeletedTask : CompanyDefaultDeleteTask
    {
        private readonly ISuppliersService _suppliersService;

        public SuppliersMarkAsDeletedTask(ISuppliersService suppliersService)
        {
            _suppliersService = suppliersService;
        }

        public override void Execute(long id, long companyId, Guid userId)
        {
            _suppliersService.MarkSupplierAsDeleted(new MarkCompanyDefaultAsDeletedRequest(id, companyId, userId));
        }
    }
}