USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PersonalRiskAssessment')
BEGIN
	CREATE TABLE [dbo].[PersonalRiskAssessment] (
		[Id] [bigint] NULL
	) ON [PRIMARY]

	GRANT SELECT, INSERT, DELETE, UPDATE ON [PersonalRiskAssessment] TO [AllowAll]
END

--//@UNDO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PersonalRiskAssessment')
BEGIN
	DROP TABLE [PersonalRiskAssessment];
END