USE [BusinessSafe]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'SafetyPhrase')
BEGIN
	CREATE TABLE [dbo].[SafetyPhrase](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[Title] [nvarchar](200) NOT NULL,
		[ReferenceNumber] [nvarchar](50) NOT NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
		CONSTRAINT [PK_SafetyPhrase] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT ON [SafetyPhrase] TO AllowAll
	GRANT INSERT ON [SafetyPhrase] TO AllowAll
	GRANT UPDATE ON [SafetyPhrase] TO AllowAll
END

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafetyPhrase')
BEGIN
	DROP TABLE [SafetyPhrase]
END
GO
