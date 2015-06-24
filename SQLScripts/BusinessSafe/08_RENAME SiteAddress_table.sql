----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* Rename the SiteAddress table */
----------------------------------------------------------------------------------------------------------------------------------------------------------

GO

IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SiteAddress' AND TYPE = 'U')
	AND NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'Site' AND TYPE = 'U')
BEGIN
	PRINT 'here'
	exec sp_rename 'SiteAddress', 'Site'
END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [Site] TO [AllowAll]

GO

--//@UNDO 
IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'Site' AND TYPE = 'U')
BEGIN
	exec sp_rename 'Site', 'SiteAddress'
END


SELECT * FROM site