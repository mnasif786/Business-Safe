USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'PersonalRiskAssessmentId')
BEGIN
	ALTER TABLE EmployeeChecklist
	ADD PersonalRiskAssessmentId [bigint] NULL
END
GO

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'PersonalRiskAssessmentId')
BEGIN
	ALTER TABLE EmployeeChecklist
	DROP COLUMN PersonalRiskAssessmentId
END
GO