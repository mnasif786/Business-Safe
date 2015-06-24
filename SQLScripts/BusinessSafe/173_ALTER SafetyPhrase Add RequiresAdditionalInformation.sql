USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafetyPhrase' AND COLUMN_NAME = 'RequiresAdditionalInformation')
BEGIN
	ALTER TABLE [SafetyPhrase]
	ADD [RequiresAdditionalInformation] bit NULL
END
GO

--//@UNDO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafetyPhrase' AND COLUMN_NAME = 'RequiresAdditionalInformation')
BEGIN
	ALTER TABLE [SafetyPhrase]
	DROP COLUMN [RequiresAdditionalInformation]
END
GO