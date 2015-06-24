using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using System.Collections.Generic;

namespace BusinessSafe.Application.Contracts.Company
{
    public interface IRiskAssessorService
    {
        RiskAssessorDto GetByIdAndCompanyId(long id, long companyId);
        IEnumerable<RiskAssessorDto> GetByCompanyId(long companyId);
        long Create(CreateEditRiskAssessorRequest request);
        IEnumerable<RiskAssessorDto> Search(SearchRiskAssessorRequest request);
        bool HasRiskAssessorGotOutstandingRiskAssessments(long riskAssessorId, long companyId);
		void Update(CreateEditRiskAssessorRequest request);
        void MarkDeleted(MarkRiskAssessorAsDeletedAndUndeletedRequest request);

        void MarkUndeleted(MarkRiskAssessorAsDeletedAndUndeletedRequest request);
    }
}
