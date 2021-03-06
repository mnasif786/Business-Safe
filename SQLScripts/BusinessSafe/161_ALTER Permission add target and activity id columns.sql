USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Permission' AND COLUMN_NAME = 'PermissionTargetId')
BEGIN
	ALTER TABLE [Permission]
	ADD [PermissionTargetId] [bigint] NULL		
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Permission' AND COLUMN_NAME = 'PermissionActivityId')
BEGIN
	ALTER TABLE [Permission]
	ADD [PermissionActivityId] [int] NULL		
END
GO	


--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Permission' AND COLUMN_NAME = 'PermissionTargetId')
BEGIN
	ALTER TABLE [Permission]
	DROP COLUMN [PermissionTargetId]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Permission' AND COLUMN_NAME = 'PermissionActivityId')
BEGIN
	ALTER TABLE [Permission]
	DROP COLUMN [PermissionActivityId]
END
GO

