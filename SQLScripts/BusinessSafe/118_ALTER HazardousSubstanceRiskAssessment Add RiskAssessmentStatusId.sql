USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'RiskAssessmentStatusId')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment] ADD [RiskAssessmentStatusId] smallint NULL
END
GO


--//@UNDO 
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'RiskAssessmentStatusId')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment] DROP [RiskAssessmentStatusId] 
END
GO
