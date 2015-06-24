USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckCategoryQuestion')
BEGIN
	CREATE TABLE [dbo].[SafeCheckCategoryQuestion](
		[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),		
		[CategoryId] [uniqueidentifier] NOT NULL,
		[QuestionId] [uniqueidentifier] NOT NULL,
	CONSTRAINT [PK_SafeCheckCategoryQuestion] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE ON [SafeCheckCategoryQuestion] TO AllowAll
	GRANT SELECT, INSERT, UPDATE ON [SafeCheckCategoryQuestion] TO AllowSelectInsertUpdate
END
GO

--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckCategoryQuestion')
BEGIN
	DROP TABLE [dbo].[SafeCheckCategoryQuestion]
END
GO