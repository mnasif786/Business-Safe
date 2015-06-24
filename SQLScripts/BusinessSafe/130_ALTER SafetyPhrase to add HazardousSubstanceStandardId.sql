USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafetyPhrase' AND COLUMN_NAME = 'HazardousSubstanceStandardId')
BEGIN
	ALTER TABLE [SafetyPhrase]
	ADD [HazardousSubstanceStandardId] [bigint] NULL		
END
GO	


--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafetyPhrase' AND COLUMN_NAME = 'HazardousSubstanceStandardId')
BEGIN
	ALTER TABLE [SafetyPhrase]
	DROP COLUMN [HazardousSubstanceStandardId]
END
GO

