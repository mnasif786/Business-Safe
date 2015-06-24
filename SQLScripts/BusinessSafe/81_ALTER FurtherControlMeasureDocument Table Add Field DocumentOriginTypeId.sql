USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FurtherControlMeasureDocument' AND COLUMN_NAME = 'DocumentOriginTypeId')
BEGIN
	ALTER TABLE [FurtherControlMeasureDocument]
	ADD [DocumentOriginTypeId] SMALLINT NULL
END
GO	

--//@UNDO 
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FurtherControlMeasureDocument' AND COLUMN_NAME = 'DocumentOriginTypeId')
BEGIN
	ALTER TABLE [FurtherControlMeasureDocument]
	DROP COLUMN [DocumentOriginTypeId]
END
GO