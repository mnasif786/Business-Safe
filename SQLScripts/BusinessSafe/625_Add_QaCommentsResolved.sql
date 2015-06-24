USE [BusinessSafe]
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCheckListAnswer' AND COLUMN_NAME = 'QaCommentsResolved')
BEGIN
	ALTER TABLE [SafeCheckCheckListAnswer]
	ADD [QaCommentsResolved] bit
END
GO

--//@UNDO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCheckListAnswer' AND COLUMN_NAME = 'QaCommentsResolved')
BEGIN
	ALTER TABLE [SafeCheckCheckListAnswer]
	DROP COLUMN [QaCommentsResolved] 
END
GO