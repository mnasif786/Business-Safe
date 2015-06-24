USE [BusinessSafe]
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklist' AND COLUMN_NAME = 'ChecklistLastModifiedBy')
BEGIN
	ALTER TABLE [SafeCheckChecklist]
	ADD [ChecklistLastModifiedBy] [nvarchar](50) NULL
END
GO

--//@UNDO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklist' AND COLUMN_NAME = 'ChecklistLastModifiedBy')
BEGIN
	ALTER TABLE [SafeCheckChecklist]
	DROP [ChecklistLastModifiedBy] 
END
GO
