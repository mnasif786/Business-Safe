IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'ResponsibilityReason')
BEGIN
	CREATE TABLE [ResponsibilityReason]
	(
		Id bigint IDENTITY(1,1) NOT NULL
		,Reason varchar(max) NOT NULL	
		,Deleted bit NOT NULL DEFAULT 0		
		,CreatedOn datetime NOT NULL DEFAULT GetDate()
		,CreatedBy uniqueidentifier NULL
		,LastModifiedBy uniqueidentifier NULL
		,LastModifiedOn datetime NULL DEFAULT GetDate()
		
		,CONSTRAINT PK_ResponsibilityReason PRIMARY KEY (Id)
	)
	
	GRANT SELECT ON [ResponsibilityReason] TO [AllowAll]
	
END

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'ResponsibilityReason')
BEGIN
	DROP TABLE [ResponsibilityCategory]
END
