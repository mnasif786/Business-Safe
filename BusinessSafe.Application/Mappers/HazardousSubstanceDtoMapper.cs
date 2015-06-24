using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class HazardousSubstanceDtoMapper
    {
        public HazardousSubstanceDto Map(HazardousSubstance entity)
        {
            return new HazardousSubstanceDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Reference = entity.Reference,
                SdsDate = entity.SdsDate,
                DetailsOfUse = entity.DetailsOfUse,
                AssessmentRequired = entity.AssessmentRequired,
                CompanyId = entity.CompanyId,
                CreatedOn = entity.CreatedOn,
                Supplier = entity.Supplier != null ? new SupplierDtoMapper().Map(entity.Supplier) : null,
                Pictograms = entity.HazardousSubstancePictograms != null ? new PictogramDtoMapper().Map(entity.HazardousSubstancePictograms.Select(hsp => hsp.Pictogram)).ToList() : null,
                RiskPhrases = entity.HazardousSubstanceRiskPhrases != null ? new RiskPhraseDtoMapper().Map(entity.HazardousSubstanceRiskPhrases.Select(x => x.RiskPhrase)).ToList() : null,
                HazardousSubstanceSafetyPhrases = entity.HazardousSubstanceSafetyPhrases != null  ? new HazardousSubstanceSafetyPhraseDtoMapper().Map(entity.HazardousSubstanceSafetyPhrases): null,
                Standard = entity.Standard,
                LinkedRiskAsessments = new HazardousSubstanceLinkedRiskAssessmentDtoMapper().Map(entity.HazardousSubstanceRiskAssessments)
            };
        }

        public IEnumerable<HazardousSubstanceDto> Map(IEnumerable<HazardousSubstance> entities)
        {
            return entities.Select(Map);
        }
    }
}
