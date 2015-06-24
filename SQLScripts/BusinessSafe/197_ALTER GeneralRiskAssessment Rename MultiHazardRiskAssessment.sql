USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GeneralRiskAssessment')
BEGIN
	EXEC sp_rename 'GeneralRiskAssessment', 'MultiHazardRiskAssessment'
END

--//@UNDO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'MultiHazardRiskAssessment')
BEGIN
	EXEC sp_rename 'MultiHazardRiskAssessment', 'GeneralRiskAssessment'
END