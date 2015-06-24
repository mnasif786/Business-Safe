USE [BusinessSafe]	

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'FriendlyReference')
BEGIN
	ALTER TABLE [dbo].[EmployeeChecklist] 
	DROP COLUMN	[FriendlyReference]
END
GO

--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'FriendlyReference')
BEGIN
	ALTER TABLE [dbo].[EmployeeChecklist] 
	ADD [FriendlyReference] [nvarchar](100) NULL
END
GO