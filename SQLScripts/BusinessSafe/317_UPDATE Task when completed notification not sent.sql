USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'SendCompletionNotificationEmail')
BEGIN
	ALTER TABLE [Task]
	ADD [SendCompletionNotificationEmail] [bit] NULL
END
GO

UPDATE [Task] 
SET [SendCompletionNotificationEmail] = 1 
WHERE [SendCompletionNotificationEmail] IS NULL
GO

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'SendCompletionNotificationEmail')
BEGIN
	ALTER TABLE [Task]
	DROP COLUMN [SendCompletionNotificationEmail] 
END
GO