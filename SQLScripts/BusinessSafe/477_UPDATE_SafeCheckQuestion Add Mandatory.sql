USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckQuestion' AND COLUMN_NAME = 'Mandatory')
BEGIN
	ALTER TABLE [SafeCheckQuestion]
	ADD [Mandatory] [bit] NOT NULL DEFAULT (0)
END

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckQuestion' AND COLUMN_NAME = 'Mandatory')
BEGIN
	ALTER TABLE [SafeCheckQuestion]
	DROP COLUMN [Mandatory]
END

