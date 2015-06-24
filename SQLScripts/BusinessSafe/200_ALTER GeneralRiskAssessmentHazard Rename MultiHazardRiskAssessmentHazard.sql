USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GeneralRiskAssessmentHazard')
BEGIN
	EXEC sp_rename 'GeneralRiskAssessmentHazard', 'MultiHazardRiskAssessmentHazard'
END

--//@UNDO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'MultiHazardRiskAssessmentHazard')
BEGIN
	EXEC sp_rename 'MultiHazardRiskAssessmentHazard', 'GeneralRiskAssessmentHazard'
END