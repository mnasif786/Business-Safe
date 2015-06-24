USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'HazardousSubstanceId')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	ADD [HazardousSubstanceId] [bigint] NULL		
END
GO	


--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'HazardousSubstanceId')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	DROP COLUMN [HazardousSubstanceId]
END
GO

