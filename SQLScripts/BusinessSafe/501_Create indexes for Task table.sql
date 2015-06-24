IF NOT EXISTS(SELECT * FROM sys.indexes AS i 
WHERE i.name ='IX_Task_Deleted_HazardousSubstanceRiskAssessmentId')

BEGIN

CREATE INDEX [IX_Task_MultiHazardRiskAssessmentHazardId_Deleted] ON [dbo].[Task] ([MultiHazardRiskAssessmentHazardId], [Deleted])
CREATE INDEX [IX_Task_Deleted_HazardousSubstanceRiskAssessmentId] ON [dbo].[Task] ([Deleted], [HazardousSubstanceRiskAssessmentId]) INCLUDE ([Id], [Title], [Description], [Reference], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [TaskAssignedToId], [TaskCompletionDueDate], [TaskStatusId], [TaskCompletedDate], [TaskCompletedComments], [TaskCategoryId], [TaskReoccurringTypeId], [TaskReoccurringEndDate], [FollowingTaskId], [OriginalTaskId], [TaskGuid], [SendTaskNotification], [SendTaskCompletedNotification], [SendTaskOverdueNotification], [TaskCompletedBy], [SiteId])
CREATE INDEX [IX_MultiHazardRiskAssessmentHazard_RiskAssessmentId_Deleted] ON [dbo].[MultiHazardRiskAssessmentHazard] ([RiskAssessmentId], [Deleted])
CREATE INDEX [IX_Task_Deleted_ResponsibilityId] ON [dbo].[Task] ([Deleted], [ResponsibilityId])
CREATE INDEX [IX_Task_Deleted_SignificantFindingId] ON [dbo].[Task] ([Deleted], [SignificantFindingId])
CREATE INDEX [IX_Task_Deleted_TaskAssignedToId_TaskStatusId] ON [dbo].[Task] ([Deleted], [TaskAssignedToId],[TaskStatusId])
CREATE INDEX [IX_RiskAssessmentReview_RiskAssessmentId] ON [dbo].[RiskAssessmentReview] ([RiskAssessmentId])
END
