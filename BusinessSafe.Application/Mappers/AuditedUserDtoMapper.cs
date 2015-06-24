using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class AuditedUserDtoMapper
    {
        public AuditedUserDto Map(UserForAuditing user)
        {
            return new AuditedUserDto()
                       {
                           Id = user.Id,
                           Name = user.Employee != null ? user.Employee.FullName : string.Empty
                       };
        }
    }
}