USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckQuestionResponse')
BEGIN
	CREATE TABLE [dbo].[SafeCheckQuestionResponse](
		[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),		
		
		[Title] [nvarchar](250) NOT NULL,
		[Comment] [nvarchar](250) NOT NULL,
		[Date] DateTime NULL,
		[ResponseType]  [nvarchar](250) NULL,
		[QuestionId] [uniqueidentifier] NOT NULL,
	CONSTRAINT [PK_SafeCheckQuestionReponse] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE ON [SafeCheckQuestionResponse] TO AllowAll
	GRANT SELECT, INSERT, UPDATE ON [SafeCheckQuestionResponse] TO AllowSelectInsertUpdate
END
GO

--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckQuestionResponse')
BEGIN
	DROP TABLE [dbo].[SafeCheckQuestionResponse]
END
GO