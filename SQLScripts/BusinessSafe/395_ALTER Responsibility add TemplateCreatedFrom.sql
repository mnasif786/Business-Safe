USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Responsibility' AND COLUMN_NAME = 'StatutoryResponsibilityTemplateCreatedFromId')
BEGIN
	ALTER TABLE [Responsibility]
	ADD [StatutoryResponsibilityTemplateCreatedFromId] BIGINT NULL 
END

--//@UNDO 

USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Responsibility' AND COLUMN_NAME = 'StatutoryResponsibilityTemplateCreatedFromId')
BEGIN
	ALTER TABLE [Responsibility]
	DROP [StatutoryResponsibilityTemplateCreatedFromId] 
END