USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'SiteId')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	ADD [SiteId] [bigint] NULL		
END
GO	


--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'SiteId')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	DROP COLUMN [SiteId]
END
GO

