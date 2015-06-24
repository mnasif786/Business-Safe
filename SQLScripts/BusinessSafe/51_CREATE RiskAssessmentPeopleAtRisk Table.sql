USE [BusinessSafe]
GO

print 'Create [RiskAssessmentPeopleAtRisk]'
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'RiskAssessmentPeopleAtRisk' AND TYPE = 'U')
BEGIN
	CREATE TABLE [RiskAssessmentPeopleAtRisk](
		[RiskAssessmentId] [bigint] NOT NULL,
		[PeopleAtRiskId] [bigint] NOT NULL,
	 CONSTRAINT [PK_RiskAssessmentPeopleAtRisk] PRIMARY KEY CLUSTERED 
	(
		[RiskAssessmentId] ASC,
		[PeopleAtRiskId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [RiskAssessmentPeopleAtRisk] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'RiskAssessmentPeopleAtRisk' AND TYPE = 'U')
BEGIN
	DROP TABLE [RiskAssessmentPeopleAtRisk]
END
