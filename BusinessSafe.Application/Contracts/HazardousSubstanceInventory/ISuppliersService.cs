using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Contracts.HazardousSubstanceInventory
{
    public interface ISuppliersService
    {
        IEnumerable<SupplierDto> GetForCompany(long companyId);
        void MarkSupplierAsDeleted (MarkCompanyDefaultAsDeletedRequest request);
        long CreateSupplier(SaveSupplierRequest request);
        void UpdateSupplier(SaveSupplierRequest request);
        IEnumerable<SupplierDto> Search(string searchTerm, long companyId, int pageLimit);
    }
}