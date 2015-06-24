USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'FurtherControlMeasureTaskCategoryId')
BEGIN
	EXEC sp_rename
    @objname = 'Task.FurtherControlMeasureTaskCategoryId',
    @newname = 'TaskCategoryId',
    @objtype = 'COLUMN'
END
GO

--//@UNDO
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'FurtherControlMeasureTaskCategoryId')
BEGIN
	EXEC sp_rename
    @objname = 'Task.TaskCategoryId',
    @newname = 'FurtherControlMeasureTaskCategoryId',
    @objtype = 'COLUMN'
END
GO
