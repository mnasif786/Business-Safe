using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using System.Linq;
using BusinessSafe.Domain.Entities;
using NHibernate.Driver;

namespace BusinessSafe.Application.Mappers
{
    public class ActionPlanDtoMapper
    {
        private bool _withActions = false;
        private bool _withSites = false;
        private bool _actionTasks = false;
        private bool _withStatus = false;

        public ActionPlanDtoMapper WithActions()
        {            
            _withActions = true;
            return this;
        }

        public ActionPlanDtoMapper WithSites()
        {
            _withSites = true;
            return this;
        }

        public ActionPlanDtoMapper WithActionTasks()
        {
            _actionTasks = true;
            return this;
        }

        public ActionPlanDtoMapper WithStatus()
        {
            _withStatus = true;
            return this;
        }


        public ActionPlanDto Map(ActionPlan entity)
        {
            ActionPlanDto dto = new ActionPlanDto()
            {
                Id = entity.Id,
                CompanyId = entity.CompanyId,
                Title = entity.Title,
            
                DateOfVisit = entity.DateOfVisit,
                VisitBy = entity.VisitBy,
                SubmittedOn = entity.SubmittedOn,
                AreasVisited = entity.AreasVisited,
                AreasNotVisited = entity.AreasNotVisited,
                ExecutiveSummaryDocumentLibraryId = entity.ExecutiveSummaryDocumentLibraryId,
                NoLongerRequired = entity.NoLongerRequired
            };

            if(_withSites)
            {
                if (entity.Site != null)
                {
                    dto.Site = new SiteDtoMapper().Map(entity.Site);
                }
            }

            if (_withActions)
            {
                if (entity.Actions != null)
                {
                    if (_actionTasks)
                    {
                        dto.Actions = new ActionDtoMapper().WithTasks().WithStatus().Map(entity.Actions);
                    }
                    else
                    {
                        dto.Actions = new ActionDtoMapper().Map(entity.Actions);
                    }       
                }
            }

            if (_withStatus)
            {
                if (entity.Actions != null)
                {
                    dto.Status = entity.GetStatusFromActions();   
                }
            }

            return dto;
        }

        public IEnumerable<ActionPlanDto> Map(IEnumerable<Domain.Entities.ActionPlan> entities)
        {
            return entities.Select(Map);
        }
    }

}