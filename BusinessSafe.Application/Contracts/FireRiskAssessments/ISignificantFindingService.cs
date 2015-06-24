using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Contracts.FireRiskAssessments
{
    public interface ISignificantFindingService
    {
        void MarkSignificantFindingAsDeleted(MarkSignificantFindingAsDeletedRequest request);
    }
}
