----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the ResponsibilityTaskCategory table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'ResponsibilityTaskCategory' AND TYPE = 'U')
BEGIN
	CREATE TABLE	[ResponsibilityTaskCategory]
	(
		Id bigint IDENTITY(1,1) NOT NULL
		,Category varchar(max) NOT NULL	
		,Deleted bit NOT NULL DEFAULT 0		
		,CreatedOn datetime NOT NULL DEFAULT GetDate()
		,CreatedBy uniqueidentifier NULL
		,LastModifiedBy uniqueidentifier NULL
		,LastModifiedOn datetime NULL DEFAULT GetDate()
		
		,CONSTRAINT PK_Id PRIMARY KEY (Id)
	)
END
GO
	
GRANT SELECT ON [ResponsibilityTaskCategory] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'ResponsibilityTaskCategory' AND TYPE = 'U')
BEGIN
	DROP TABLE [ResponsibilityTaskCategory]
END
