USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'AddedDocument' AND TYPE = 'U')
BEGIN
	CREATE TABLE [AddedDocument]
	(
		Id BIGINT IDENTITY(1, 1) NOT NULL,
		DocumentLibraryId BIGINT NOT NULL,
		[Filename] NVARCHAR(500) NULL,
		Extension NVARCHAR(10) NULL,
		FilesizeByte BIGINT NULL,
		[Title] NVARCHAR(100) NULL,
		[Description] NVARCHAR(100) NULL,
		DocumentTypeId BIGINT NULL,
		SiteId BIGINT NULL,
		Deleted BIT NOT NULL DEFAULT 0,
		CreatedOn DATETIME NOT NULL,
		CreatedBy UNIQUEIDENTIFIER NOT NULL,
		LastModifiedOn DATETIME NULL,
		LastModifiedBy UNIQUEIDENTIFIER NULL
	)
END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [AddedDocument] TO [AllowAll]
GO

--//@UNDO 
IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'AddedDocument' AND TYPE = 'U')
BEGIN
	DROP TABLE [AddedDocument]
END