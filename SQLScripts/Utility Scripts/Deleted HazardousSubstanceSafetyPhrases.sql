DECLARE @deletedHazardousSubstanceSafetyPhraseId AS BIGINT
DECLARE @deletedDate AS DATETIME
DECLARE @deletedBy AS UNIQUEIDENTIFIER
DECLARE @hazardousSubstanceId AS BIGINT
DECLARE @safetyPhraseId AS BIGINT
DECLARE @additionalInformation AS NVARCHAR(200)

CREATE TABLE #DeletedDeletedHazardousSubstanceSafetyPhrases
(
	HazardousSubstanceSafetyPhraseId BIGINT NULL,
	DeletedDate DATETIME NULL,
	DeletedBy UNIQUEIDENTIFIER,
	HazardousSubstanceId BIGINT NULL,
	SafetyPhraseId BIGINT NULL,
	AdditionalInformation NVARCHAR(200)	NULL
)
	
DECLARE curDeletedHazardousSubstanceSafetyPhrase CURSOR
FOR
SELECT DISTINCT [EntityId], [UpdateDate], [UserName]
FROM [dbo].[Audit]
WITH (NOLOCK)
WHERE [EntityName] = 'BusinessSafe.Domain.Entities.HazardousSubstanceSafetyPhrase'
AND [Type] = 'D'
ORDER BY [UpdateDate] DESC

OPEN curDeletedHazardousSubstanceSafetyPhrase
FETCH NEXT FROM curDeletedHazardousSubstanceSafetyPhrase INTO @deletedHazardousSubstanceSafetyPhraseId, @deletedDate, @deletedBy

WHILE (@@FETCH_STATUS <> -1)
BEGIN

	SELECT @hazardousSubstanceId = CAST([NewValue] AS BIGINT)
	FROM [Audit] 
	WHERE [EntityName] = 'BusinessSafe.Domain.Entities.HazardousSubstanceSafetyPhrase'
	AND [EntityId] = CAST(@deletedHazardousSubstanceSafetyPhraseId AS NVARCHAR(200))
	AND [FieldName] = 'HazardousSubstanceId'
	AND [Type] = 'I'

	SELECT @safetyPhraseId = CAST([NewValue] AS BIGINT)
	FROM [Audit] 
	WHERE [EntityName] = 'BusinessSafe.Domain.Entities.HazardousSubstanceSafetyPhrase'
	AND [EntityId] = CAST(@deletedHazardousSubstanceSafetyPhraseId AS NVARCHAR(200))
	AND [FieldName] = 'SafetyPhraseId'
	AND [Type] = 'I'
				
	SELECT @additionalInformation = CASE WHEN [NewValue] = '' THEN NULL ELSE [NewValue] END
	FROM [Audit] 
	WHERE [EntityName] = 'BusinessSafe.Domain.Entities.HazardousSubstanceSafetyPhrase'
	AND [EntityId] = CAST(@deletedHazardousSubstanceSafetyPhraseId AS NVARCHAR(200))
	AND [FieldName] = 'AdditionalInformation'
	AND [Type] = 'I'
				
	INSERT INTO #DeletedDeletedHazardousSubstanceSafetyPhrases
	(
		HazardousSubstanceSafetyPhraseId,
		DeletedDate,
		DeletedBy,
		HazardousSubstanceId,
		SafetyPhraseId,
		AdditionalInformation
	)
	VALUES
	(
		@deletedHazardousSubstanceSafetyPhraseId,
		@deletedDate,
		@deletedBy,
		@hazardousSubstanceId,
		@safetyPhraseId,
		@additionalInformation
	)
	
	FETCH NEXT FROM curDeletedHazardousSubstanceSafetyPhrase INTO @deletedHazardousSubstanceSafetyPhraseId, @deletedDate, @deletedBy
END

CLOSE curDeletedHazardousSubstanceSafetyPhrase
DEALLOCATE curDeletedHazardousSubstanceSafetyPhrase

