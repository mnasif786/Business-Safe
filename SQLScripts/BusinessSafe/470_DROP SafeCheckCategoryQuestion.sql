USE [BusinessSafe]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckCategoryQuestion')
BEGIN
	DROP TABLE [SafeCheckCategoryQuestion]
END
GO

--//@UNDO 
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckCategoryQuestion')
BEGIN
	CREATE TABLE [dbo].[SafeCheckCategoryQuestion](
		[Id] [uniqueidentifier] NOT NULL,
		[CategoryId] [uniqueidentifier] NOT NULL,
		[QuestionId] [uniqueidentifier] NOT NULL,
	 CONSTRAINT [PK_SafeCheckCategoryQuestion] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[SafeCheckCategoryQuestion] ADD  DEFAULT (newid()) FOR [Id]
END

