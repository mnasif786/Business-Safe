USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskPhrase' AND COLUMN_NAME = 'HazardousSubstanceGroupId')
BEGIN
	ALTER TABLE [RiskPhrase]
	ADD [HazardousSubstanceGroupId] [bigint] NULL		
END
GO	

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskPhrase' AND COLUMN_NAME = 'HazardousSubstanceGroupId')
BEGIN
	ALTER TABLE [RiskPhrase]
	DROP COLUMN [HazardousSubstanceGroupId]
END
GO

