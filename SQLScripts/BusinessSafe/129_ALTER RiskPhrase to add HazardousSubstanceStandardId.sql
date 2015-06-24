USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskPhrase' AND COLUMN_NAME = 'HazardousSubstanceStandardId')
BEGIN
	ALTER TABLE [RiskPhrase]
	ADD [HazardousSubstanceStandardId] [bigint] NULL		
END
GO	


--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskPhrase' AND COLUMN_NAME = 'HazardousSubstanceStandardId')
BEGIN
	ALTER TABLE [RiskPhrase]
	DROP COLUMN [HazardousSubstanceStandardId]
END
GO

