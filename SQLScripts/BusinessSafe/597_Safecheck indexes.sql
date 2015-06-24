
IF NOT EXISTS(SELECT * FROM sys.indexes AS i
WHERE name = 'IX_SafeCheckCheckListQuestion_ChecklistId_QuestionId')

BEGIN

CREATE NONCLUSTERED INDEX [IX_SafeCheckCheckListQuestion_ChecklistId_QuestionId] ON [dbo].[SafeCheckCheckListQuestion] 
(
	[CheckListId] ASC,
	[QuestionId] ASC
)
INCLUDE ( [Id],
[Deleted],
[CreatedBy],
[CreatedOn],
[LastModifiedBy],
[LastModifiedOn],
[QuestionNumber],
[CategoryNumber]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

END

GO


IF NOT EXISTS(SELECT * FROM sys.indexes AS i
WHERE name = 'IX_SafeCheckQuestionResponse_QuestionId')

BEGIN

CREATE NONCLUSTERED INDEX [IX_SafeCheckQuestionResponse_QuestionId] ON [dbo].[SafeCheckQuestionResponse] 
(
	[QuestionId] ASC
)
INCLUDE ( [Id],
[Title],
[Date],
[ResponseType],
[SupportingEvidence],
[ActionRequired],
[GuidanceNotes],
[ReportLetterStatement],
[ReportLetterStatementCategoryId],
[CreatedBy],
[CreatedOn],
[LastModifiedBy],
[LastModifiedOn],
[Deleted]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

END

GO

IF NOT EXISTS(SELECT * FROM sys.indexes AS i
WHERE name = 'IX_SafeCheckCheckListAnswer_ChecklistId')

BEGIN

CREATE NONCLUSTERED INDEX [IX_SafeCheckCheckListAnswer_ChecklistId] ON [dbo].[SafeCheckCheckListAnswer] 
(
	[CheckListId] ASC
)
INCLUDE ( [Id],
[QuestionId],
[ResponseId],
[SupportingEvidence],
[ActionRequired],
[AssignedTo],
[GuidanceNotes],
[TimescaleId],
[EmployeeNotListed],
[CreatedBy],
[CreatedOn],
[LastModifiedBy],
[LastModifiedOn],
[Deleted],
[QaComments]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

END

IF NOT EXISTS(SELECT * FROM sys.indexes AS i
WHERE name = 'IX_SafeCheckCheckList_ChecklistCreatedBy')
BEGIN

CREATE NONCLUSTERED INDEX [IX_SafeCheckCheckList_ChecklistCreatedBy] ON [dbo].[SafeCheckCheckList] 
(
	[ChecklistCreatedBy] ASC
)
INCLUDE ( [Id],
[CompanyName],
[ClientId],
[SiteId],
[CoveringLetterContent],
[VisitDate],
[VisitBy],
[VisitType],
[Status],
[PersonSeenJobTitle],
[PersonSeenName],
[PersonSeenSalutation],
[AreasVisited],
[AreasNotVisited],
[EmailAddress],
[ImpressionTypeId],
[CreatedBy],
[CreatedOn],
[LastModifiedBy],
[LastModifiedOn],
[Deleted],
[ActionPlanId],
[ChecklistCreatedOn],
[ChecklistCompletedBy],
[ChecklistCompletedOn],
[ChecklistSubmittedBy],
[ChecklistSubmittedOn],
[ChecklistLastModifiedBy],
[QaAdvisor],
[IndustryId]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

END



ALTER INDEX PK_SafeCheckCheckListAnswer ON SafeCheckCheckListAnswer REBUILD
ALTER INDEX PK_SafeCheckCheckListQuestion ON SafeCheckCheckListQuestion REBUILD
ALTER INDEX PK_SafeCheckCheckList ON SafeCheckCheckList REBUILD