USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckCheckListQuestion')
BEGIN
	CREATE TABLE [dbo].[SafeCheckCheckListQuestion](
		[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),		
		[CheckListId] [uniqueidentifier] NOT NULL,
		[QuestionId] [uniqueidentifier] NOT NULL,
	CONSTRAINT [PK_SafeCheckCheckListQuestion] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE ON [SafeCheckCheckListQuestion] TO AllowAll
	GRANT SELECT, INSERT, UPDATE ON [SafeCheckCheckListQuestion] TO AllowSelectInsertUpdate
END
GO

--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckCheckListQuestion')
BEGIN
	DROP TABLE [dbo].[SafeCheckCheckListQuestion]
END
GO