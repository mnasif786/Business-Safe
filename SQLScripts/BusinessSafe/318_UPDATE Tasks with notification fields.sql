USE [BusinessSafe]
GO

sp_RENAME 'Task.SendCompletionNotificationEmail', 'SendTaskCompletedNotification' , 'COLUMN'

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'SendTaskOverdueNotification')
BEGIN
	ALTER TABLE [Task]
	ADD [SendTaskOverdueNotification] [bit] NULL
END
GO

UPDATE [Task] 
SET [SendTaskOverdueNotification] = 1 
WHERE [SendTaskOverdueNotification] IS NULL
GO

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'SendTaskOverdueNotification')
BEGIN
	ALTER TABLE [Task]
	DROP COLUMN [SendTaskOverdueNotification] 
END
GO


