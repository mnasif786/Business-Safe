using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Contracts.Company
{
    public interface ICompanyDefaultService
    {
        IEnumerable<CompanyDefaultDto> GetAllHazardsForCompany(long companyId);
        IEnumerable<CompanyDefaultDto> GetAllPeopleAtRiskForCompany(long companyId);
        

        IEnumerable<CompanyDefaultDto> GetAllPeopleAtRiskForRiskAssessments(long companyId, long riskAssessmentId);
        IEnumerable<CompanyDefaultDto> GetAllMultiHazardRiskAssessmentHazardsForCompany(long companyId, HazardTypeEnum hazardType, long riskAssessmentId);
        IEnumerable<CompanyDefaultDto> GetAllSourceOfIgnitionForRiskAssessment(long companyId, long riskAssessmentId);
        IEnumerable<CompanyDefaultDto> GetAllSourceOfFuelForRiskAssessment(long companyId, long riskAssessmentId);
        IEnumerable<CompanyDefaultDto> GetAllFireSafetyControlMeasuresForRiskAssessments(long companyId, long riskAssessmentId);

        CompanyHazardDto GetHazardForCompany(long companyId, long hazardId);
        long SaveSourceOfIgnition(SaveCompanyDefaultRequest request);
        long SaveHazard(SaveCompanyHazardDefaultRequest request);
        long SavePeopleAtRisk(SaveCompanyDefaultRequest request);
        long SaveFireSafetyControlMeasure(SaveCompanyDefaultRequest request);
        long SaveSourceOfFuel(SaveCompanyDefaultRequest request);
        void MarkHazardAsDeleted(MarkCompanyDefaultAsDeletedRequest request);
        void MarkPersonAtRiskAsDeleted(MarkCompanyDefaultAsDeletedRequest request);
        
        bool CanDeleteHazard(long hazardId, long companyId);

        long SaveInjury(SaveInjuryRequest request);
    }
}