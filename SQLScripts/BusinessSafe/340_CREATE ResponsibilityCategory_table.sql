IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'ResponsibilityCategory')
BEGIN
	CREATE TABLE [ResponsibilityCategory]
	(
		Id bigint IDENTITY(1,1) NOT NULL
		,Category varchar(max) NOT NULL	
		,Deleted bit NOT NULL DEFAULT 0		
		,CreatedOn datetime NOT NULL DEFAULT GetDate()
		,CreatedBy uniqueidentifier NULL
		,LastModifiedBy uniqueidentifier NULL
		,LastModifiedOn datetime NULL DEFAULT GetDate()
		
		,CONSTRAINT PK_ResponsibilityCategory PRIMARY KEY (Id)
	)
	
	GRANT SELECT ON [ResponsibilityCategory] TO [AllowAll]
	
END

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'ResponsibilityCategory')
BEGIN
	DROP TABLE [ResponsibilityCategory]
END
