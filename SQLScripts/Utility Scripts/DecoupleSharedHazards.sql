--SELECT 
--[MultiHazardRiskAssessmentHazard].[Id] AS [MultiHazardRiskAssessmentHazardId],
--[Hazard].[Name] AS [Hazard_Name],
--[MultiHazardRiskAssessmentHazard].[Description] AS [MultiHazardRiskAssessmentHazard_Description],
--[MultiHazardRiskAssessmentHazard].[RiskAssessmentId] AS [MultiHazardRiskAssessmentHazard_RiskAssessmentId],
--[Hazard].[RiskAssessmentId] AS [Hazard_RiskAssessmentId],
--[Hazard].[Id] AS [Hazard_Id]
--FROM [MultiHazardRiskAssessmentHazard]
--LEFT JOIN [Hazard]
--ON [MultiHazardRiskAssessmentHazard].[HazardId] = [Hazard].[Id]
--LEFT JOIN [RiskAssessment]
--ON [MultiHazardRiskAssessmentHazard].[RiskAssessmentId] = [RiskAssessment].[Id]
--WHERE [MultiHazardRiskAssessmentHazard].[RiskAssessmentId] <> [Hazard].[RiskAssessmentId]

BEGIN TRAN

DECLARE @multiHazardRiskAssessmentHazardId AS BIGINT
DECLARE @riskAssessmentId AS BIGINT
DECLARE @originalHazardId AS BIGINT
DECLARE @newHazardId AS BIGINT

DECLARE curMultiHazardRiskAssessments CURSOR
FOR
SELECT 
[MultiHazardRiskAssessmentHazard].[Id] ,
[MultiHazardRiskAssessmentHazard].[RiskAssessmentId],
[Hazard].[Id]
FROM [MultiHazardRiskAssessmentHazard]
LEFT JOIN [Hazard]
ON [MultiHazardRiskAssessmentHazard].[HazardId] = [Hazard].[Id]
WHERE [MultiHazardRiskAssessmentHazard].[RiskAssessmentId] <> [Hazard].[RiskAssessmentId]

OPEN curMultiHazardRiskAssessments
FETCH NEXT FROM curMultiHazardRiskAssessments INTO @multiHazardRiskAssessmentHazardId, @riskAssessmentId, @originalHazardId

WHILE (@@FETCH_STATUS <> -1)
BEGIN
	
	INSERT INTO [Hazard]
	(
		[Name],
		[RiskAssessmentId],
		[CompanyId],
		[Deleted],
		[CreatedOn],
		[CreatedBy],
		[LastModifiedOn],
		[LastModifiedBy]
	)
	SELECT
	[Name],
	@riskAssessmentId,
	[CompanyId],
	[Deleted],
	[CreatedOn],
	[CreatedBy],
	[LastModifiedOn],
	[LastModifiedBy]
	FROM [Hazard]
	WHERE [Id] = @originalHazardId
	
	SET @newHazardId = SCOPE_IDENTITY()
	
	INSERT INTO [HazardHazardType]
	(
		[HazardId],
		[HazardTypeId]
	)
	SELECT 
	@newHazardId,
	[HazardTypeId]
	FROM [HazardHazardType]
	WHERE [HazardId] = @originalHazardId
	
	UPDATE [MultiHazardRiskAssessmentHazard] 
	SET [HazardId] = @newHazardId
	WHERE [Id] = @multiHazardRiskAssessmentHazardId
	
	FETCH NEXT FROM curMultiHazardRiskAssessments INTO @multiHazardRiskAssessmentHazardId, @riskAssessmentId, @originalHazardId
END

CLOSE curMultiHazardRiskAssessments
DEALLOCATE curMultiHazardRiskAssessments

--COMMIT TRAN
--ROLLBACK TRAN