USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckImmediateRiskNotification' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE	[dbo].[SafeCheckImmediateRiskNotification] 
	ADD [Deleted] [bit] NOT NULL DEFAULT(0)			
END
GO


--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckImmediateRiskNotification' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE	[dbo].[SafeCheckImmediateRiskNotification]
	DROP COLUMN [Deleted]
END
GO

