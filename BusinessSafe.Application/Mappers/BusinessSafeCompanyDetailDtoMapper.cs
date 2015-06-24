using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class BusinessSafeCompanyDetailDtoMapper
    {
        public BusinessSafeCompanyDetailDto Map(BusinessSafeCompanyDetail entity)
        {
            return new BusinessSafeCompanyDetailDto()
                       {
                           BusinessSafeContactEmployeeFullName = entity.BusinessSafeContactEmployee.FullName,
                           BusinessSafeContactEmployeeId = entity.BusinessSafeContactEmployee.Id
                       };
        }
    }
}
