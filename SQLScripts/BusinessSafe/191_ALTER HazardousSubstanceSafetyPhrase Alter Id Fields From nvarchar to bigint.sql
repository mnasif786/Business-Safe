USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' AND COLUMN_NAME = 'HazardousSubstanceId')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
	ALTER COLUMN [HazardousSubstanceId] bigint not null
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' AND COLUMN_NAME = 'SafetyPhraseId')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
	ALTER COLUMN [SafetyPhraseId] bigint not null
END
GO

--//@UNDO 
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' AND COLUMN_NAME = 'HazardousSubstanceId')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
	ALTER COLUMN [HazardousSubstanceId] nvarchar(200) not null
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' AND COLUMN_NAME = 'SafetyPhraseId')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
	ALTER COLUMN [SafetyPhraseId] nvarchar(50) not null
END
GO