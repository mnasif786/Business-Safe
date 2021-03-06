USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Document' AND COLUMN_NAME = 'Title')
BEGIN
	ALTER TABLE [Document]
	ADD [Title] nvarchar(100) NULL
	
END
GO	

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Document' AND COLUMN_NAME = 'Title')
BEGIN
	ALTER TABLE [Document]
	DROP COLUMN [Title]
END
GO