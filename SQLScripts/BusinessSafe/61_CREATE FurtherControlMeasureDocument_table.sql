----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the FurtherControlMeasureDocument table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'FurtherControlMeasureDocument' AND TYPE = 'U')
BEGIN
	CREATE TABLE [FurtherControlMeasureDocument]
	(
		Id bigint IDENTITY(1,1) NOT NULL
		,FurtherControlMeasureId bigint NULL
		,DocumentLibraryId bigint NOT NULL
		,[Filename]	nvarchar(500) NULL
		,Extension	nvarchar(10) NULL
		,FilesizeByte bigint NULL
		,[Description] nvarchar(255) NULL
		,DocumentTypeId bigint NULL
		,Deleted bit NOT NULL DEFAULT 0
		,CreatedOn datetime NOT NULL 
		,CreatedBy uniqueidentifier NOT NULL
		,LastModifiedOn datetime NULL 
		,LastModifiedBy uniqueidentifier NULL
	)
END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [FurtherControlMeasureDocument] TO [AllowAll]
GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'FurtherControlMeasureDocument' AND TYPE = 'U')
BEGIN
	DROP TABLE [FurtherControlMeasureDocument]
END

