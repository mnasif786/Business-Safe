IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckClientTypeQuestion')
BEGIN
	CREATE TABLE [dbo].[SafeCheckClientTypeQuestion](
		[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),		
		[ClientTypeId] [uniqueidentifier] NOT NULL,
		[QuestionId] [uniqueidentifier] NOT NULL,
	CONSTRAINT [PK_SafeCheckClientTypeQuestion] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE ON [SafeCheckClientTypeQuestion] TO AllowAll
	GRANT SELECT, INSERT, UPDATE ON [SafeCheckClientTypeQuestion] TO AllowSelectInsertUpdate
END
GO

--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckClientTypeQuestion')
BEGIN
	DROP TABLE [dbo].[SafeCheckClientTypeQuestion]
END
GO