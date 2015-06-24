USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Task')
BEGIN
	EXEC sp_rename 'FurtherControlMeasureTask', 'Task'
END

--//@UNDO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FurtherControlMeasureTask')
BEGIN
	EXEC sp_rename 'Task', 'FurtherControlMeasureTask'
END