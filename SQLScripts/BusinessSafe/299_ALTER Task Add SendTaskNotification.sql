USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'SendTaskNotification')
BEGIN
	ALTER TABLE [Task]
	ADD [SendTaskNotification] [bit] NULL
END
GO

UPDATE [Task] 
SET [SendTaskNotification] = 0 
WHERE [SendTaskNotification] IS NULL

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'SendTaskNotification')
BEGIN
	ALTER TABLE [Task]
	DROP COLUMN [SendTaskNotification] 
END
GO