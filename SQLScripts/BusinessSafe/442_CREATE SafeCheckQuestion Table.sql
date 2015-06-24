USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckQuestion')
BEGIN
	CREATE TABLE [dbo].[SafeCheckQuestion](
		[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),		
		[CustomQuestion] BIT,
		[Title] [varchar](3000) NOT NULL,
		[RelatedCategoryId] [uniqueidentifier] NULL	
	CONSTRAINT [PK_SafeCheckQuestion] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE ON [SafeCheckQuestion] TO AllowAll
	GRANT SELECT, INSERT, UPDATE ON [SafeCheckQuestion] TO AllowSelectInsertUpdate
END
GO

--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckQuestion')
BEGIN
	DROP TABLE [dbo].[SafeCheckQuestion]
END
GO