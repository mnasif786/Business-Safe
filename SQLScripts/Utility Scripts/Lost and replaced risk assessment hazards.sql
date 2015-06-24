DECLARE @deletedMultiHazardRiskAssessmentHazardId AS BIGINT
DECLARE @multiHazardRiskAssessmentId AS BIGINT
DECLARE @hazardId AS BIGINT
DECLARE @deletedVersionCreatedOn AS DATETIME
DECLARE @deletedDate AS DATETIME
DECLARE @clientId AS BIGINT
DECLARE @createdBy AS UNIQUEIDENTIFIER

DECLARE curDeletedMultiHazardRiskAssessmentHazard CURSOR
FOR
SELECT DISTINCT [EntityId], [UpdateDate]
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
	CreatedDate DATETIME,
	DeletedDate DATETIME
)
	
OPEN curDeletedMultiHazardRiskAssessmentHazard
FETCH NEXT FROM curDeletedMultiHazardRiskAssessmentHazard INTO @deletedMultiHazardRiskAssessmentHazardId, @deletedDate

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
		CreatedDate,
		DeletedDate
	)
	VALUES
	(
		@deletedMultiHazardRiskAssessmentHazardId,
		@multiHazardRiskAssessmentId,
		@hazardId,
		@clientId,
		@createdBy,
		@deletedVersionCreatedOn,
		@deletedDate
	)
	
	FETCH NEXT FROM curDeletedMultiHazardRiskAssessmentHazard INTO @deletedMultiHazardRiskAssessmentHazardId, @deletedDate
END

CLOSE curDeletedMultiHazardRiskAssessmentHazard
DEALLOCATE curDeletedMultiHazardRiskAssessmentHazard
SET IDENTITY_INSERT [MultiHazardRiskAssessmentHazard] OFF

SELECT 
customer.CustomerKey AS [CAN],
poUser.[UserName] AS [User],
deleted.ClientId,
deleted.MultiHazardRiskAssessmentHazardId AS [Deleted RiskAssessmentHazardId], 
deleted.MultiHazardRiskAssessmentId AS [Deleted RiskAssessmentId],
deleted.HazardId AS [Deleted HazardId],
CAST(deleted.CreatedDate AS VARCHAR) AS [Deleted Version Created On],
CAST(deleted.DeletedDate AS VARCHAR) AS [Deleted On],
existing.Id AS [Replacement RiskAssessmentHazardId], 
existing.RiskAssessmentId AS [Replacement RiskAssessmentId],
existing.HazardId AS [Replacement HazardId],
CAST(existing.CreatedOn AS VARCHAR) AS [Replaced On]
FROM #DeletedRecords deleted
LEFT JOIN [MultiHazardRiskAssessmentHazard] existing
ON deleted.MultiHazardRiskAssessmentId = existing.RiskAssessmentId
AND deleted.HazardId = existing.HazardId
LEFT JOIN [Peninsula].[dbo].[TblCustomers] customer
ON deleted.ClientId = customer.CustomerId
LEFT JOIN [PeninsulaOnline].[dbo].[User] poUser
ON deleted.[CreatedBy] = poUser.[Id]
ORDER BY deleted.DeletedDate DESC

DROP TABLE #DeletedRecords
GO

--SELECT * FROM #DeletedRecords