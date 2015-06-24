using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class PeopleAtRiskDto
    {
        public bool IsSystemDefault { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        
        //public static PeopleAtRiskDto CreateFrom(PeopleAtRisk peopleAtRisk)
        //{
        //    return new PeopleAtRiskDto()
        //               {
        //                   Id = peopleAtRisk.Id,
        //                   Name = peopleAtRisk.Name,
        //                   IsSystemDefault = peopleAtRisk.CompanyId == 0,
        //               };
        //}
    }
}