USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FurtherControlMeasureDocument' AND COLUMN_NAME = 'DocumentId')
BEGIN
	ALTER TABLE [FurtherControlMeasureDocument]
	ADD [DocumentId] [bigint] NULL
END
GO

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FurtherControlMeasureDocument' AND COLUMN_NAME = 'DocumentId')
BEGIN
	ALTER TABLE [FurtherControlMeasureDocument]
	DROP COLUMN [DocumentId]
END
