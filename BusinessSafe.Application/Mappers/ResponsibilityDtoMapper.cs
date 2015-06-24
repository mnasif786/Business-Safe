using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    //public class ResponsibilityDtoMapper
    //{
    //    public ResponsibilityDto MapWithTasksAndTaskDerivedProperties(Responsibility entity)
    //    {

    //        if (entity == null)
    //        {
    //            return null;
    //        }

    //        var dto = new ResponsibilityDto()
    //                  {
    //                      Id = entity.Id,
    //                      CompanyId = entity.CompanyId,
    //                      Description = entity.Description,
    //                      Site = new SiteStructureElementDtoMapper().Map(entity.Site),
    //                      InitialTaskReoccurringType = entity.InitialTaskReoccurringType,
    //                      Owner = new EmployeeDtoMapper().Map(entity.Owner),
    //                      Title = entity.Title,
    //                      ResponsibilityTasks = new TaskDtoMapper().MapWithAssignedTo(entity.ResponsibilityTasks),
    //                      ResponsibilityCategory = new ResponsibilityCategoryDtoMapper().Map(entity.ResponsibilityCategory),
    //                      ResponsibilityReason =entity.ResponsibilityReason!=null ? new ResponsibilityReasonDtoMapper().Map(entity.ResponsibilityReason) : null,
    //                      CreatedOn = entity.CreatedOn.Value,
    //                      Deleted = entity.Deleted,
    //                      NextDueDate = entity.NextDueDate,
    //                      HasMultipleFrequencies = entity.HasMultipleFrequencies,
    //                      StatusDerivedFromTasks = entity.GetStatusDerivedFromTasks()
    //                  };

    //        return dto;
    //    }

    //    public IEnumerable<ResponsibilityDto> MapWithTasksAndTaskDerivedProperties(IEnumerable<Responsibility> entities)
    //    {
    //        return entities.Select(MapWithTasksAndTaskDerivedProperties);
    //    }

    //}

    public class ResponsibilityDtoMapper
    {
        private ResponsibilityDto _responsibilityDto { get; set; }
        private Responsibility _responsibility { get; set; }

        public ResponsibilityDtoMapper(Responsibility responsibility)
        {
            _responsibility = responsibility;
            _responsibilityDto = null;

            if(responsibility != null)
            {
                _responsibilityDto = new ResponsibilityDto()
                {
                    Id = _responsibility.Id,
                    CompanyId = _responsibility.CompanyId,
                    Description = _responsibility.Description,
                    Site = _responsibility.Site != null ? new SiteStructureElementDtoMapper().Map(_responsibility.Site) : null,
                    InitialTaskReoccurringType = _responsibility.InitialTaskReoccurringType,
                    Owner = _responsibility.Owner != null ? new EmployeeDtoMapper().Map(_responsibility.Owner) : null,
                    Title = _responsibility.Title,
                    ResponsibilityCategory = new ResponsibilityCategoryDtoMapper().Map(_responsibility.ResponsibilityCategory),
                    ResponsibilityReason = _responsibility.ResponsibilityReason != null ? new ResponsibilityReasonDtoMapper().Map(_responsibility.ResponsibilityReason) : null,
                    CreatedOn = _responsibility.CreatedOn.Value,
                    Deleted = _responsibility.Deleted,
                    NextDueDate = _responsibility.NextDueDate,
                    HasMultipleFrequencies = _responsibility.HasMultipleFrequencies,
                    StatusDerivedFromTasks = _responsibility.GetStatusDerivedFromTasks()
                };
            }

        }

        public ResponsibilityDto Map()
        {
            return _responsibilityDto;
        }

        public ResponsibilityDtoMapper WithTasks()
        {
            if (_responsibility != null)
            {
                _responsibilityDto.ResponsibilityTasks = new ResponsibilityTaskDtoMapper().MapWithAssignedToAndStatutoryTemplates(_responsibility.ResponsibilityTasks);
            }
            return this;
        }

        public ResponsibilityDtoMapper WithUncreatedStatutoryResponsibilityTaskTemplates()
        {
            if (_responsibility != null)
            {
                _responsibilityDto.UncreatedStatutoryResponsibilityTaskTemplates =
                    new StatutoryResponsibilityTaskTemplateDtoMapper().Map(
                        _responsibility.GetUncreatedStatutoryResponsibilityTaskTemplates()).ToList();
            }
            return this;
        }


        public ResponsibilityDtoMapper WithStatutoryResponsibilityTaskTemplates()
        {
            if (_responsibility != null)
            {
                _responsibilityDto.StatutoryResponsibilityTaskTemplates =
                    new StatutoryResponsibilityTaskTemplateDtoMapper().Map(
                        _responsibility.StatutoryResponsibilityTemplateCreatedFrom.ResponsibilityTasks).ToList();
            }

            return this;
        }
    }
}