----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the Document table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Document')
BEGIN
	CREATE TABLE [dbo].[Document](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[DocumentLibraryId] [bigint] NOT NULL,
		[Filename] [nvarchar](500) NULL,
		[Extension] [nvarchar](10) NULL,
		[FilesizeByte] [bigint] NULL,
		[Description] [nvarchar](255) NULL,
		[DocumentTypeId] [bigint] NULL,
		[Deleted] [bit] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[LastModifiedOn] [datetime] NULL,
		[LastModifiedBy] [uniqueidentifier] NULL
		CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)
	) ON [PRIMARY]
END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [Document] TO [AllowAll]
GO

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Document')
BEGIN
	DROP TABLE [Document]
END
