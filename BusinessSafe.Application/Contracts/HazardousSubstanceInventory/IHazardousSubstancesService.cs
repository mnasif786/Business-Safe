using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request.HazardousSubstanceInventory;

namespace BusinessSafe.Application.Contracts.HazardousSubstanceInventory
{
    public interface IHazardousSubstancesService
    {
        IEnumerable<HazardousSubstanceDto> GetForCompany(long companyId);
        IEnumerable<HazardousSubstanceDto> Search(SearchHazardousSubstancesRequest request);
		IEnumerable<HazardousSubstanceDto> GetHazardousSubstancesForSearchTerm(string term, long companyId, int pageLimit);
        long Add(AddHazardousSubstanceRequest request);
        bool HasHazardousSubstanceGotRiskAssessments(long hazardousSubstanceId, long companyId);
        void MarkForDelete(MarkHazardousSubstanceAsDeleteRequest request);
        void Reinstate(ReinstateHazardousSubstanceRequest request);
        HazardousSubstanceDto GetByIdAndCompanyId(long hazardousSubstanceId, long companyId);
        void Update(AddHazardousSubstanceRequest request);
        bool HasSupplierGotHazardousSubstances(long supplierId, long companyId);
    }
}