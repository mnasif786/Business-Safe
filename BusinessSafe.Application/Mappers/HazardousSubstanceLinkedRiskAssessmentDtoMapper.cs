using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class HazardousSubstanceLinkedRiskAssessmentDtoMapper
    {
        public IEnumerable<HazardousSubstanceLinkedRiskAssessmentDto> Map(IEnumerable<HazardousSubstanceRiskAssessment> riskAssessments)
        {
            return riskAssessments.Where(x => x.Deleted == false).Select(Map);
        }

        public HazardousSubstanceLinkedRiskAssessmentDto Map(HazardousSubstanceRiskAssessment riskAssessment)
        {
            return new HazardousSubstanceLinkedRiskAssessmentDto()
                       {
                           Id = riskAssessment.Id
                       };
        }
    }
}