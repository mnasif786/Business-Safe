USE [BusinessSafe]
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklist' AND COLUMN_NAME = 'QaAdvisor')
BEGIN
	ALTER TABLE [SafeCheckChecklist]
	ADD [QaAdvisor] UNIQUEIDENTIFIER NULL
END
GO

--//@UNDO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklist' AND COLUMN_NAME = 'QaAdvisor')
BEGIN
	ALTER TABLE [SafeCheckChecklist]
	DROP [QaAdvisor] 
END
GO
