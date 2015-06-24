USE [BusinessSafe]

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PersonalRiskAssessmentEmployee')
BEGIN
	DROP TABLE [dbo].[PersonalRiskAssessmentEmployee]
END
GO

--//@UNDO 
