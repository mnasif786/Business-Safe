using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class ActionDtoMapper
    {
        private bool _withTasks = false;
        private bool _withStatus;

        public IEnumerable<ActionDto> Map(IEnumerable<BusinessSafe.Domain.Entities.Action> entities)
        {
            return entities.Select(Map);
        }

        
        public ActionDtoMapper WithTasks()
        {
            _withTasks = true;
            return this;
        }

        public ActionDtoMapper WithStatus()
        {
            _withStatus = true;
            return this;
        }

        public ActionDto Map(BusinessSafe.Domain.Entities.Action entity)
        {
            var dto = new ActionDto
            {
                Id = entity.Id,
                Title = entity.Title,
                AreaOfNonCompliance = entity.AreaOfNonCompliance,
                ActionRequired = entity.ActionRequired,
                TargetTimescale = entity.TargetTimescale,
                AssignedTo = (entity.AssignedTo != null) ? (Guid?)entity.AssignedTo.Id: null,
                DueDate = entity.DueDate,       
                QuestionStatus = entity.QuestionStatus,
                Reference = entity.Reference,
                Category = entity.Category,
                GuidanceNote = entity.GuidanceNotes,
                ActionPlan = entity.ActionPlan
            };

            if (_withStatus)
            {
                dto.Status = entity.GetStatusFromTasks();
            }

            if (_withTasks)
            {
                if (entity.ActionTasks != null)
                {
                    dto.ActionTasks = entity.ActionTasks.Select(x => new ActionTaskDtoMapper().MapWithAssignedTo(x));
                }
            }

            return dto;
        }
    }
}
