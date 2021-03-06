USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'ClientDocumentType' AND TYPE = 'U')
BEGIN
	CREATE TABLE [dbo].[ClientDocumentType](
		[Id] [bigint] NOT NULL,
		[DocumentGroupId] [bigint] NOT NULL,
	 CONSTRAINT [PK_ClientDocumentType] PRIMARY KEY CLUSTERED 
	(
		[DocumentGroupId] ASC,
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [ClientDocumentType] TO [AllowAll]
GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'ClientDocumentType' AND TYPE = 'U')
BEGIN
	DROP TABLE [ClientDocumentType]
END