USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FurtherControlMeasureDocument')
BEGIN
	EXEC sp_rename 'FurtherControlMeasureDocument', 'TaskDocument'
END

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TaskDocument')
BEGIN
	EXEC sp_rename 'TaskDocument', 'FurtherControlMeasureDocument'
END