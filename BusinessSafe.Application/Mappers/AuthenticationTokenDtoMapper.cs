using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public static class AuthenticationTokenDtoMapper
    {
        public static AuthenticationTokenDto Map(AuthenticationToken entity)
        {
            if(entity == null)
                return null;

            var dto = new AuthenticationTokenDto
                      {
                          Id = entity.Id, 
                          ApplicationToken = entity.ApplicationToken.Id,
                          IsEnabled = entity.IsEnabled, 
                          ReasonForDeauthorisation = entity.ReasonForDeauthorisation,
                          User = new UserDtoMapper().MapIncludingAllowedSitesAndEmployee(entity.User)
                      };

            return dto;
        }

        public static IEnumerable<AuthenticationTokenDto> Map(IList<AuthenticationToken> entities)
        {
            return entities.Select(Map);
        }
    }
}