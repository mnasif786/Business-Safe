USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'StatutoryResponsibilityTaskTemplateCreatedFromId')
BEGIN
	ALTER TABLE [Task]
	ADD [StatutoryResponsibilityTaskTemplateCreatedFromId] BIGINT NULL 
END

--//@UNDO 

USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'StatutoryResponsibilityTaskTemplateCreatedFromId')
BEGIN
	ALTER TABLE [Task]
	DROP [StatutoryResponsibilityTaskTemplateCreatedFromId] 
END