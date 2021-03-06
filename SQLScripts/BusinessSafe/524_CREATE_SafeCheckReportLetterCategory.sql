USE [BusinessSafe]


IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'ReportLetterStatementCategory')
BEGIN
		CREATE TABLE [dbo].[ReportLetterStatementCategory](
			[Id] [uniqueidentifier] NOT NULL,
			[Name] [varchar](100) NOT NULL
		 CONSTRAINT [PK_ReportLetterStatementCategory] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]
		
		GRANT SELECT, INSERT, UPDATE, DELETE ON [ReportLetterStatementCategory] TO AllowAll
		GRANT SELECT, INSERT, UPDATE ON [ReportLetterStatementCategory] TO AllowSelectInsertUpdate
END
GO


IF  EXISTS( SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'ReportLetterStatementCategory')
BEGIN		
	INSERT INTO [dbo].[ReportLetterStatementCategory]([Id], [Name]) VALUES ('652043c9-eddf-414d-9204-62eb76e3f86d', 'Health and Safety Risk Management')
	INSERT INTO [dbo].[ReportLetterStatementCategory]([Id], [Name]) VALUES ('a3afdfe3-eb69-4bd6-ab7e-68317adf0d22', 'Management of Health and Safety Documentation');
	INSERT INTO [dbo].[ReportLetterStatementCategory]([Id], [Name]) VALUES ('9548df42-716f-43ea-a446-269f7668326c', 'Management of Practices and Procedures')
	INSERT INTO [dbo].[ReportLetterStatementCategory]([Id], [Name]) VALUES ('c0f7fb70-7351-4b4e-891b-fadbe1a08f38', 'Management of the Premises')
END
GO

--//@UNDO 

IF  EXISTS( SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'ReportLetterStatementCategory')
BEGIN
	DROP TABLE [ReportLetterStatementCategory] 
END
