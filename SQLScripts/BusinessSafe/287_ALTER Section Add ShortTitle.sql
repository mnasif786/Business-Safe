USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Section' AND COLUMN_NAME = 'ShortTitle')
BEGIN
	ALTER TABLE [Section]
	ADD [ShortTitle] [nvarchar](50) NULL

END
GO	

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Section' AND COLUMN_NAME = 'ShortTitle')
BEGIN
	UPDATE [Section] SET [ShortTitle] = 'Fuel' WHERE [Id] = 35
	UPDATE [Section] SET [ShortTitle] = 'Heat' WHERE [Id] = 36
	UPDATE [Section] SET [ShortTitle] = 'People at Risk' WHERE [Id] = 37
	UPDATE [Section] SET [ShortTitle] = 'Escape' WHERE [Id] = 38
	UPDATE [Section] SET [ShortTitle] = 'Equipment' WHERE [Id] = 39
	UPDATE [Section] SET [ShortTitle] = 'Warning Systems' WHERE [Id] = 40
	UPDATE [Section] SET [ShortTitle] = 'Administration' WHERE [Id] = 41
	UPDATE [Section] SET [ShortTitle] = 'Review' WHERE [Id] = 42
END
GO

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Section' AND COLUMN_NAME = 'ShortTitle')
BEGIN
	ALTER TABLE [Section]
	DROP COLUMN [ShortTitle] 
END
GO	