USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FurtherControlMeasureTask' AND COLUMN_NAME = 'Discriminator')
BEGIN
	ALTER TABLE [FurtherControlMeasureTask]
	ADD [Discriminator] [nvarchar](50)
END
GO

--//@UNDO 
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FurtherControlMeasureTask' AND COLUMN_NAME = 'Discriminator')
BEGIN
	ALTER TABLE [FurtherControlMeasureTask]
	DROP COLUMN [Discriminator]
END
GO