SELECT 
[HazardousSubstance].CompanyId,
customer.CustomerKey AS [CAN],
[HazardousSubstance].Id,
[HazardousSubstance].Reference,
[HazardousSubstance].Name,
[SafetyPhrase].Id,
[SafetyPhrase].ReferenceNumber,
[SafetyPhrase].Title,
deleted.AdditionalInformation,
poUser.Id,
poUser.UserName,
deleted.DeletedDate,
deleted.HazardousSubstanceSafetyPhraseId,
[HazardousSubstanceSafetyPhrase].Id
FROM #DeletedDeletedHazardousSubstanceSafetyPhrases deleted
LEFT JOIN [SafetyPhrase]
ON deleted.SafetyPhraseId = [SafetyPhrase].Id
LEFT JOIN [HazardousSubstance]
ON deleted.HazardousSubstanceId = [HazardousSubstance].Id
LEFT JOIN [Peninsula].[dbo].[TblCustomers] customer
ON [HazardousSubstance].CompanyId = customer.CustomerId
LEFT JOIN [PeninsulaOnline].[dbo].[User] poUser
ON deleted.[DeletedBy] = poUser.[Id]
LEFT JOIN [HazardousSubstanceSafetyPhrase]
ON deleted.SafetyPhraseId = [HazardousSubstanceSafetyPhrase].SafetyPhraseId
AND deleted.HazardousSubstanceId = [HazardousSubstanceSafetyPhrase].HazardousSubstanceId
WHERE customer.CustomerKey NOT LIKE 'DEMO%'
ORDER BY 
customer.CustomerKey, 
[HazardousSubstance].Reference,
[SafetyPhrase].ReferenceNumber



SELECT 
[HazardousSubstance].CompanyId,
customer.CustomerKey AS [CAN],
[HazardousSubstance].Id AS [HazardousSubstance.Id],
[HazardousSubstance].Reference AS [HazardousSubstance.Reference],
[HazardousSubstance].Name AS [HazardousSubstance.Name],
[SafetyPhrase].Id AS [SafetyPhrase.Id],
[SafetyPhrase].ReferenceNumber AS [SafetyPhrase.ReferenceNumber],
[SafetyPhrase].Title AS [SafetyPhrase.Title],
deleted.AdditionalInformation,
poUser.Id AS [UserId],
poUser.UserName,
CAST(MAX(deleted.DeletedDate) AS VARCHAR) AS [Last Delete Date],
COUNT(*) AS [No. of Deleted Recs],
COUNT(DISTINCT [HazardousSubstanceSafetyPhrase].Id) AS [No. of Recreated Recs]
FROM #DeletedDeletedHazardousSubstanceSafetyPhrases deleted
LEFT JOIN [SafetyPhrase]
ON deleted.SafetyPhraseId = [SafetyPhrase].Id
LEFT JOIN [HazardousSubstance]
ON deleted.HazardousSubstanceId = [HazardousSubstance].Id
LEFT JOIN [Peninsula].[dbo].[TblCustomers] customer
ON [HazardousSubstance].CompanyId = customer.CustomerId
LEFT JOIN [PeninsulaOnline].[dbo].[User] poUser
ON deleted.[DeletedBy] = poUser.[Id]
LEFT JOIN [HazardousSubstanceSafetyPhrase]
ON deleted.SafetyPhraseId = [HazardousSubstanceSafetyPhrase].SafetyPhraseId
AND deleted.HazardousSubstanceId = [HazardousSubstanceSafetyPhrase].HazardousSubstanceId
WHERE customer.CustomerKey NOT LIKE 'DEMO%'
GROUP BY
[HazardousSubstance].CompanyId,
customer.CustomerKey,
[HazardousSubstance].Id,
[HazardousSubstance].Reference,
[HazardousSubstance].Name,
[SafetyPhrase].Id,
[SafetyPhrase].ReferenceNumber,
[SafetyPhrase].Title,
deleted.AdditionalInformation,
poUser.Id,
poUser.UserName
ORDER BY 
COUNT(*) DESC,
customer.CustomerKey, 
[HazardousSubstance].Reference,
[SafetyPhrase].ReferenceNumber

--DROP TABLE #DeletedDeletedHazardousSubstanceSafetyPhrases