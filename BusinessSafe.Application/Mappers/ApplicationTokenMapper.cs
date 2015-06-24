using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public static class ApplicationTokenMapper
    {
        public static ApplicationTokenDto Map(ApplicationToken entity)
        {
            return entity == null ? null : new ApplicationTokenDto
                                               {
                                                   IsEnabled = entity.IsEnabled,
                                                   AppName = entity.AppName
                                               };
        }
    }
}