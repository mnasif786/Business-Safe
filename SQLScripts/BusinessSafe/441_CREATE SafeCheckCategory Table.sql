USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckCategory')
BEGIN
	CREATE TABLE [dbo].[SafeCheckCategory](
		[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),	
		[Title] text,
		[ReportTitle] text,
		[Mandatory] bit
		
	CONSTRAINT [PK_SafeCheckCategory] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE ON [SafeCheckCategory] TO AllowAll
	GRANT SELECT, INSERT, UPDATE ON [SafeCheckCategory] TO AllowSelectInsertUpdate
END
GO

--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckCategory')
BEGIN
	DROP TABLE [dbo].[SafeCheckCategory]
END
GO