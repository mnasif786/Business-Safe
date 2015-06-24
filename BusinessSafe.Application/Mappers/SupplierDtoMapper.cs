using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class SupplierDtoMapper 
    {
        public SupplierDto Map(Supplier entity)
        {
            return new SupplierDto
                   {
                       Id = entity.Id,
                       Name = entity.Name,
                       IsSystemDefault = GetIsSystemDefault(entity.CompanyId)
                   };
        }

        public IEnumerable<SupplierDto> Map(IEnumerable<Supplier> entities)
        {
            return entities.Select(Map);
        }

        private static bool GetIsSystemDefault(long? companyId)
        {
            return companyId.HasValue == false;
        }
    }
}
