IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckIndustryQuestion')
BEGIN
	CREATE TABLE [dbo].[SafeCheckIndustryQuestion](
		[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),		
		[IndustryId] [uniqueidentifier] NOT NULL,
		[QuestionId] [uniqueidentifier] NOT NULL,
	CONSTRAINT [PK_SafeCheckIndustryQuestion] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE ON [SafeCheckIndustryQuestion] TO AllowAll
	GRANT SELECT, INSERT, UPDATE ON [SafeCheckIndustryQuestion] TO AllowSelectInsertUpdate
END
GO

--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckIndustryQuestion')
BEGIN
	DROP TABLE [dbo].[SafeCheckIndustryQuestion]
END
GO