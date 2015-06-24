IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckClientQuestion')
BEGIN
	CREATE TABLE [dbo].[SafeCheckClientQuestion](
		[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),		
		[ClientId] [bigint] NOT NULL,
		[ClientAccountNumber] varchar(100) NOT NULL,
		[QuestionId] [uniqueidentifier] NOT NULL,
	CONSTRAINT [PK_SafeCheckClientQuestion] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE ON [SafeCheckClientQuestion] TO AllowAll
	GRANT SELECT, INSERT, UPDATE ON [SafeCheckClientQuestion] TO AllowSelectInsertUpdate
END
GO

--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckClientQuestion')
BEGIN
	DROP TABLE [dbo].[SafeCheckClientQuestion]
END
GO