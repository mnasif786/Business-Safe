using System;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Request.Responsibilities
{
    public class CreateResponsibilityTasksFromWizardRequest
    {
        public long CompanyId { get; set; }
        public long SiteId { get; set; }
        public long ResponsibilityId { get; set; }
        public long TaskTemplateId { get; set; }
        public TaskReoccurringType Frequency { get; set; }

        public Guid AssigneeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid UserId { get; set; }
        public Guid TaskGuid { get; set; }

        public static CreateResponsibilityTasksFromWizardRequest Create(long companyId, Guid userId, long siteId, long responsibilityId, long taskTemplateid, TaskReoccurringType frequency, Guid? assigneeId, string startDate, string endDate, Guid taskGuid)
        {
            var result = new CreateResponsibilityTasksFromWizardRequest
                             {
                                 CompanyId = companyId,
                                 UserId = userId,
                                 SiteId = siteId,
                                 ResponsibilityId = responsibilityId,
                                 TaskTemplateId = taskTemplateid,
                                 Frequency = frequency,
                                 AssigneeId = assigneeId.HasValue ? assigneeId.Value : Guid.NewGuid(),
                                 StartDate = DateTime.Parse(startDate),
                                 EndDate = !string.IsNullOrEmpty(endDate) ? DateTime.Parse(endDate) : (DateTime?)null,
                                 TaskGuid = taskGuid
                             };

            return result;
        }
    }
}