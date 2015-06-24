USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckCheckList')
BEGIN
	CREATE TABLE [dbo].[SafeCheckCheckList](
		[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),		
		[CompanyName] [varchar](3000) NOT NULL,
	CONSTRAINT [PK_SafeCheckCheckList] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE, DELETE ON [SafeCheckCheckList] TO AllowAll
	GRANT SELECT, INSERT, UPDATE ON [SafeCheckCheckList] TO AllowSelectInsertUpdate
END
GO

--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckCheckList')
BEGIN
	DROP TABLE [dbo].[SafeCheckCheckList]
END
GO