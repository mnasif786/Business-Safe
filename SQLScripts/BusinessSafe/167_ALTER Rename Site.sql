USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Site')
BEGIN
	EXEC sp_rename 'Site', 'SiteStructureElement'
END

--//@UNDO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SiteStructureElement')
BEGIN
	EXEC sp_rename 'SiteStructureElement', 'Site'
END
