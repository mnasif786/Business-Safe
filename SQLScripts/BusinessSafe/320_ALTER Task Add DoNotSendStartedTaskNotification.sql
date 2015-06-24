USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'DoNotSendStartedTaskNotification')
BEGIN
	ALTER TABLE [Task]
	ADD [DoNotSendStartedTaskNotification] [bit] NULL
END
GO

UPDATE [Task] 
SET [DoNotSendStartedTaskNotification] = 1
WHERE [DoNotSendStartedTaskNotification] IS NULL

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'DoNotSendStartedTaskNotification')
BEGIN
	ALTER TABLE [Task]
	DROP COLUMN [DoNotSendStartedTaskNotification] 
END
GO