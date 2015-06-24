USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'RiskAssessmentsNonEmployees' AND TYPE = 'U')
BEGIN
	CREATE TABLE [RiskAssessmentsNonEmployees](
		[RiskAssessmentId] [bigint] NOT NULL,
		[NonEmployeeId] [bigint] NOT NULL,
	 CONSTRAINT [PK_RiskAssessmentsNonEmployees] PRIMARY KEY CLUSTERED 
	(
		[RiskAssessmentId] ASC,
		[NonEmployeeId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

END
GO


GRANT SELECT, INSERT,DELETE, UPDATE ON [RiskAssessmentsNonEmployees] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'RiskAssessmentsNonEmployees' AND TYPE = 'U')
BEGIN
	DROP TABLE [RiskAssessmentsNonEmployees]
END