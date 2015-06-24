using System.Linq;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    //TODO: This all needs to go, this should not be here. PTD
    public class CompanyHazardDto : CompanyDefaultDto
    {
        public bool IsGra { get; set; }
        public bool IsPra { get; set; }
        public bool IsFra { get; set; }

        public static CompanyHazardDto CreateFrom(Hazard companyDefault)
        {
            return new CompanyHazardDto()
                       {
                           Id = companyDefault.Id,
                           Name = companyDefault.Name,
                           IsSystemDefault = GetIsSystemDefault(companyDefault.CompanyId),
                           IsGra = companyDefault.HazardTypes.Count(x => x.Id == (int)HazardTypeEnum.General) == 1,
                           IsPra = companyDefault.HazardTypes.Count(x => x.Id == (int)HazardTypeEnum.Personal) == 1,
                           IsFra = companyDefault.HazardTypes.Count(x=>x.Id == (int)HazardTypeEnum.Fire)==1
                       };
        }
    }
}