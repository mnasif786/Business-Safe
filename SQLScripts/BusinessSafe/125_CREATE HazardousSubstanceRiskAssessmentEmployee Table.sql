USE [BusinessSafe]
GO

print 'Create [HazardousSubstanceRiskAssessmentEmployee]'
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'HazardousSubstanceRiskAssessmentEmployee' AND TYPE = 'U')
BEGIN
	CREATE TABLE [HazardousSubstanceRiskAssessmentEmployee](
		[RiskAssessmentId] [bigint] NOT NULL,
		[EmployeeId] [uniqueidentifier] NOT NULL,
	 CONSTRAINT [PK_HazardousSubstanceRiskAssessmentEmployee] PRIMARY KEY CLUSTERED 
	(
		[RiskAssessmentId] ASC,
		[EmployeeId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [HazardousSubstanceRiskAssessmentEmployee] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'HazardousSubstanceRiskAssessmentEmployee' AND TYPE = 'U')
BEGIN
	DROP TABLE [HazardousSubstanceRiskAssessmentEmployee]
END
