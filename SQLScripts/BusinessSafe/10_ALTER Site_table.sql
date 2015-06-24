----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the SiteRequestStructure table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'Site' AND TYPE = 'U')
BEGIN
	ALTER TABLE	[Site]		
		ALTER COLUMN SiteId bigint NULL
	
END
GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'Site' AND TYPE = 'U')
BEGIN
	ALTER TABLE	[Site]
		ALTER COLUMN SiteId bigint NOT NULL
END