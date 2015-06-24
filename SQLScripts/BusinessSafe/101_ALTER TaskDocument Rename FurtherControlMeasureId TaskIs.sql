USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'TaskDocument' AND COLUMN_NAME = 'FurtherControlMeasureId')
BEGIN
	EXEC sp_rename
    @objname = 'TaskDocument.FurtherControlMeasureId',
    @newname = 'TaskId',
    @objtype = 'COLUMN'
END
GO

--//@UNDO
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'TaskDocument' AND COLUMN_NAME = 'FurtherControlMeasureId')
BEGIN
	EXEC sp_rename
    @objname = 'Task.TaskId',
    @newname = 'FurtherControlMeasureId',
    @objtype = 'COLUMN'
END
GO
