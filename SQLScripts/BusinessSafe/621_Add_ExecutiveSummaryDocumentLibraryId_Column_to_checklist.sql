USE [BusinessSafe]
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'ExecutiveSummaryDocumentLibraryId')
BEGIN
	ALTER TABLE [SafeCheckCheckList]
	ADD [ExecutiveSummaryDocumentLibraryId] bigint NULL
END
GO

--//@UNDO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'ExecutiveSummaryDocumentLibraryId')
BEGIN
	ALTER TABLE [SafeCheckCheckList]
	DROP [ExecutiveSummaryDocumentLibraryId]
END
GO