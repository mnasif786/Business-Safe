USE [BusinessSafe]

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Action')
BEGIN
	CREATE TABLE [dbo].[Action](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[Title] [nvarchar](250) NULL,
		
	CONSTRAINT [PK_Action] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE, DELETE ON [Action] TO AllowAll
	GRANT SELECT, INSERT, UPDATE ON [Action] TO AllowSelectInsertUpdate
END
GO

--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Action')
BEGIN
	DROP TABLE [dbo].[Action]
END
GO