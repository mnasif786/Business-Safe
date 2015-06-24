USE BusinessSafe
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'EmployeeTaskNotification' AND COLUMN_NAME = 'EventType') 

BEGIN
	ALTER TABLE [EmployeeTaskNotification]
	ADD [EventTypeId] [smallint] NOT NULL
END
GO

--//@UNDO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'EmployeeTaskNotification' AND COLUMN_NAME = 'EventType')
BEGIN
	ALTER TABLE [EmployeeTaskNotification]
	DROP [EventTypeId] 
END
GO