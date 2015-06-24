USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckChecklist' AND COLUMN_NAME = 'ClientSiteGeneralNotes')
BEGIN
	ALTER TABLE [dbo].[SafeCheckChecklist]
	ADD [ClientSiteGeneralNotes] nvarchar(3000) NULL
END
GO

--//@UNDO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckChecklist' AND COLUMN_NAME = 'ClientSiteGeneralNotes')
BEGIN
	ALTER TABLE [dbo].[SafeCheckChecklist]
	DROP [ClientSiteGeneralNotes]
END
GO