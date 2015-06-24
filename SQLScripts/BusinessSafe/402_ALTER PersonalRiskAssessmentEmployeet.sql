USE [BusinessSafe]

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessmentEmployee' AND COLUMN_NAME = 'EmployeeId' AND DATA_TYPE = 'bigint')
BEGIN
	ALTER TABLE [dbo].[PersonalRiskAssessmentEmployee]
	DROP COLUMN [EmployeeId] 
	
	ALTER TABLE [dbo].[PersonalRiskAssessmentEmployee]
	ADD [EmployeeId] [uniqueidentifier] NOT NULL
END
GO

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessmentEmployee' AND COLUMN_NAME = 'EmployeeId' AND DATA_TYPE = 'uniqueidentifier')
BEGIN
	ALTER TABLE [dbo].[PersonalRiskAssessmentEmployee]
	DROP COLUMN [EmployeeId] 

	ALTER TABLE [dbo].[PersonalRiskAssessmentEmployee]
	ADD [EmployeeId] [bigint] NOT NULL
END
GO