USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' AND COLUMN_NAME = 'AdditionalInformation')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
	ADD [AdditionalInformation] nvarchar(200) NULL
END
GO

--//@UNDO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' AND COLUMN_NAME = 'AdditionalInformation')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
	DROP COLUMN [AdditionalInformation]
END
GO