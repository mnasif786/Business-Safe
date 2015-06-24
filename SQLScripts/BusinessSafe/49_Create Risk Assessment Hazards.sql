USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'RiskAssessmentHazards' AND TYPE = 'U')
BEGIN
	CREATE TABLE [RiskAssessmentHazards](
		[Id] bigint IDENTITY(1,1) NOT NULL,
		[RiskAssessmentId] [bigint] NOT NULL,
		[HazardId] [int] NOT NULL,
		[Description] [nvarchar](150) NULL,
		[Deleted] [bit] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[CreatedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
	CONSTRAINT [PK_RiskAssessmentHazards] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY] 
END
GO


GRANT SELECT, INSERT,DELETE, UPDATE ON [RiskAssessmentHazards] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'RiskAssessmentHazards' AND TYPE = 'U')
BEGIN
	DROP TABLE [RiskAssessmentHazards]
END
