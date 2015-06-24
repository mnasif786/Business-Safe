using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class CompanyDefaultDto
    {
        
        public bool IsSystemDefault { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }

        private static CompanyDefaultDto CreateFrom(ICompanyDefault companyDefault)
        {
            return new CompanyDefaultDto()
            {
                Id = companyDefault.Id,
                Name = companyDefault.Name,
                IsSystemDefault = GetIsSystemDefault(companyDefault.CompanyId)
            };
        }

        public static IEnumerable<CompanyDefaultDto> CreateFrom(IEnumerable<ICompanyDefault> companyDefaults)
        {
            return companyDefaults.Select(CreateFrom);
        }

        protected static bool GetIsSystemDefault(long? companyId)
        {
            return companyId.HasValue == false;
        }

    }
}