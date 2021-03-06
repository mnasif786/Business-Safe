DECLARE @deletedMultiHazardRiskAssessmentHazardId AS BIGINT
DECLARE @multiHazardRiskAssessmentId AS BIGINT
DECLARE @hazardId AS BIGINT
DECLARE @deletedVersionCreatedOn AS DATETIME
DECLARE @deletedDate AS DATETIME
DECLARE @clientId AS BIGINT
DECLARE @createdBy AS UNIQUEIDENTIFIER

DECLARE curDeletedMultiHazardRiskAssessmentHazard CURSOR
FOR
SELECT DISTINCT [EntityId]
FROM [dbo].[Audit]
WITH (NOLOCK)
WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentHazard'
AND [Type] = 'D'

CREATE TABLE #DeletedRecords
(
	MultiHazardRiskAssessmentHazardId BIGINT,
	MultiHazardRiskAssessmentId BIGINT,
	HazardId BIGINT,
	ClientID BIGINT,
	CreatedBy UNIQUEIDENTIFIER,
	CreatedDate DATETIME
)

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
				
	SELECT @deletedVersionCreatedOn = CONVERT(DATETIME, LEFT([NewValue], 10), 103) + CONVERT(DATETIME, RIGHT([NewValue], 8), 108)
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentHazard'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentHazardId AS NVARCHAR(200))
				AND [FieldName] = 'CreatedOn'
				AND [Type] = 'I'
				
	SELECT @clientId = [ClientId]
				FROM [RiskAssessment]
				WHERE [Id] = @multiHazardRiskAssessmentId	
				
	SELECT @createdBy = CAST([NewValue] AS UNIQUEIDENTIFIER)
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentHazard'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentHazardId AS NVARCHAR(200))
				AND [FieldName] = 'CreatedById'
				AND [Type] = 'I'
				
	INSERT INTO #DeletedRecords
	(
		MultiHazardRiskAssessmentHazardId,
	    MultiHazardRiskAssessmentId,
		HazardId,
		ClientId,
		CreatedBy,
		CreatedDate
	)
	VALUES
	(
		@deletedMultiHazardRiskAssessmentHazardId,
		@multiHazardRiskAssessmentId,
		@hazardId,
		@clientId,
		@createdBy,
		@deletedVersionCreatedOn
	)

	FETCH NEXT FROM curDeletedMultiHazardRiskAssessmentHazard INTO @deletedMultiHazardRiskAssessmentHazardId
END

CLOSE curDeletedMultiHazardRiskAssessmentHazard
DEALLOCATE curDeletedMultiHazardRiskAssessmentHazard

SELECT 
deleted.ClientId,
customer.CustomerKey AS [CAN],
deleted.[CreatedBy] as [User Id],
poUser.[UserName] AS [User Name],
deleted.MultiHazardRiskAssessmentHazardId AS [RiskAssessmentHazardId], 
deleted.MultiHazardRiskAssessmentId AS [RiskAssessmentId],
riskAssessment.[Reference] as [Risk Assessment Reference],
riskAssessment.[Title] as [Risk Assessment Title],
deleted.HazardId AS [HazardId],
hazard.[Name] as [Hazard Name]
FROM #DeletedRecords deleted
LEFT JOIN [RiskAssessment] riskAssessment
ON deleted.MultiHazardRiskAssessmentId = riskAssessment.Id
LEFT JOIN [Hazard] hazard
ON deleted.HazardId = hazard.Id
LEFT JOIN [Peninsula].[dbo].[TblCustomers] customer
ON deleted.ClientId = customer.CustomerId
LEFT JOIN [PeninsulaOnline].[dbo].[User] poUser
ON deleted.[CreatedBy] = poUser.[Id]
ORDER BY 
customer.CustomerKey, 
riskAssessment.Id, 
hazard.[Name]

DROP TABLE #DeletedRecords
GO

DECLARE @deletedMultiHazardRiskAssessmentControlMeasureId AS BIGINT
DECLARE @multiHazardRiskAssessmentHazardId AS BIGINT
DECLARE @createdBy AS UNIQUEIDENTIFIER

CREATE TABLE #DeletedControlMeasures
(
	ControlMeasureId BIGINT,
	MultiHazardRiskAssessmentHazardId BIGINT,
	CreatedBy UNIQUEIDENTIFIER
)

DECLARE curDeletedMultiHazardRiskAssessmentControlMeasure CURSOR
FOR
SELECT DISTINCT [EntityId]
FROM [dbo].[Audit]
WITH (NOLOCK)
WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentControlMeasure'
AND [Type] = 'D'

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

	SELECT @createdBy = CAST([NewValue] AS UNIQUEIDENTIFIER)
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentControlMeasure'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentControlMeasureId AS NVARCHAR(200))
				AND [FieldName] = 'CreatedById'
				AND [Type] = 'I'
				
	INSERT INTO #DeletedControlMeasures
	(
		[ControlMeasureId],
		[MultiHazardRiskAssessmentHazardId],
		[CreatedBy]
	)
	VALUES
	(
		@deletedMultiHazardRiskAssessmentControlMeasureId,
		@multiHazardRiskAssessmentHazardId,
		@createdBy
	)

	FETCH NEXT FROM curDeletedMultiHazardRiskAssessmentControlMeasure INTO @deletedMultiHazardRiskAssessmentControlMeasureId
END

CLOSE curDeletedMultiHazardRiskAssessmentControlMeasure
DEALLOCATE curDeletedMultiHazardRiskAssessmentControlMeasure
GO

