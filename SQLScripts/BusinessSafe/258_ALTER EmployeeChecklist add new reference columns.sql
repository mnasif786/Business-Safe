USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'ReferencePrefix')
BEGIN
	ALTER TABLE [dbo].[EmployeeChecklist] 
	ADD [ReferencePrefix] [nvarchar](30) NULL
	
	ALTER TABLE [dbo].[EmployeeChecklist] 
	ADD [ReferenceIncremental] [bigint] NULL
END
GO

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'ReferencePrefix')
BEGIN
	DROP INDEX [IX_EmployeeChecklistReference] ON [EmployeeChecklist]
	
	ALTER TABLE [dbo].[EmployeeChecklist] 
	DROP COLUMN [ReferencePrefix]
	
	ALTER TABLE [dbo].[EmployeeChecklist] 
	DROP COLUMN [ReferenceIncremental]
END
GO
