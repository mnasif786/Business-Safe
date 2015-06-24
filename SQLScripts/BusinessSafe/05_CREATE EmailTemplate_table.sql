----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the ResponsibilityTaskCategory table */
----------------------------------------------------------------------------------------------------------------------------------------------------------

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'EmailTemplate' AND TYPE = 'U')
BEGIN
	CREATE TABLE	[EmailTemplate]
	(
		 Id bigint IDENTITY(1,1) NOT NULL
		,Name nvarchar(100) NOT NULL 
		,[Subject] nvarchar(200)NOT NULL	
		,Body nvarchar(max)NOT NULL								
		,Deleted bit NOT NULL DEFAULT 0
		,CreatedOn datetime NOT NULL DEFAULT GetDate()	
		,CreatedBy uniqueidentifier NULL
		,LastModifiedOn datetime NULL DEFAULT GetDate()
		,LastModifiedBy uniqueidentifier NULL
	 CONSTRAINT [PK_EmailTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
))
END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [EmailTemplate] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'EmailTemplate' AND TYPE = 'U')
BEGIN
	DROP TABLE [EmailTemplate]
END
