USE [BusinessSafe]
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'EscalationTaskDueTomorrow')
BEGIN
GRANT SELECT,UPDATE,INSERT ON EscalationTaskDueTomorrow TO AllowSelectInsertUpdate
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'Task' And COLUMN_NAME = 'SendTaskDueTomorrowNotification')
BEGIN
ALTER TABLE TASK ALTER COLUMN SendTaskDueTomorrowNotification BIT null
END
GO