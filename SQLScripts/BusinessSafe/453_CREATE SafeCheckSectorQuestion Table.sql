IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckSectorQuestion')
BEGIN
	CREATE TABLE [dbo].[SafeCheckSectorQuestion](
		[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),		
		[SectorId] [uniqueidentifier] NOT NULL,
		[QuestionId] [uniqueidentifier] NOT NULL,
	CONSTRAINT [PK_SafeCheckSectorQuestion] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE ON [SafeCheckSectorQuestion] TO AllowAll
	GRANT SELECT, INSERT, UPDATE ON [SafeCheckSectorQuestion] TO AllowSelectInsertUpdate
END
GO

--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckSectorQuestion')
BEGIN
	DROP TABLE [dbo].[SafeCheckSectorQuestion]
END
GO