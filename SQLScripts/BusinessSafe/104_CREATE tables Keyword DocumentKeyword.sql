USE [BusinessSafe]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Keyword')
BEGIN
	CREATE TABLE [dbo].[Keyword](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[Text] [nvarchar](50) NOT NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
		CONSTRAINT [PK_Keyword] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT ON [Keyword] TO AllowAll
	GRANT INSERT ON [Keyword] TO AllowAll
	GRANT UPDATE ON [Keyword] TO AllowAll
END

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'DocumentKeyword')
BEGIN
	CREATE TABLE [dbo].[DocumentKeyword](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[KeywordId] [bigint] NULL,
		[DocumentId] [bigint] NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
		CONSTRAINT [PK_DocumentKeyword] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT ON [DocumentKeyword] TO AllowAll
	GRANT INSERT ON [DocumentKeyword] TO AllowAll
	GRANT UPDATE ON [DocumentKeyword] TO AllowAll
END

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Keyword')
BEGIN
	DROP TABLE [Keyword]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DocumentKeyword')
BEGIN
	DROP TABLE [DocumentKeyword]
END
GO
