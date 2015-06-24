USE [BusinessSafe]
GO

IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckQaAdvisor')
BEGIN
		CREATE TABLE [dbo].[SafeCheckQaAdvisor](
			[Id] [uniqueidentifier] NOT NULL,
			[Forename] [varchar](100) NOT NULL,
			[Surname] [varchar](100) NOT NULL,
			[Email] [varchar](100) NOT NULL,
			[Deleted] bit NOT NULL
		 CONSTRAINT [PK_SafeCheckQaAdvisor] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]
		
		GRANT SELECT, INSERT, UPDATE, DELETE ON [SafeCheckQaAdvisor] TO AllowAll
		GRANT SELECT, INSERT, UPDATE ON [SafeCheckQaAdvisor] TO AllowSelectInsertUpdate
END
GO


IF  EXISTS( SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckQaAdvisor')
BEGIN		
	INSERT INTO [dbo].[SafeCheckQaAdvisor]([Id], [Forename], [Surname], [Email], [Deleted]) VALUES (NEWID(), 'Michael', 'Carter','michael.carter@peninsula-uk.com', 0)
	INSERT INTO [dbo].[SafeCheckQaAdvisor]([Id], [Forename], [Surname], [Email], [Deleted]) VALUES (NEWID(), 'Andrew', 'Burgess', 'Andrew.Burgess@peninsula-uk.com', 0);
	INSERT INTO [dbo].[SafeCheckQaAdvisor]([Id], [Forename], [Surname],[Email], [Deleted]) VALUES (NEWID(), 'Anthony', 'Sykes' ,'Anthony.Sykes@peninsula-uk.com', 0)
	INSERT INTO [dbo].[SafeCheckQaAdvisor]([Id], [Forename], [Surname],[Email], [Deleted]) VALUES (NEWID(), 'Ian', 'Bloxsome', 'Ian.bloxsome@peninsula-uk.com', 0)
END
GO

--//@UNDO 

IF  EXISTS( SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckQaAdvisor')
BEGIN
	DROP TABLE [SafeCheckQaAdvisor] 
END
