USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'FollowingFurtherControlMeasureTaskId')
BEGIN
	EXEC sp_rename
    @objname = 'Task.FollowingFurtherControlMeasureTaskId',
    @newname = 'FollowingTaskId',
    @objtype = 'COLUMN'
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'OriginalFurtherControlMeasureTaskId')
BEGIN
	EXEC sp_rename
    @objname = 'Task.OriginalFurtherControlMeasureTaskId',
    @newname = 'OriginalTaskId',
    @objtype = 'COLUMN'
END
GO


--//@UNDO
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'FollowingFurtherControlMeasureTaskId')
BEGIN
	EXEC sp_rename
    @objname = 'Task.FollowingTaskId',
    @newname = 'FollowingFurtherControlMeasureTaskId',
    @objtype = 'COLUMN'
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'OriginalFurtherControlMeasureTaskId')
BEGIN
	EXEC sp_rename
    @objname = 'Task.OriginalTaskId',
    @newname = 'OriginalFurtherControlMeasureTaskId',
    @objtype = 'COLUMN'
END
GO