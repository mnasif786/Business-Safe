USE [BusinessSafe]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'RiskPhrase')
BEGIN
	CREATE TABLE [dbo].[RiskPhrase](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[Title] [nvarchar](200) NOT NULL,
		[ReferenceNumber] [nvarchar](50) NOT NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
		CONSTRAINT [PK_RiskPhrase] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT ON [RiskPhrase] TO AllowAll
	GRANT INSERT ON [RiskPhrase] TO AllowAll
	GRANT UPDATE ON [RiskPhrase] TO AllowAll
END

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskPhrase')
BEGIN
	DROP TABLE [RiskPhrase]
END
GO
