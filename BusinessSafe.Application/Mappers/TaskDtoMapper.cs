
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class TaskDtoMapper
    {
        public TaskDto MapWithAssignedToAndHazard(Task entity)
        {
            var dto = MapWithAssignedTo(entity);

            if (entity.Self as MultiHazardRiskAssessmentFurtherControlMeasureTask != null)
            {
                var multiHazardRiskAssessmentFurtherControlMeasureTaskDto =
                    dto as MultiHazardRiskAssessmentFurtherControlMeasureTaskDto;

                var multiHazardRiskAssessmentFurtherControlMeasureTas =
                    entity.Self as MultiHazardRiskAssessmentFurtherControlMeasureTask;

                multiHazardRiskAssessmentFurtherControlMeasureTaskDto.RiskAssessmentHazard =
                    multiHazardRiskAssessmentFurtherControlMeasureTas.MultiHazardRiskAssessmentHazard != null
                        ? new RiskAssessmentHazardDtoMapper().Map(
                            multiHazardRiskAssessmentFurtherControlMeasureTas.MultiHazardRiskAssessmentHazard)
                        : null;

            }

            return dto;
        }

        public TaskDto MapWithAssignedTo(Task entity)
        {
            TaskDto dto = null;

            if (entity.Self as ResponsibilityTask != null)
            {
                var responsibilityTask = (ResponsibilityTask) entity;
                dto = new ResponsibilityTaskDto()
                          {
                              Responsibility = new ResponsibilityDto()
                                                   {
                                                       Id = responsibilityTask.Responsibility.Id,
                                                       Title = responsibilityTask.Responsibility.Title,
                                                       Description = responsibilityTask.Responsibility.Description
                                                   }
                          };

            }

            if (entity.Self as ActionTask != null)
            {
                dto = new ActionTaskDto();
            }


            if (entity.Self as RiskAssessmentReviewTask != null)
            {
                dto = new RiskAssessmentReviewTaskDto();
            }

            if (entity.Self as MultiHazardRiskAssessmentFurtherControlMeasureTask != null)
            {

                if (entity.RiskAssessment.Self is GeneralRiskAssessment)
                {
                    dto = new MultiHazardRiskAssessmentFurtherControlMeasureTaskDto();

                    dto.DefaultDocumentType = DocumentTypeEnum.GRADocumentType; //TODO: This does not belong here, it belongs in the entity then map that. PTD.
                }

                if (entity.RiskAssessment.Self is PersonalRiskAssessment)
                {
                    dto = new MultiHazardRiskAssessmentFurtherControlMeasureTaskDto();
                    dto.DefaultDocumentType = DocumentTypeEnum.PRADocumentType; //TODO: This does not belong here, it belongs in the entity then map that. PTD.
                }

            }

            if (entity.Self as HazardousSubstanceRiskAssessmentFurtherControlMeasureTask != null)
            {
                dto = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTaskDto();

                dto.DefaultDocumentType = DocumentTypeEnum.HSRADocumentType; //TODO: This does not belong here, it belongs in the entity then map that. PTD.
            }

            if (entity.Self as FireRiskAssessmentFurtherControlMeasureTask != null)
            {
                dto = new FireRiskAssessmentFurtherControlMeasureTaskDto();
                dto.DefaultDocumentType = DocumentTypeEnum.FRADocumentType; //TODO: This does not belong here, it belongs in the entity then map that. PTD.
            }

            return PopulateTaskDto(entity, dto);
        }

        protected TaskDto PopulateTaskDto(Task entity, TaskDto dto)
        {
            dto.Id = entity.Id;
            dto.Title = entity.Title;
            dto.Description = entity.Description;
            dto.Reference = entity.Reference;
            dto.TaskGuid = entity.TaskGuid;
            //todo: change task assignedto to employeeDto
            dto.TaskAssignedTo = entity.TaskAssignedTo != null
                                     ? new EmployeeDtoMapper().Map(entity.TaskAssignedTo) //Replaced:TaskAssignedToDto.CreateFrom(entity.TaskAssignedTo)
                                     : null;

            //todo: TaskCompletionDueDate should be DateTime?
            dto.TaskCompletionDueDate = entity.TaskCompletionDueDate.HasValue
                                            ? entity.TaskCompletionDueDate.Value.ToShortDateString()
                                            : null;

            dto.TaskStatus = entity.TaskStatus;
            dto.TaskStatusString = entity.TaskStatus.ToString();
            dto.TaskStatusId = (int) entity.TaskStatus;

            dto.TaskCompletedComments = entity.TaskCompletedComments;
            dto.TaskCompletedDate = entity.TaskCompletedDate;

            //todo: should be ResponsibilityTaskCategoryDto and should be called category.
            dto.TaskCategory = TaskCategoryDtoMapper.Map(entity.Category);

            dto.Documents = entity.Documents != null ? new TaskDocumentDtoMapper().Map(entity.Documents) : null;
            dto.IsReoccurring = entity.TaskReoccurringType != TaskReoccurringType.None; //TODO: This does not belong here, it belongs in the entity then map that. PTD.
            dto.TaskReoccurringType = entity.TaskReoccurringType;
            dto.TaskReoccurringEndDate = entity.TaskReoccurringEndDate;
            dto.Deleted = entity.Deleted;

            //todo: rename and make correct type
            dto.CreatedDate = entity.CreatedOn.HasValue ? entity.CreatedOn.Value.ToShortDateString() : "";

            dto.RiskAssessment = entity.RiskAssessment != null
                                     ? new RiskAssessmentDtoMapper().MapWithTitleReference(entity.RiskAssessment)
                                     : null;

            dto.SendTaskNotification = entity.SendTaskNotification.GetValueOrDefault();
            dto.SendTaskCompletedNotification = entity.SendTaskCompletedNotification.GetValueOrDefault();
            dto.SendTaskOverdueNotification = entity.SendTaskOverdueNotification.GetValueOrDefault();
            dto.SendTaskDueTomorrowNotification = entity.SendTaskDueTomorrowNotification.GetValueOrDefault();

            if (entity.TaskCompletedDate.HasValue
                && entity.LastModifiedBy != null
                && entity.LastModifiedBy.Employee != null)
            {
                dto.TaskCompletedBy = new UserEmployeeDto()
                                          {
                                              EmployeeId = entity.LastModifiedBy.Employee.Id
                                              ,
                                              Forename = entity.LastModifiedBy.Employee.Forename
                                              ,
                                              Surname = entity.LastModifiedBy.Employee.Surname
                                          };
            }

            dto.Site = entity.Site != null ? new SiteStructureElementDtoMapper().Map(entity.Site) : null;

            dto.DerivedDisplayStatus = entity.DerivedDisplayStatus;

            return dto;
        }

        public IEnumerable<TaskDto> MapWithAssignedTo(IEnumerable<Task> entities)
        {
            return entities.Select(MapWithAssignedTo);
        }

        public IEnumerable<TaskDto> MapWithAssignedTo(IEnumerable<FurtherControlMeasureTask> entities)
        {
            return entities.Select(MapWithAssignedTo);
        }

        public IEnumerable<TaskDto> MapWithAssignedTo(IEnumerable<MultiHazardRiskAssessmentFurtherControlMeasureTask> entities)
        {
            return entities.Select(MapWithAssignedTo);
        }

        public IEnumerable<TaskDto> MapWithAssignedTo(IEnumerable<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask> entities)
        {
            return entities.Select(MapWithAssignedTo);
        }

        public IEnumerable<TaskDto> MapWithAssignedTo(IEnumerable<RiskAssessmentReviewTask> entities)
        {
            return entities.Select(MapWithAssignedTo);
        }
    }


    /// <summary>
    /// als experiment
    /// </summary>
    public class TaskMapperFactory
    {
        private TaskDto dto;
        private Task _entity;
        public TaskMapperFactory(Task entity)
        {
            _entity = entity;
            dto = new TaskDto();

            if (entity.Self as ResponsibilityTask != null)
            {
                dto = new ResponsibilityTaskDto();
            }

            if (entity.Self as RiskAssessmentReviewTask != null)
            {
                dto = new RiskAssessmentReviewTaskDto();
            }

            if (entity.Self as MultiHazardRiskAssessmentFurtherControlMeasureTask != null)
            {

                if (entity.RiskAssessment.Self is GeneralRiskAssessment)
                {
                    dto = new MultiHazardRiskAssessmentFurtherControlMeasureTaskDto();

                    dto.DefaultDocumentType = DocumentTypeEnum.GRADocumentType; //TODO: This does not belong here, it belongs in the entity then map that. PTD.
                }

                if (entity.RiskAssessment.Self is PersonalRiskAssessment)
                {
                    dto = new MultiHazardRiskAssessmentFurtherControlMeasureTaskDto();
                    dto.DefaultDocumentType = DocumentTypeEnum.PRADocumentType; //TODO: This does not belong here, it belongs in the entity then map that. PTD.
                }

            }

            if (entity.Self as HazardousSubstanceRiskAssessmentFurtherControlMeasureTask != null)
            {
                dto = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTaskDto();

                dto.DefaultDocumentType = DocumentTypeEnum.HSRADocumentType; //TODO: This does not belong here, it belongs in the entity then map that. PTD.
            }

            if (entity.Self as FireRiskAssessmentFurtherControlMeasureTask != null)
            {
                dto = new FireRiskAssessmentFurtherControlMeasureTaskDto();
                dto.DefaultDocumentType = DocumentTypeEnum.FRADocumentType; //TODO: This does not belong here, it belongs in the entity then map that. PTD.
            }

            dto.Id = entity.Id;
            dto.Title = entity.Title;
            dto.Description = entity.Description;
            dto.Reference = entity.Reference;
            dto.TaskGuid = entity.TaskGuid;
            //todo: change task assignedto to employeeDto
            dto.TaskAssignedTo = entity.TaskAssignedTo != null
                                     ? new EmployeeDtoMapper().Map(entity.TaskAssignedTo) //Replaced:TaskAssignedToDto.CreateFrom(entity.TaskAssignedTo)
                                     : null;

            //todo: TaskCompletionDueDate should be DateTime?
            dto.TaskCompletionDueDate = entity.TaskCompletionDueDate.HasValue
                                            ? entity.TaskCompletionDueDate.Value.ToShortDateString()
                                            : null;

            dto.TaskStatus = entity.TaskStatus;
            dto.TaskStatusString = entity.TaskStatus.ToString();
            dto.TaskStatusId = (int)entity.TaskStatus;

            dto.TaskCompletedComments = entity.TaskCompletedComments;
            dto.TaskCompletedDate = entity.TaskCompletedDate;

            //todo: should be ResponsibilityTaskCategoryDto and should be called category.
            dto.TaskCategory = TaskCategoryDtoMapper.Map(entity.Category);

            dto.Documents = entity.Documents != null ? new TaskDocumentDtoMapper().Map(entity.Documents) : null;
            dto.IsReoccurring = entity.TaskReoccurringType != TaskReoccurringType.None; //TODO: This does not belong here, it belongs in the entity then map that. PTD.
            dto.TaskReoccurringType = entity.TaskReoccurringType;
            dto.TaskReoccurringEndDate = entity.TaskReoccurringEndDate;
            dto.Deleted = entity.Deleted;

            //todo: rename and make correct type
            dto.CreatedDate = entity.CreatedOn.HasValue ? entity.CreatedOn.Value.ToShortDateString() : "";

            dto.RiskAssessment = entity.RiskAssessment != null
                                     ? new RiskAssessmentDtoMapper().MapWithEmployeesAndNonEmployeesAndSiteAndRiskAssessor(entity.RiskAssessment)
                                     : null;

            dto.SendTaskNotification = entity.SendTaskNotification.GetValueOrDefault();
            dto.SendTaskCompletedNotification = entity.SendTaskCompletedNotification.GetValueOrDefault();
            dto.SendTaskOverdueNotification = entity.SendTaskOverdueNotification.GetValueOrDefault();
            dto.SendTaskDueTomorrowNotification = entity.SendTaskDueTomorrowNotification.GetValueOrDefault();

            if (entity.TaskCompletedDate.HasValue
                && entity.LastModifiedBy != null
                && entity.LastModifiedBy.Employee != null)
            {
                dto.TaskCompletedBy = new UserEmployeeDto()
                {
                    EmployeeId = entity.LastModifiedBy.Employee.Id
                    ,
                    Forename = entity.LastModifiedBy.Employee.Forename
                    ,
                    Surname = entity.LastModifiedBy.Employee.Surname
                };
            }

            dto.Site = entity.Site != null ? new SiteStructureElementDtoMapper().Map(entity.Site) : null;

            dto.DerivedDisplayStatus = entity.DerivedDisplayStatus;
          
        }

        public TaskMapperFactory WithAssignedTo()
        {
            dto.TaskAssignedTo = _entity.TaskAssignedTo != null
                                    ? new EmployeeDtoMapper().Map(_entity.TaskAssignedTo) //Replaced:TaskAssignedToDto.CreateFrom(entity.TaskAssignedTo)
                                    : null;
            return this;
        }
    }


}