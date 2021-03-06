USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'User' AND COLUMN_NAME = 'PermissionsApplyToAllSites')
BEGIN
	ALTER TABLE [User]
	ADD [PermissionsApplyToAllSites] [bit] NULL		
END
GO	

UPDATE [User] SET [PermissionsApplyToAllSites] = 0 WHERE [PermissionsApplyToAllSites] = NULL
GO

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'User' AND COLUMN_NAME = 'PermissionsApplyToAllSites')
BEGIN
	ALTER TABLE [User]
	DROP COLUMN [PermissionsApplyToAllSites] 
END
GO	

