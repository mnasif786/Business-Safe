----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the SiteRequestStructure table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'Site' AND TYPE = 'U')
BEGIN
	ALTER TABLE	[Site]
		ADD SiteType smallint NOT NULL DEFAULT 1
	
	
END
GO


--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'Site' AND TYPE = 'U')
BEGIN
	ALTER TABLE	[Site]
		DROP COLUMN  SiteType
END