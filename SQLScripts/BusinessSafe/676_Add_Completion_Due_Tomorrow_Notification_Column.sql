USE BusinessSafe
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'Task' AND COLUMN_NAME = 'SendTaskDueTomorrowNotification') 

BEGIN
	ALTER TABLE [Task]
	ADD [SendTaskDueTomorrowNotification] BIT NOT NULL DEFAULT 1
END
GO

--//@UNDO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'Task' AND COLUMN_NAME = 'SendTaskDueTomorrowNotification')
BEGIN
	ALTER TABLE [Task]
	DROP [SendTaskDueTomorrowNotification] 
END
GO