USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckCheckListAnswer')
BEGIN
	CREATE TABLE [dbo].[SafeCheckCheckListAnswer](
		[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),		
		[CheckListId] [uniqueidentifier] NOT NULL,
		[QuestionId] [uniqueidentifier] NOT NULL,
		[SelectedResponse] [int] NULL,
		[Comment] [text] NULL,		
	CONSTRAINT [PK_SafeCheckCheckListAnswer] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE, DELETE ON [SafeCheckCheckListAnswer] TO AllowAll
	GRANT SELECT, INSERT, UPDATE ON [SafeCheckCheckListAnswer] TO AllowSelectInsertUpdate
END
GO

--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckCheckListAnswer')
BEGIN
	DROP TABLE [dbo].[SafeCheckCheckListAnswer]
END
GO