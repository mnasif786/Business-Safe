USE [BusinessSafe]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessmentControlMeasure')
BEGIN
	CREATE TABLE [dbo].[HazardousSubstanceRiskAssessmentControlMeasure](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[HazardousSubstanceRiskAssessmentId] [bigint] NOT NULL,
		[ControlMeasure] [nvarchar](150) NULL,
		[Deleted] [bit] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[CreatedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[LastModifiedBy] [uniqueidentifier] NULL
		CONSTRAINT [PK_HazardousSubstanceRiskAssessmentControlMeassure] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	
	GRANT SELECT ON [HazardousSubstanceRiskAssessmentControlMeasure] TO AllowAll
	GRANT INSERT ON [HazardousSubstanceRiskAssessmentControlMeasure] TO AllowAll
	GRANT UPDATE ON [HazardousSubstanceRiskAssessmentControlMeasure] TO AllowAll
	GRANT DELETE ON [HazardousSubstanceRiskAssessmentControlMeasure] TO AllowAll
END
Go

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessmentControlMeasure')
BEGIN
	DROP TABLE [HazardousSubstanceRiskAssessmentControlMeasure]
END
GO
