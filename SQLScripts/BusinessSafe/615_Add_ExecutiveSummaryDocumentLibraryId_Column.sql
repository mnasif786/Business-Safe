USE [BusinessSafe]
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'ActionPlan' AND COLUMN_NAME = 'ExecutiveSummaryDocumentLibraryId')
BEGIN
	ALTER TABLE [ActionPlan]
	ADD [ExecutiveSummaryDocumentLibraryId] bigint NULL
END
GO

--//@UNDO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'ActionPlan' AND COLUMN_NAME = 'ExecutiveSummaryDocumentLibraryId')
BEGIN
	ALTER TABLE [ActionPlan]
	DROP [ExecutiveSummaryDocumentLibraryId]
END
GO