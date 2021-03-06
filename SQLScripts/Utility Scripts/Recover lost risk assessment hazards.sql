--Show all the entities that have been affected.
SELECT [EntityName], COUNT(*)
FROM [dbo].[Audit]
WHERE [Type] = 'D'
GROUP BY [EntityName]

----Gets a list of all inserts for MHRAHs that have been deleted
--SELECT * 
--FROM [dbo].[Audit]
--WHERE [EntityId] IN
--(
--	SELECT [EntityId]
--	FROM [dbo].[Audit]
--	WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentHazard'
--	AND [Type] = 'D'
--)
--AND [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentHazard'
--AND [Type] = 'I'
--ORDER BY [EntityId] DESC,[Id] DESC

----Gets a list of all updates for MHRAHs that have been deleted
--SELECT * 
--FROM [dbo].[Audit]
--WHERE [EntityId] IN
--(
--	SELECT [EntityId]
--	FROM [dbo].[Audit]
--	WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentHazard'
--	AND [Type] = 'D'
--)
--AND [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentHazard'
--AND [Type] = 'U'
--AND [FieldName] NOT IN ('LastModifiedOn', 'LastModifiedById')
--ORDER BY [EntityId] DESC,[Id] DESC




----Gets a list of all inserts for MHRACMs that have been deleted
--SELECT * 
--FROM [dbo].[Audit]
--WHERE [EntityId] IN
--(
--	SELECT [EntityId]
--	FROM [dbo].[Audit]
--	WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentControlMeasure'
--	AND [Type] = 'D'
--)
--AND [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentControlMeasure'
--AND [Type] = 'I'
--ORDER BY [EntityId] DESC,[Id] DESC

----Gets a list of all updates for MHRACMs that have been deleted
--SELECT * 
--FROM [dbo].[Audit]
--WHERE [EntityId] IN
--(
--	SELECT [EntityId]
--	FROM [dbo].[Audit]
--	WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentControlMeasure'
--	AND [Type] = 'D'
--)
--AND [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentControlMeasure'
--AND [Type] = 'U'
--AND [FieldName] NOT IN ('LastModifiedOn', 'LastModifiedById')
--ORDER BY [EntityId] DESC,[Id] DESC




----Gets a list of all inserts for MHRAFCMTs that have been deleted
--SELECT * 
--FROM [dbo].[Audit]
--WHERE [EntityId] IN
--(
--	SELECT [EntityId]
--	FROM [dbo].[Audit]
--	WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentFurtherControlMeasureTask'
--	AND [Type] = 'D'
--)
--AND [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentFurtherControlMeasureTask'
--AND [Type] = 'I'
--ORDER BY [EntityId] DESC,[Id] DESC

----Gets a list of all updates for MHRAFCMTs that have been deleted
--SELECT * 
--FROM [dbo].[Audit]
--WHERE [EntityId] IN
--(
--	SELECT [EntityId]
--	FROM [dbo].[Audit]
--	WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentFurtherControlMeasureTask'
--	AND [Type] = 'D'
--)
--AND [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentFurtherControlMeasureTask'
--AND [Type] = 'U'
--AND [FieldName] NOT IN ('LastModifiedOn', 'LastModifiedById')
--ORDER BY [EntityId] DESC,[Id] DESC



--USE [BusinessSafe]

BEGIN TRAN

--Go through all deleted Multi Hazard Risk Assessment Hazards that have been deleted and reinsert them.
SET IDENTITY_INSERT [MultiHazardRiskAssessmentHazard] ON
DECLARE @deletedMultiHazardRiskAssessmentHazardId AS BIGINT
DECLARE @multiHazardRiskAssessmentId AS BIGINT
DECLARE @hazardId AS BIGINT
DECLARE @description AS NVARCHAR(150)
DECLARE @createdOn AS DATETIME
DECLARE @createdBy AS UNIQUEIDENTIFIER

DECLARE curDeletedMultiHazardRiskAssessmentHazard CURSOR
FOR
SELECT DISTINCT [EntityId]
FROM [dbo].[Audit]
WITH (NOLOCK)
WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentHazard'
AND [Type] = 'D'
AND [EntityId] NOT IN (SELECT [Id] FROM [MultiHazardRiskAssessmentHazard])

OPEN curDeletedMultiHazardRiskAssessmentHazard
FETCH NEXT FROM curDeletedMultiHazardRiskAssessmentHazard INTO @deletedMultiHazardRiskAssessmentHazardId

WHILE (@@FETCH_STATUS <> -1)
BEGIN

	SELECT @multiHazardRiskAssessmentId = CAST([NewValue] AS BIGINT)
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentHazard'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentHazardId AS NVARCHAR(200))
				AND [FieldName] = 'MultiHazardRiskAssessmentId'
				AND [Type] = 'I'

	SELECT @hazardId = CAST([NewValue] AS BIGINT)
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentHazard'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentHazardId AS NVARCHAR(200))
				AND [FieldName] = 'HazardId'
				AND [Type] = 'I'
	
	SELECT @description = CAST([NewValue] AS NVARCHAR(150))
			FROM [Audit] 
			WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentHazard'
			AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentHazardId AS NVARCHAR(200))
			AND [FieldName] = 'Description'
			AND [Type] = 'I'
			
	SELECT @createdOn = CONVERT(DATETIME, LEFT([NewValue], 10), 103) + CONVERT(DATETIME, RIGHT([NewValue], 8), 108)
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentHazard'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentHazardId AS NVARCHAR(200))
				AND [FieldName] = 'CreatedOn'
				AND [Type] = 'I'
	
	SELECT @createdBy = CAST([NewValue] AS UNIQUEIDENTIFIER)
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentHazard'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentHazardId AS NVARCHAR(200))
				AND [FieldName] = 'CreatedById'
				AND [Type] = 'I'

	PRINT 'Inserting MultiHazardRiskAssessmentHazard with ID ' + CAST(@deletedMultiHazardRiskAssessmentHazardId AS NVARCHAR(200))

	INSERT INTO [MultiHazardRiskAssessmentHazard]
	(
		[Id],
		[RiskAssessmentId],
		[HazardId],
		[Description],
		[Deleted],
		[CreatedOn],
		[CreatedBy],
		[LastModifiedOn],
		[LastModifiedBy]
	)
	VALUES
	(
		@deletedMultiHazardRiskAssessmentHazardId,
		@multiHazardRiskAssessmentId,
		@hazardId,
		@description,
		0,
		@createdOn,
		@createdBy,
		@createdOn,
		@createdBy
	)

	FETCH NEXT FROM curDeletedMultiHazardRiskAssessmentHazard INTO @deletedMultiHazardRiskAssessmentHazardId
END

CLOSE curDeletedMultiHazardRiskAssessmentHazard
DEALLOCATE curDeletedMultiHazardRiskAssessmentHazard
SET IDENTITY_INSERT [MultiHazardRiskAssessmentHazard] OFF
GO


--Perform any updates for deleted MultiHazardRiskAssessmentHazards
DECLARE @updatedMultiHazardRiskAssessmentHazardId AS BIGINT
DECLARE @description AS NVARCHAR(150)

DECLARE curUpdatedMultiHazardRiskAssessmentHazard CURSOR
FOR
SELECT [EntityId], [NewValue]
FROM [dbo].[Audit]
WHERE [EntityId] IN
(
	SELECT [EntityId]
	FROM [dbo].[Audit]
	WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentHazard'
	AND [Type] = 'D'
)
AND [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentHazard'
AND [Type] = 'U'
AND [FieldName] = 'Description'
ORDER BY [Id]

OPEN curUpdatedMultiHazardRiskAssessmentHazard
FETCH NEXT FROM curUpdatedMultiHazardRiskAssessmentHazard INTO @updatedMultiHazardRiskAssessmentHazardId, @description

WHILE (@@FETCH_STATUS <> -1)
BEGIN
	PRINT 'Updating MultiHazardRiskAssessmentHazard with ID ' + CAST(@updatedMultiHazardRiskAssessmentHazardId AS NVARCHAR(200))
	
	UPDATE [MultiHazardRiskAssessmentHazard] 
	SET [Description] = @description
	WHERE Id = @updatedMultiHazardRiskAssessmentHazardId
	
	FETCH NEXT FROM curUpdatedMultiHazardRiskAssessmentHazard INTO @updatedMultiHazardRiskAssessmentHazardId, @description
END

CLOSE curUpdatedMultiHazardRiskAssessmentHazard
DEALLOCATE curUpdatedMultiHazardRiskAssessmentHazard
GO






--Go through all deleted MultiHazardRiskAssessmentControlMeasures that have been deleted and reinsert them.
SET IDENTITY_INSERT [MultiHazardRiskAssessmentControlMeasure] ON
DECLARE @deletedMultiHazardRiskAssessmentControlMeasureId AS BIGINT
DECLARE @multiHazardRiskAssessmentHazardId AS BIGINT
DECLARE @controlMeasure AS NVARCHAR(300)
DECLARE @createdOn AS DATETIME
DECLARE @createdBy AS UNIQUEIDENTIFIER

DECLARE curDeletedMultiHazardRiskAssessmentControlMeasure CURSOR
FOR
SELECT DISTINCT [EntityId]
FROM [dbo].[Audit]
WITH (NOLOCK)
WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentControlMeasure'
AND [Type] = 'D'
AND [EntityId] NOT IN (SELECT [Id] FROM [MultiHazardRiskAssessmentControlMeasure])

OPEN curDeletedMultiHazardRiskAssessmentControlMeasure
FETCH NEXT FROM curDeletedMultiHazardRiskAssessmentControlMeasure INTO @deletedMultiHazardRiskAssessmentControlMeasureId

WHILE (@@FETCH_STATUS <> -1)
BEGIN

	SELECT @multiHazardRiskAssessmentHazardId = CAST([NewValue] AS BIGINT)
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentControlMeasure'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentControlMeasureId AS NVARCHAR(200))
				AND [FieldName] = 'MultiHazardRiskAssessmentHazardId'
				AND [Type] = 'I'
				
	SELECT @controlMeasure = CAST([NewValue] AS NVARCHAR(150))
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentControlMeasure'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentControlMeasureId AS NVARCHAR(200))
				AND [FieldName] = 'ControlMeasure'
				AND [Type] = 'I'

	SELECT @createdOn = CONVERT(DATETIME, LEFT([NewValue], 10), 103) + CONVERT(DATETIME, RIGHT([NewValue], 8), 108)
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentControlMeasure'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentControlMeasureId AS NVARCHAR(200))
				AND [FieldName] = 'CreatedOn'
				AND [Type] = 'I'
	
	SELECT @createdBy = CAST([NewValue] AS UNIQUEIDENTIFIER)
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentControlMeasure'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentControlMeasureId AS NVARCHAR(200))
				AND [FieldName] = 'CreatedById'
				AND [Type] = 'I'

	PRINT 'Inserting MultiHazardRiskAssessmentControlMeasure with ID ' + CAST(@deletedMultiHazardRiskAssessmentControlMeasureId AS NVARCHAR(200))
	
	INSERT INTO [MultiHazardRiskAssessmentControlMeasure]
	(
		[Id],
		[MultiHazardRiskAssessmentHazardId],
		[ControlMeasure],
		[Deleted],
		[CreatedOn],
		[CreatedBy],
		[LastModifiedOn],
		[LastModifiedBy]
	)
	VALUES
	(
		@deletedMultiHazardRiskAssessmentControlMeasureId,
		@multiHazardRiskAssessmentHazardId,
		@controlMeasure,
		0,
		@createdOn,
		@createdBy,
		@createdOn,
		@createdBy
	)

	FETCH NEXT FROM curDeletedMultiHazardRiskAssessmentControlMeasure INTO @deletedMultiHazardRiskAssessmentControlMeasureId
END

CLOSE curDeletedMultiHazardRiskAssessmentControlMeasure
DEALLOCATE curDeletedMultiHazardRiskAssessmentControlMeasure
SET IDENTITY_INSERT [MultiHazardRiskAssessmentControlMeasure] OFF
GO






--Perform any updates for deleted MultiHazardRiskAssessmentControlMeasures
DECLARE @updatedMultiHazardRiskAssessmentControlMeasureId AS BIGINT
DECLARE @controlMeasure AS NVARCHAR(300)

DECLARE curUpdatedMultiHazardRiskAssessmentControlMeasure CURSOR
FOR
SELECT [EntityId], [NewValue]
FROM [dbo].[Audit]
WHERE [EntityId] IN
(
	SELECT [EntityId]
	FROM [dbo].[Audit]
	WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentControlMeasure'
	AND [Type] = 'D'
)
AND [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentControlMeasure'
AND [Type] = 'U'
AND [FieldName] = 'ControlMeasure'
ORDER BY [Id]

OPEN curUpdatedMultiHazardRiskAssessmentControlMeasure
FETCH NEXT FROM curUpdatedMultiHazardRiskAssessmentControlMeasure INTO @updatedMultiHazardRiskAssessmentControlMeasureId, @controlMeasure

WHILE (@@FETCH_STATUS <> -1)
BEGIN
	PRINT 'Updating MultiHazardRiskAssessmentControlMeasure with ID ' + CAST(@updatedMultiHazardRiskAssessmentControlMeasureId AS NVARCHAR(200))
	
	UPDATE [MultiHazardRiskAssessmentControlMeasure] 
	SET [ControlMeasure] = @controlMeasure
	WHERE Id = @updatedMultiHazardRiskAssessmentControlMeasureId
	
	FETCH NEXT FROM curUpdatedMultiHazardRiskAssessmentControlMeasure INTO @updatedMultiHazardRiskAssessmentControlMeasureId, @controlMeasure
END

CLOSE curUpdatedMultiHazardRiskAssessmentControlMeasure
DEALLOCATE curUpdatedMultiHazardRiskAssessmentControlMeasure
GO








--Go through all deleted MultiHazardRiskAssessmentFurtherControlMeasureTask that have been deleted and reinsert them.
SET IDENTITY_INSERT [Task] ON
DECLARE @deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId AS BIGINT
DECLARE @title AS nvarchar(50)
DECLARE @description AS nvarchar(500)
DECLARE @reference AS nvarchar(50)
DECLARE @taskAssignedToId AS UNIQUEIDENTIFIER
DECLARE @taskCompletionDueDate AS DATETIME
DECLARE @createdOn AS DATETIME
DECLARE @createdBy AS UNIQUEIDENTIFIER
DECLARE @categoryId AS BIGINT
DECLARE @taskGuid AS UNIQUEIDENTIFIER
DECLARE @sendTaskNotification AS BIT
DECLARE @sendTaskCompletedNotification AS BIT
DECLARE @sendTaskOverdueNotification AS BIT
DECLARE @multiHazardRiskAssessmentHazardId AS BIGINT

--Status and rocurringtype = 0
--original taskid - taskid

DECLARE curDeletedMultiHazardRiskAssessmentFurtherControlMeasureTask CURSOR
FOR
SELECT DISTINCT [EntityId]
FROM [dbo].[Audit]
WITH (NOLOCK)
WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentFurtherControlMeasureTask'
AND [Type] = 'D'
AND [EntityId] NOT IN (SELECT [Id] FROM [Task])

OPEN curDeletedMultiHazardRiskAssessmentFurtherControlMeasureTask
FETCH NEXT FROM curDeletedMultiHazardRiskAssessmentFurtherControlMeasureTask INTO @deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId

WHILE (@@FETCH_STATUS <> -1)
BEGIN

	SELECT @title = CAST([NewValue] AS  nvarchar(50))
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentFurtherControlMeasureTask'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId AS NVARCHAR(200))
				AND [FieldName] = 'Title'
				AND [Type] = 'I'
				
	SELECT @description = CAST([NewValue] AS  nvarchar(500))
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentFurtherControlMeasureTask'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId AS NVARCHAR(200))
				AND [FieldName] = 'Description'
				AND [Type] = 'I'			
				
	SELECT @reference = CAST([NewValue] AS  nvarchar(50))
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentFurtherControlMeasureTask'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId AS NVARCHAR(200))
				AND [FieldName] = 'Reference'
				AND [Type] = 'I'	
		
	SELECT @taskAssignedToId = CAST([NewValue] AS  UNIQUEIDENTIFIER)
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentFurtherControlMeasureTask'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId AS NVARCHAR(200))
				AND [FieldName] = 'TaskAssignedToId '
				AND [Type] = 'I'	
				
	SELECT @taskCompletionDueDate = CONVERT(DATETIME, LEFT([NewValue], 10), 103) + CONVERT(DATETIME, RIGHT([NewValue], 8), 108)
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentFurtherControlMeasureTask'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId AS NVARCHAR(200))
				AND [FieldName] = 'TaskCompletionDueDate '
				AND [Type] = 'I' 
				
	SELECT @createdOn = CONVERT(DATETIME, LEFT([NewValue], 10), 103) + CONVERT(DATETIME, RIGHT([NewValue], 8), 108)
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentFurtherControlMeasureTask'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId AS NVARCHAR(200))
				AND [FieldName] = 'CreatedOn'
				AND [Type] = 'I' 
				
	SELECT @createdBy = CAST([NewValue] AS UNIQUEIDENTIFIER)
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentFurtherControlMeasureTask'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId AS NVARCHAR(200))
				AND [FieldName] = 'CreatedById'
				AND [Type] = 'I' 			
							
	SELECT @categoryId = CAST([NewValue] AS BIGINT)
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentFurtherControlMeasureTask'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId AS NVARCHAR(200))
				AND [FieldName] = 'CategoryId'
				AND [Type] = 'I' 
				
	SELECT @taskGuid = CAST([NewValue] AS UNIQUEIDENTIFIER)
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentFurtherControlMeasureTask'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId AS NVARCHAR(200))
				AND [FieldName] = 'TaskGuid'
				AND [Type] = 'I' 	
				
	SELECT @taskGuid = CAST([NewValue] AS UNIQUEIDENTIFIER)
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentFurtherControlMeasureTask'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId AS NVARCHAR(200))
				AND [FieldName] = 'TaskGuid'
				AND [Type] = 'I' 	
				
	SELECT @sendTaskNotification = CASE [NewValue] WHEN 'True' THEN 1 ELSE 0 END 
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentFurtherControlMeasureTask'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId AS NVARCHAR(200))
				AND [FieldName] = 'SendTaskNotification'
				AND [Type] = 'I'
	
	SELECT @sendTaskCompletedNotification = CASE [NewValue] WHEN 'True' THEN 1 ELSE 0 END 
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentFurtherControlMeasureTask'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId AS NVARCHAR(200))
				AND [FieldName] = 'SendTaskCompletedNotification'
				AND [Type] = 'I'
	
	SELECT @sendTaskOverdueNotification = CASE [NewValue] WHEN 'True' THEN 1 ELSE 0 END 
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentFurtherControlMeasureTask'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId AS NVARCHAR(200))
				AND [FieldName] = 'SendTaskOverdueNotification'
				AND [Type] = 'I'

	SELECT @multiHazardRiskAssessmentHazardId = CAST([NewValue] AS BIGINT)
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentFurtherControlMeasureTask'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId AS NVARCHAR(200))
				AND [FieldName] = 'MultiHazardRiskAssessmentHazardId'
				AND [Type] = 'I' 
	
	PRINT 'Inserting MultiHazardRiskAssessmentFurtherControlMeasureTask with ID ' + CAST(@deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId AS NVARCHAR(200))
					
	INSERT INTO [Task]
	(
		[Id],
		[Title],
		[Description],
		[Reference],
		[TaskAssignedToId],
		[TaskCompletionDueDate],
		[Deleted],
		[CreatedOn],
		[CreatedBy],
		[LastModifiedOn],
		[LastModifiedBy],
		[TaskCategoryId],
		[TaskReoccurringTypeId],
		[OriginalTaskId],
		[TaskGuid],
		[SendTaskNotification],
		[SendTaskCompletedNotification],
		[SendTaskOverdueNotification],
		[MultiHazardRiskAssessmentHazardId],
		[Discriminator]
	)
	VALUES
	(
		@deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId,
		@title,
		@description,
		@reference,
		@taskAssignedToId,
		@taskCompletionDueDate,
		0,
		@createdOn,
		@createdBy,
		@createdOn,
		@createdBy,
		@categoryId,
		0,
		@deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId,
		@taskGuid,
		@sendTaskNotification,
		@sendTaskCompletedNotification,
		@sendTaskOverdueNotification,
		@multiHazardRiskAssessmentHazardId,
		'MultiHazardRiskAssessmentFurtherControlMeasureTask'
	)

	FETCH NEXT FROM curDeletedMultiHazardRiskAssessmentFurtherControlMeasureTask INTO @deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId
END

CLOSE curDeletedMultiHazardRiskAssessmentFurtherControlMeasureTask
DEALLOCATE curDeletedMultiHazardRiskAssessmentFurtherControlMeasureTask
SET IDENTITY_INSERT [Task] OFF
GO


--COMMIT TRAN
--ROLLBACK TRAN



