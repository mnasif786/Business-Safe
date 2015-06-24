----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the DocumentType table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'DocumentType' AND TYPE = 'U')
BEGIN
	CREATE TABLE [DocumentType]
	(
		Id bigint IDENTITY(1,1) NOT NULL
		,[Name]	nvarchar(500) NULL
		,Deleted bit NOT NULL DEFAULT 0
		,CreatedOn datetime NOT NULL 
		,CreatedBy uniqueidentifier NULL
		,LastModifiedOn datetime NULL 
		,LastModifiedBy uniqueidentifier NULL
	)
END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [DocumentType] TO [AllowAll]
GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'DocumentType' AND TYPE = 'U')
BEGIN
	DROP TABLE [DocumentType]
END
