----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the SiteRequestStructure table */
----------------------------------------------------------------------------------------------------------------------------------------------------------

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SiteAddress' AND TYPE = 'U')
BEGIN
	CREATE TABLE	[SiteAddress]
	(
		Id bigint IDENTITY(1,1) NOT NULL		
		,SiteId bigint NOT NULL
		,ParentId bigint NULL
		,ClientId bigint NULL
		,[Name] nvarchar(100) NOT NULL
		,Reference nvarchar(100) NULL													
		,Deleted bit NOT NULL DEFAULT 0
		,CreatedOn datetime NOT NULL DEFAULT GetDate()	
		,CreatedBy uniqueidentifier NULL
		,LastModifiedOn datetime NULL DEFAULT GetDate()
		,LastModifiedBy uniqueidentifier NULL
	)
END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [SiteAddress] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'SiteAddress' AND TYPE = 'U')
BEGIN
	DROP TABLE [SiteAddress]
END
