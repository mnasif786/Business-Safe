USE [BusinessSafe]
GO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckQuestion' AND COLUMN_NAME = 'LastModifieddOn')
BEGIN
	EXEC sp_rename 'SafeCheckQuestion.[LastModifieddOn]', 'LastModifiedOn', 'COLUMN'
END
GO

--//@UNDO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckQuestion' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	EXEC sp_rename 'SafeCheckQuestion.[LastModifiedOn]', 'LastModifieddOn', 'COLUMN'
END
GO
