USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'Title' AND CHARACTER_MAXIMUM_LENGTH = 50)
BEGIN
	ALTER TABLE [Task]
	ALTER COLUMN [Title] NVARCHAR(250)
END


--//@UNDO 


