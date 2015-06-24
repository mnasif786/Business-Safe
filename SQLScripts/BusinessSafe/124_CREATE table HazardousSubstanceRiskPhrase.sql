USE [BusinessSafe]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase')
BEGIN
	CREATE TABLE [dbo].[HazardousSubstanceRiskPhrase](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[HazardousSubstanceId] [nvarchar](200) NOT NULL,
		[RiskPhraseId] [nvarchar](50) NOT NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
		CONSTRAINT [PK_HazardousSubstanceRiskPhrase] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT ON [HazardousSubstanceRiskPhrase] TO AllowAll
	GRANT INSERT ON [HazardousSubstanceRiskPhrase] TO AllowAll
	GRANT UPDATE ON [HazardousSubstanceRiskPhrase] TO AllowAll
END

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase')
BEGIN
	DROP TABLE [HazardousSubstanceRiskPhrase]
END
GO
