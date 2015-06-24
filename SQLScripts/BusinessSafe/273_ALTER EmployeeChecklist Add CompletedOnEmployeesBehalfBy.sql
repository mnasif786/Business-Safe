USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'CompletedOnEmployeesBehalfById')
BEGIN
	ALTER TABLE [dbo].[EmployeeChecklist]
	ADD [CompletedOnEmployeesBehalfById] [uniqueidentifier] NULL
END
GO

--//@UNDO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'CompletedOnEmployeesBehalfById')
BEGIN
	ALTER TABLE [dbo].[EmployeeChecklist]
	DROP [CompletedOnEmployeesBehalfById] 
END
GO