SELECT 
riskAssessment.ClientId,
customer.CustomerKey AS [CAN],
deleted.[CreatedBy] as [User Id],
poUser.[UserName] AS [User Name],
riskAssessmentHazard.Id as [riskAssessmentHazard Id],
riskAssessment.Id AS [Risk Assessment Id],
riskAssessment.[Reference] as [Risk Assessment Reference],
riskAssessment.[Title] as [Risk Assessment Title],
hazard.Id as [Hazard Id],
hazard.[Name] as [Hazard Name],
deleted.ControlMeasureId as [Control Measure Id],
controlMeasure.ControlMeasure as [Control Measure Description]
FROM #DeletedControlMeasures deleted
LEFT JOIN [MultiHazardRiskAssessmentControlMeasure] controlMeasure
ON deleted.ControlMeasureId = controlMeasure.Id
LEFT JOIN [MultiHazardRiskAssessmentHazard] riskAssessmentHazard
ON deleted.[MultiHazardRiskAssessmentHazardId] = riskAssessmentHazard.Id
LEFT Join Hazard hazard
ON riskAssessmentHazard.HazardId = hazard.Id
LEFT Join [RiskAssessment] riskAssessment
ON riskAssessmentHazard.RiskAssessmentId = riskAssessment.Id
LEFT JOIN [Peninsula].[dbo].[TblCustomers] customer
ON riskAssessment.ClientId = customer.CustomerId
LEFT JOIN [PeninsulaOnline].[dbo].[User] poUser
ON deleted.[CreatedBy] = poUser.[Id]
ORDER BY 
customer.CustomerKey, 
riskAssessment.Id, 
hazard.[Name],
controlMeasure.ControlMeasure

DROP TABLE #DeletedControlMeasures

GO

CREATE TABLE #DeletedTasks
(
	TaskId BIGINT,
	MultiHazardRiskAssessmentHazardId BIGINT,
	CreatedBy UNIQUEIDENTIFIER
)

DECLARE @deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId AS BIGINT
DECLARE @multiHazardRiskAssessmentHazardId AS BIGINT
DECLARE @createdBy UNIQUEIDENTIFIER

DECLARE curDeletedMultiHazardRiskAssessmentFurtherControlMeasureTask CURSOR
FOR
SELECT DISTINCT [EntityId]
FROM [dbo].[Audit]
WITH (NOLOCK)
WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentFurtherControlMeasureTask'
AND [Type] = 'D'

OPEN curDeletedMultiHazardRiskAssessmentFurtherControlMeasureTask
FETCH NEXT FROM curDeletedMultiHazardRiskAssessmentFurtherControlMeasureTask INTO @deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId

WHILE (@@FETCH_STATUS <> -1)
BEGIN
	
	SELECT @multiHazardRiskAssessmentHazardId = CAST([NewValue] AS BIGINT)
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentFurtherControlMeasureTask'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId AS NVARCHAR(200))
				AND [FieldName] = 'MultiHazardRiskAssessmentHazardId'
				AND [Type] = 'I' 
				
	SELECT @createdBy = CAST([NewValue] AS UNIQUEIDENTIFIER)
				FROM [Audit] 
				WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentFurtherControlMeasureTask'
				AND [EntityId] = CAST(@deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId AS NVARCHAR(200))
				AND [FieldName] = 'CreatedById'
				AND [Type] = 'I'
	
	INSERT INTO #DeletedTasks
	(
		TaskId,
		MultiHazardRiskAssessmentHazardId,
		CreatedBy
	)
	VALUES
	(
		@deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId,
		@multiHazardRiskAssessmentHazardId,
		@createdBy
	)

	FETCH NEXT FROM curDeletedMultiHazardRiskAssessmentFurtherControlMeasureTask INTO @deletedMultiHazardRiskAssessmentFurtherControlMeasureTaskId
END

CLOSE curDeletedMultiHazardRiskAssessmentFurtherControlMeasureTask
DEALLOCATE curDeletedMultiHazardRiskAssessmentFurtherControlMeasureTask


SELECT 
riskAssessment.ClientId,
customer.CustomerKey AS [CAN],
deleted.[CreatedBy] as [User Id],
poUser.[UserName] AS [User Name],
riskAssessmentHazard.Id as [riskAssessmentHazard Id],
riskAssessment.Id AS [Risk Assessment Id],
riskAssessment.[Reference] as [Risk Assessment Reference],
riskAssessment.[Title] as [Risk Assessment Title],
hazard.Id as [Hazard Id],
hazard.[Name] as [Hazard Name],
task.Id as [Task Id],
task.Reference as [Task Ref],
task.Title as [Task Title]
FROM #DeletedTasks deleted
LEFT JOIN [Task] task
ON deleted.TaskId = task.Id
LEFT JOIN [MultiHazardRiskAssessmentHazard] riskAssessmentHazard
ON deleted.[MultiHazardRiskAssessmentHazardId] = riskAssessmentHazard.Id
LEFT Join Hazard hazard
ON riskAssessmentHazard.HazardId = hazard.Id
LEFT Join [RiskAssessment] riskAssessment
ON riskAssessmentHazard.RiskAssessmentId = riskAssessment.Id
LEFT JOIN [Peninsula].[dbo].[TblCustomers] customer
ON riskAssessment.ClientId = customer.CustomerId
LEFT JOIN [PeninsulaOnline].[dbo].[User] poUser
ON deleted.[CreatedBy] = poUser.[Id]
ORDER BY 
customer.CustomerKey,
riskAssessment.Id, 
hazard.[Name],
task.Reference


DROP TABLE #DeletedTasks



