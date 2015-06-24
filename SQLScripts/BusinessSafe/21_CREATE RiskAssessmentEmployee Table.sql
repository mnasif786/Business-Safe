USE [BusinessSafe]
GO

print 'Create [RiskAssessmentEmployee]'
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'RiskAssessmentEmployees' AND TYPE = 'U')
BEGIN
	CREATE TABLE [RiskAssessmentEmployees](
		[RiskAssessmentId] [bigint] NOT NULL,
		[EmployeeId] [uniqueidentifier] NOT NULL,
	 CONSTRAINT [PK_RiskAssessmentEmployee] PRIMARY KEY CLUSTERED 
	(
		[RiskAssessmentId] ASC,
		[EmployeeId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [RiskAssessmentEmployees] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'RiskAssessmentEmployees' AND TYPE = 'U')
BEGIN
	DROP TABLE [RiskAssessmentEmployees]
END
