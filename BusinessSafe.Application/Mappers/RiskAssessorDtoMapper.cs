using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class RiskAssessorDtoMapper
    {
        public RiskAssessorDto Map(RiskAssessor entity)
        {
            if (entity == null) return null;

            var dto = new RiskAssessorDto
                          {
                              Id = entity.Id,
                              HasAccessToAllSites = entity.HasAccessToAllSites,
                              DoNotSendTaskOverdueNotifications = entity.DoNotSendTaskOverdueNotifications,
                              DoNotSendTaskCompletedNotifications = entity.DoNotSendTaskCompletedNotifications,
                              DoNotSendReviewDueNotification = entity.DoNotSendReviewDueNotification,
                              FormattedName = entity.FormattedName,
                              Site = entity.Site != null ? new SiteStructureElementDto(){Id = entity.Site.Id, Name = entity.Site.Name} : null
                          };

            return dto;
        }

        public RiskAssessorDto MapWithEmployee(RiskAssessor entity)
        {
            var dto = Map(entity);
            dto.Employee = new EmployeeDtoMapper().MapWithNationality(entity.Employee);
            return dto;
        }

        public RiskAssessorDto MapWithEmployeeAndSite(RiskAssessor entity)
        {
            var dto = MapWithEmployee(entity);
            dto.Site = entity.Site != null ? new SiteStructureElementDtoMapper().Map(entity.Site) : null;
            return dto;
        }

        public IEnumerable<RiskAssessorDto> Map(IEnumerable<RiskAssessor> entities)
        {
            return entities.Select(Map);
        }

        public IEnumerable<RiskAssessorDto> MapWithEmployee(IEnumerable<RiskAssessor> entities)
        {
            return entities.Select(MapWithEmployee);
        }

        public IEnumerable<RiskAssessorDto> MapWithEmployeeAndSite(IEnumerable<RiskAssessor> entities)
        {
            return entities.Select(MapWithEmployeeAndSite);
        }
    }
}
