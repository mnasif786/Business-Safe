USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklistEmail' AND COLUMN_NAME = 'RecipientEmail')
BEGIN
	ALTER TABLE [dbo].[EmployeeChecklistEmail] 
	ADD [RecipientEmail] [nvarchar](100) NULL
END
GO

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklistEmail' AND COLUMN_NAME = 'RecipientEmail')
BEGIN
	ALTER TABLE [dbo].[EmployeeChecklistEmail] 
	DROP COLUMN [RecipientEmail]
END
GO
